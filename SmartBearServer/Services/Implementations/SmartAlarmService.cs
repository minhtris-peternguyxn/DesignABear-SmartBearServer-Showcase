using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using SmartBearServer.Hubs;
using SmartBearServer.Infrastructure;
using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Model;
using SmartBearServer.Model.DTOs;
using SmartBearServer.Repositories;
using SmartBearServer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SmartBearServer.Services.Interfaces;
using SmartBearServer.Data;

namespace SmartBearServer.Services.Implementations
{
    /// <summary>
    /// Implementation of ISmartAlarmService using the Result Pattern and 3-layer architecture.
    /// </summary>
    public class SmartAlarmService : ISmartAlarmService
    {
        private readonly ISmartAlarmRepository _alarmRepo;
        private readonly IHubContext<LLMHub> _hubContext;
        private readonly ILogger<SmartAlarmService> _logger;
        private readonly IAIService _aiService;
        private readonly Microsoft.Extensions.Caching.Distributed.IDistributedCache _cache;
        private readonly IServiceScopeFactory _scopeFactory;

        public SmartAlarmService(
            ISmartAlarmRepository alarmRepo,
            IHubContext<LLMHub> hubContext,
            ILogger<SmartAlarmService> logger,
            IAIService aiService,
            Microsoft.Extensions.Caching.Distributed.IDistributedCache cache,
            IServiceScopeFactory scopeFactory)
        {
            _alarmRepo = alarmRepo;
            _hubContext = hubContext;
            _logger = logger;
            _aiService = aiService;
            _cache = cache;
            _scopeFactory = scopeFactory;
        }

        /// <inheritdoc/>
        public async Task<Result<List<SmartAlarmDto>>> GetMyAlarmsAsync(Guid userId)
        {
            var alarms = await _alarmRepo.GetByUserAsync(userId);
            return Result<List<SmartAlarmDto>>.Success(alarms.Select(ToDto).ToList());
        }

        /// <inheritdoc/>
        public async Task<Result<SmartAlarmDto>> GetMyAlarmByIdAsync(Guid userId, string alarmId)
        {
            var alarm = await _alarmRepo.GetByIdForUserAsync(userId, alarmId);
            if (alarm == null)
                return Result<SmartAlarmDto>.Failure(new Error("Alarm.NotFound", "Alarm not found or access denied."));
            
            return Result<SmartAlarmDto>.Success(ToDto(alarm));
        }

        /// <inheritdoc/>
        public async Task<Result<SmartAlarmDto>> CreateAlarmAsync(Guid userId, SmartAlarmRequest request)
        {
            var isOwned = await _alarmRepo.IsOwnedDeviceAsync(userId, request.DeviceId);
            if (!isOwned)
                return Result<SmartAlarmDto>.Failure(new Error("Device.Unauthorized", "Device not found or does not belong to your account."));

            var alarm = new SmartAlarm
            {
                AlarmId = Guid.NewGuid().ToString(),
                DeviceId = request.DeviceId,
                Hour = request.Hour,
                Minute = request.Minute,
                WakeUpMessage = string.IsNullOrWhiteSpace(request.WakeUpMessage) ? null : request.WakeUpMessage.Trim(),
                IsEnabled = request.IsEnabled,
                UseVoice = request.UseVoice,
                AudioUrl = request.AudioUrl,
                RepeatMode = request.RepeatMode ?? "Once",
                RepeatCount = request.RepeatCount,
                CreatedAt = DateTime.UtcNow
            };

            await _alarmRepo.AddAsync(alarm);

            var created = await _alarmRepo.GetByIdForUserAsync(userId, alarm.AlarmId);
            if (created == null)
                return Result<SmartAlarmDto>.Failure(new Error("Alarm.PersistenceError", "Alarm created but failed to reload."));

            return Result<SmartAlarmDto>.Success(ToDto(created));
        }

        /// <inheritdoc/>
        public async Task<Result<SmartAlarmDto>> UpdateAlarmAsync(Guid userId, string alarmId, UpdateSmartAlarmRequest request)
        {
            var alarm = await _alarmRepo.GetByIdForUserAsync(userId, alarmId);
            if (alarm == null)
                return Result<SmartAlarmDto>.Failure(new Error("Alarm.NotFound", "Alarm not found."));

            alarm.Hour = request.Hour;
            alarm.Minute = request.Minute;
            alarm.WakeUpMessage = string.IsNullOrWhiteSpace(request.WakeUpMessage) ? null : request.WakeUpMessage.Trim();
            alarm.IsEnabled = request.IsEnabled;
            alarm.UseVoice = request.UseVoice;
            alarm.AudioUrl = request.AudioUrl;
            if (request.RepeatMode != null) alarm.RepeatMode = request.RepeatMode;
            if (request.RepeatCount != null) alarm.RepeatCount = request.RepeatCount.Value;

            await _alarmRepo.UpdateAsync(alarm);
            return Result<SmartAlarmDto>.Success(ToDto(alarm));
        }

        /// <inheritdoc/>
        public async Task<Result<SmartAlarmDto>> ToggleAlarmAsync(Guid userId, string alarmId)
        {
            var alarm = await _alarmRepo.GetByIdForUserAsync(userId, alarmId);
            if (alarm == null)
                return Result<SmartAlarmDto>.Failure(new Error("Alarm.NotFound", "Alarm not found."));

            alarm.IsEnabled = !alarm.IsEnabled;
            await _alarmRepo.UpdateAsync(alarm);

            return Result<SmartAlarmDto>.Success(ToDto(alarm));
        }

        /// <inheritdoc/>
        public async Task<Result> DeleteAlarmAsync(Guid userId, string alarmId)
        {
            var alarm = await _alarmRepo.GetByIdForUserAsync(userId, alarmId);
            if (alarm == null)
                return Result.Failure(new Error("Alarm.NotFound", "Alarm not found."));

            await _alarmRepo.DeleteAsync(alarm);
            return Result.Success();
        }

        public async Task ProcessPendingAlarmsAsync()
        {
            var now = DateTime.UtcNow.AddHours(7);
            var nextMinute = now.AddMinutes(1);

            // 1. FIRE PHASE: Find alarms due RIGHT NOW
            var dueAlarms = await _alarmRepo.GetDueAlarmsAsync(now.Hour, now.Minute);

            foreach (var alarm in dueAlarms)
            {
                var serial = alarm.Device?.SerialNumber;
                if (string.IsNullOrWhiteSpace(serial)) continue;

                var normSerial = LLMHub.NormalizeSerial(serial);
                _logger.LogInformation("[SmartAlarm] [FIRE] Checking device {Serial}: UseVoice={UseVoice}, AudioUrl='{AudioUrl}'", 
                    serial, alarm.UseVoice, alarm.AudioUrl);
                
                // Check if we have a PREPARED text in cache
                var cachedText = await _cache.GetStringAsync($"smart_alarm_prep:{normSerial}");
                if (!string.IsNullOrEmpty(cachedText))
                {
                    _logger.LogInformation("[SmartAlarm] [FIRE] Using PREPARED AI text for '{Serial}'", serial);
                    var provider = alarm.Device?.Profile?.PreferredTtsProvider ?? Constants.TtsProviders.Gcp;
                    var voiceId = alarm.Device?.Profile?.PreferredVoiceId ?? Constants.Voices.DefaultVietnamese;

                    var payload = new Dictionary<string, object?>
                    {
                        { "target_mac", normSerial },
                        { "action", "trigger_alarm" },
                        { "text", cachedText },
                        { "repeat_mode", alarm.RepeatMode },
                        { "repeat_count", alarm.RepeatCount },
                        { "provider", provider },
                        { "voice_id", voiceId },
                        { "language_code", "vi-VN" }
                    };

                    if (provider == "ElevenLabs" || provider == "ElevenLabsTTS")
                    {
                        payload.Add("model_id", "eleven_turbo_v2_5");
                        payload.Add("language_code", "vi");
                        payload.Add("voice_settings", new { stability = 0.4, similarity_boost = 0.8, style = 0.35 });
                    }

                    await _hubContext.Clients.All.SendAsync("BearResponse", payload);
                    
                    // Clear cache after use
                    await _cache.RemoveAsync($"smart_alarm_prep:{normSerial}");
                    continue;
                }

                // Normal logic
                // Normal logic
                if (alarm.UseVoice)
                {
                    var wakeUpText = string.IsNullOrWhiteSpace(alarm.WakeUpMessage)
                        ? "Chào buổi sáng! Đến giờ thức dậy rồi, bé vươn vai nào!"
                        : alarm.WakeUpMessage;

                    _logger.LogInformation("[SmartAlarm] [FIRE] {Type} alarm for device '{Serial}'", 
                        (alarm.AudioUrl == "GCS" ? "SMART" : "VOICE"), serial);

                    var provider = alarm.Device?.Profile?.PreferredTtsProvider 
                                   ?? alarm.Device?.ParentUser?.PreferredTtsProvider 
                                   ?? Constants.TtsProviders.Gcp;
                    var voiceId = alarm.Device?.Profile?.PreferredVoiceId 
                                 ?? alarm.Device?.ParentUser?.PreferredVoiceId 
                                 ?? (provider == Constants.TtsProviders.Gcp ? Constants.Voices.DefaultVietnamese : "");

                    var payload = new Dictionary<string, object?>
                    {
                        { "target_mac", normSerial },
                        { "action", "trigger_alarm" },
                        { "text", wakeUpText },
                        { "repeat_mode", alarm.RepeatMode },
                        { "repeat_count", alarm.RepeatCount },
                        { "provider", provider },
                        { "voice_id", voiceId },
                        { "language_code", "vi-VN" }
                    };

                    if (provider == "ElevenLabs" || provider == "ElevenLabsTTS")
                    {
                        payload.Add("model_id", "eleven_turbo_v2_5");
                        payload.Add("language_code", "vi");
                        payload.Add("voice_settings", new { stability = 0.4, similarity_boost = 0.8, style = 0.35 });
                    }

                    await _hubContext.Clients.All.SendAsync("BearResponse", payload);
                }
                else
                {
                    // AUDIO/MUSIC ALARM
                    var audioUrl = alarm.AudioUrl;
                    
                    if (string.IsNullOrWhiteSpace(audioUrl) || audioUrl == "GCS")
                    {
                        audioUrl = "https://assets.mixkit.co/active_storage/sfx/2869/2869-preview.mp3"; 
                    }
                    else if (!audioUrl.StartsWith("http"))
                    {
                        _logger.LogInformation("[SmartAlarm] [FIRE] Resolving GCS path for '{Serial}': {Path}", serial, audioUrl);
                        using var scope = _scopeFactory.CreateScope();
                        var mediaService = scope.ServiceProvider.GetRequiredService<IMediaService>();
                        var resolved = await mediaService.ResolveMediaAsync(alarm.Device!, audioUrl, MediaType.Music);
                        if (resolved.Status == "approved") {
                            audioUrl = resolved.StreamingURL;
                        }
                    }

                    _logger.LogInformation("[SmartAlarm] [FIRE] AUDIO alarm for device '{Serial}' | URL: {Url}", serial, audioUrl);

                    var provider = alarm.Device?.Profile?.PreferredTtsProvider ?? Constants.TtsProviders.Gcp;
                    var voiceId = alarm.Device?.Profile?.PreferredVoiceId ?? Constants.Voices.DefaultVietnamese;

                    var payload = new Dictionary<string, object?>
                    {
                        { "target_mac", normSerial },
                        { "action", "stream_gcs" },
                        { "url", audioUrl },
                        { "repeat_mode", alarm.RepeatMode },
                        { "repeat_count", alarm.RepeatCount },
                        { "provider", provider },
                        { "voice_id", voiceId },
                        { "language_code", "vi-VN" }
                    };

                    if (provider == "ElevenLabs" || provider == "ElevenLabsTTS")
                    {
                        payload.Add("model_id", "eleven_turbo_v2_5");
                        payload.Add("language_code", "vi");
                        payload.Add("voice_settings", new { stability = 0.4, similarity_boost = 0.8, style = 0.35 });
                    }

                    await _hubContext.Clients.All.SendAsync("BearResponse", payload);
                }
            }

            // 2. PREP PHASE: Find alarms due in ONE MINUTE (T-1)
            var prepAlarms = await _alarmRepo.GetDueAlarmsAsync(nextMinute.Hour, nextMinute.Minute);

            foreach (var alarm in prepAlarms)
            {
                var serial = alarm.Device?.SerialNumber;
                if (string.IsNullOrWhiteSpace(serial)) continue;
                var normSerial = LLMHub.NormalizeSerial(serial);

                _logger.LogInformation("[SmartAlarm] [PREP] Device {Serial}: UseVoice={UseVoice}, AudioUrl='{AudioUrl}'", 
                    serial, alarm.UseVoice, alarm.AudioUrl);
                
                _logger.LogInformation("[SmartAlarm] [PREP] Warm-up and AI Generation for device '{Serial}'", serial);
                
                // Notify Bear to "Wake up" its internal components (Bridge ping)
                await _hubContext.Clients.All.SendAsync("BearResponse", new
                {
                    target_mac = normSerial,
                    action = "prep_alarm",
                    message = "Chuẩn bị báo thức thông minh"
                });

                // Generate AI greeting in background
                if (alarm.UseVoice)
                {
                    _ = Task.Run(async () => {
                        using var scope = _scopeFactory.CreateScope();
                        var scopedAiService = scope.ServiceProvider.GetRequiredService<IAIService>();
                        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        try {
                            // Re-fetch device/profile to avoid context issues
                            var device = await db.Devices.Include(d => d.Profile)
                                              .FirstOrDefaultAsync(d => d.DeviceId == alarm.DeviceId);
                            
                            string childName = device?.Profile?.Name ?? "bé";
                            string personality = device?.Profile?.Personality ?? "vui vẻ, năng động";
                            
                            string prompt = $"Bạn là Gấu Bông AI thân thiện. Hãy tạo 1 lời chào buổi sáng ngắn gọn (dưới 40 từ) để đánh thức {childName} dậy. " +
                                            $"Phong cách: {personality}. Nội dung nên có: Chào buổi sáng, nhắc bé dậy tập thể dục hoặc đánh răng, " +
                                            $"và một câu chúc ngày mới tốt lành. Đừng dùng ký tự đặc biệt.";

                            _logger.LogInformation("[SmartAlarm] [PREP] Calling AI for '{Serial}'...", serial);
                            var aiResponse = await scopedAiService.Process(prompt, device);
                            
                            if (!string.IsNullOrWhiteSpace(aiResponse)) {
                                await _cache.SetStringAsync($"smart_alarm_prep:{normSerial}", aiResponse.Trim(), new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions {
                                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                                });
                                _logger.LogInformation("[SmartAlarm] [PREP] AI Text cached for '{Serial}': {Text}", serial, aiResponse);
                            }
                        } catch (Exception ex) {
                            _logger.LogError(ex, "[SmartAlarm] [PREP] AI Generation failed for '{Serial}'", serial);
                        }
                    });
                }
            }
        }

        private static SmartAlarmDto ToDto(SmartAlarm alarm)
        {
            return new SmartAlarmDto
            {
                AlarmId = alarm.AlarmId,
                DeviceId = alarm.DeviceId,
                DeviceSerialNumber = alarm.Device?.SerialNumber ?? string.Empty,
                Hour = alarm.Hour,
                Minute = alarm.Minute,
                WakeUpMessage = alarm.WakeUpMessage,
                IsEnabled = alarm.IsEnabled,
                UseVoice = alarm.UseVoice,
                AudioUrl = alarm.AudioUrl,
                RepeatMode = alarm.RepeatMode,
                RepeatCount = alarm.RepeatCount,
                CreatedAt = alarm.CreatedAt
            };
        }
    }
}
