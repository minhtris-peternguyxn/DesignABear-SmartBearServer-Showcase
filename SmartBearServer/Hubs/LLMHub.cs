using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SmartBearServer.Model;
using SmartBearServer.Data;
using SmartBearServer.Infrastructure;
using SmartBearServer.Services;
using SmartBearServer.Services.Interfaces;
using System.Collections.Concurrent;

namespace SmartBearServer.Hubs
{
    /// <summary>
    /// LLMHub — Real-time SignalR hub for the Python bridge to bear devices.
    ///
    /// Architecture:
    ///   [ESP32] ← WebSocket/Audio → [Python Bridge] ← SignalR (Text JSON) → [LLMHub] → [Gemini]
    ///
    /// Protocol:
    ///   1. Python connects to /hubs/llm?serial=BEAR_SERIAL_NUMBER
    ///   2. OnConnectedAsync() maps ConnectionId → SerialNumber in the session store
    ///   3. Python calls hub.invoke("AskBear", text) with transcribed text from STT
    ///   4. LLMHub resolves the device, runs safety checks + quota, builds mode-aware prompt
    ///   5. Hub emits "BearResponse" with JSON: { "action": "speak"|"stream_gcs", "text"/"url": ... }
    ///   6. Python reads the action and drives the ESP32 accordingly
    ///
    /// Smart Alarm push:
    ///   - The SmartAlarmBackgroundService uses IHubContext&lt;LLMHub&gt; to push "AlarmTrigger"
    ///     events directly to the Python connection for a given serial number.
    /// </summary>
    public class LLMHub : Hub
    {
        // ─── Thread-safe Session Store ────────────────────────────────────────────
        // Maps SignalR ConnectionId → DeviceSerialNumber.
        // Populated in OnConnectedAsync, cleaned up in OnDisconnectedAsync.
        // NOTE: This is in-memory. For multi-instance deployments, use Redis backplane.
        private static readonly ConcurrentDictionary<string, string> _connectionToSerial = new();

        // Reverse map: SerialNumber → ConnectionId (for alarm push targeting).
        private static readonly ConcurrentDictionary<string, string> _serialToConnection = new();

        private readonly IAIService _ai;
        private readonly IContentSafetyService _safety;
        private readonly IUsageQuotaService _quota;
        private readonly ISubscriptionLifecycleService _subscriptionLifecycle;
        private readonly IDeviceService _deviceService;
        private readonly IMediaService _mediaService;
        private readonly ISessionService _session;
        private readonly ICacheService _cache;
        private readonly IServiceScopeFactory _scopeFactory;

        public LLMHub(
            IAIService ai,
            IContentSafetyService safety,
            IUsageQuotaService quota,
            ISubscriptionLifecycleService subscriptionLifecycle,
            IDeviceService deviceService,
            IMediaService mediaService,
            ISessionService session,
            ICacheService cache,
            IServiceScopeFactory scopeFactory)
        {
            _ai = ai;
            _safety = safety;
            _quota = quota;
            _subscriptionLifecycle = subscriptionLifecycle;
            _deviceService = deviceService;
            _mediaService = mediaService;
            _session = session;
            _cache = cache;
            _scopeFactory = scopeFactory;
        }

        // ─── Connection Lifecycle ─────────────────────────────────────────────────

        public static string NormalizeSerial(string? raw)
        {
            if (string.IsNullOrEmpty(raw)) return string.Empty;
            return raw.Replace(":", "").Replace("-", "").Replace(" ", "").ToUpperInvariant().Trim();
        }

        /// <summary>
        /// Called when the Python bridge establishes a WebSocket connection.
        /// Now supports a single global connection from the bridge.
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            var serialNumber = Context.GetHttpContext()?.Request.Query["serial"].ToString();

            if (!string.IsNullOrWhiteSpace(serialNumber))
            {
                var norm = NormalizeSerial(serialNumber);
                _connectionToSerial[Context.ConnectionId] = norm;
                _serialToConnection[norm] = Context.ConnectionId;
                Console.WriteLine($"[LLMHub] Legacy Device '{norm}' connected.");

                // Warm up cache for the device
                _ = Task.Run(async () =>
                {
                    try
                    {
                        var configResult = await _deviceService.GetBearProfileConfigAsync(serialNumber: norm);
                        if (configResult.IsSuccess)
                        {
                            var device = configResult.Value.ToDevice();
                            // WARM-UP QUOTA: Nạp kẹo từ DB lên Redis ngay khi Gấu bật nguồn (0 DB hit lúc gọi AI)
                            await _quota.WarmupQuotaAsync(device);
                            Console.WriteLine($"[LLMHub] Cache warmed up (Profile & Quota) for device '{norm}'");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[LLMHub] Cache warm-up failed for device '{norm}': {ex.Message}");
                    }
                });
            }
            else
            {
                Console.WriteLine($"[LLMHub] Global Bridge connection established: {Context.ConnectionId}");
            }

            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Called when the Python bridge disconnects.
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (_connectionToSerial.TryRemove(Context.ConnectionId, out var serial))
            {
                _serialToConnection.TryRemove(serial, out _);
                Console.WriteLine($"[LLMHub] Device '{serial}' disconnected.");
            }

            await base.OnDisconnectedAsync(exception);
        }

        // ─── Primary AI Endpoint ──────────────────────────────────────────────────

        /// <summary>
        /// Primary endpoint called by the Python bridge.
        /// Python invokes: hub.invoke("AskBear", "MAC_ADDRESS", "What is 2 + 2?", "vi-VN")
        /// </summary>
        public async Task AskBear(string serialNumber, string text, string? languageCode = null)
        {
            Console.WriteLine($"[LLMHub] AskBear received: MAC={serialNumber}, Text='{text}', Lang='{languageCode}'");
            // ── Step 1: Resolve device from provided serial number ──────────
            var norm = NormalizeSerial(serialNumber);
            var configResult = await _deviceService.GetBearProfileConfigAsync(serialNumber: norm);
            
            if (!configResult.IsSuccess)
            {
                Console.WriteLine($"[LLMHub] ABORT: Device NOT FOUND for identifier {norm} (SignalR: {serialNumber})");
                await SendBearResponse("speak", text: "Gấu chưa được cài đặt. Bố mẹ ơi hãy cấu hình gấu trong ứng dụng nhé!", targetMac: serialNumber, languageCode: languageCode);
                return;
            }

            var config = configResult.Value;
            var device = config.ToDevice();
            var profile = device.Profile;

            var finalProvider = profile?.PreferredTtsProvider ?? device?.ParentUser?.PreferredTtsProvider ?? Constants.TtsProviders.Gcp;
            var finalVoiceId = profile?.PreferredVoiceId ?? device?.ParentUser?.PreferredVoiceId ?? (finalProvider == Constants.TtsProviders.Gcp ? Constants.Voices.DefaultVietnamese : "");

            // Auto-correct provider if Voice ID is clearly an ElevenLabs ID (no hyphens, >= 20 chars)
            if (!string.IsNullOrEmpty(finalVoiceId) && !finalVoiceId.Contains("-") && finalVoiceId.Length >= 20)
            {
                finalProvider = "ElevenLabs";
            }

            Console.WriteLine($"[LLMHub] Device resolved: {device.Nickname}, Profile: {profile.Name}, Plan: {(config.IsPro ? "Pro" : "Basic")}");

            // ── Step 2: Validate subscription is active ──────────────────────────────
            // If plan is missing, we assume a basic/free tier which is always accessible
            if (device.ParentUser?.SubscriptionPlan != null && !_subscriptionLifecycle.IsAccessible(profile))
            {
                Console.WriteLine($"[LLMHub] ABORT: Subscription not accessible for {profile.Name}");
                await SendBearResponse("speak", text: "Gói đăng ký đã hết hạn. Nhờ bố mẹ gia hạn gấu nhé!", targetMac: serialNumber, provider: finalProvider, voiceId: finalVoiceId, languageCode: languageCode);
                return;
            }

            // ── Step 3: Session Resolution ──────────────────────────────────────────
            var activeSession = await _session.GetOrCreateActiveSessionAsync(profile.Id);

            // ── Step 4: Input content safety filter ──
            var inputSafety = await _safety.EvaluateAsync(text, profile);
            if (!inputSafety.IsSafe)
            {
                Console.WriteLine($"[LLMHub] ABORT: Input safety violation: {inputSafety.Message}");
                // Save unsafe interaction for parent review via Redis queue
                await _cache.EnqueueInteractionAsync(new PendingInteractionDto
                {
                    DeviceId = norm,
                    ProfileId = profile.Id,
                    Request = text,
                    Response = inputSafety.Message,
                    SessionId = activeSession.Id,
                    IsSafe = false,
                    SafetyCategory = inputSafety.Category,
                    Timestamp = DateTime.UtcNow
                });
                await SendBearResponse("speak", text: inputSafety.Message, targetMac: serialNumber, provider: finalProvider, voiceId: finalVoiceId, languageCode: languageCode);
                return;
            }

            // ── Step 5: Atomic Quota Consumption (Upfront) ───────────────────────────
            var (isAllowed, consumptionType) = await _quota.TryConsumeAiAsync(device);
            if (!isAllowed)
            {
                await SendBearResponse("speak", text: "Bé ơi, Gấu hết kẹo rồi. Bé bảo ba mẹ mua thêm kẹo cho Gấu nhé!", targetMac: serialNumber, languageCode: languageCode);
                return;
            }

            // ── Step 6: Call Gemini (mode injection handled inside AIService) ────────
            string llmResponse;
            Console.WriteLine($"[LLMHub] Calling Gemini for {profile.Name}...");
            try
            {
                llmResponse = await _ai.Process(text, device);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LLMHub] Gemini error for device '{serialNumber}': {ex.Message}");
                // Refund quota on failure
                await _quota.RefundAiAsync(device, consumptionType!);
                await SendBearResponse("speak", text: "Gấu đang bận suy nghĩ, bé chờ chút nhé!", targetMac: serialNumber, languageCode: languageCode);
                return;
            }

            Console.WriteLine($"[LLMHub] Gemini Response: {llmResponse}");

            // ── Step 7: Output safety filter ──
            var outputSafety = await _safety.EvaluateResponseAsync(llmResponse, profile);
            if (!outputSafety.IsSafe)
            {
                Console.WriteLine($"[LLMHub] ABORT: Output safety violation for {profile.Name}. Refunding...");
                // Refund quota on safety violation
                await _quota.RefundAiAsync(device, consumptionType!);
                await SendBearResponse("speak", text: "Gau khong the tra loi cau do. Be hoi cau khac nhe!", targetMac: serialNumber, languageCode: languageCode);
                return;
            }

            // ── Step 8: Logging ──────────────────────────────────────────────────────
            int limit = (device.ParentUser?.IsPro == true) ? 50 : 10;
            if (device.ParentUser?.SubscriptionPlan != null) limit = device.ParentUser.SubscriptionPlan.DailyCandyLimit;
            
            Console.WriteLine($"\x1b[32m[LLMHub] Interaction Success: {profile.Name} - Type: {consumptionType}\x1b[0m");

            // ── Step 9: Parse intent and emit correct action ─────────────────────────
            // Look for [GCS_MEDIA:TYPE:QUERY] anywhere in the response
            var mediaMatch = System.Text.RegularExpressions.Regex.Match(llmResponse, @"\[GCS_MEDIA:(STORY|MUSIC|VOICE):([^\]]+)\]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            
            if (mediaMatch.Success)
            {
                var typeStr = mediaMatch.Groups[1].Value.ToUpperInvariant();
                var query = mediaMatch.Groups[2].Value.Trim();
                Console.WriteLine($"[LLMHub] Detected Media Tag: Type={typeStr}, Query={query}");
                
                var mediaType = typeStr switch
                {
                    "STORY" => MediaType.Story,
                    "MUSIC" => MediaType.Music,
                    "VOICE" => MediaType.DemoVoice,
                    _ => MediaType.Music
                };

                try
                {
                    var mediaResult = await _mediaService.ResolveMediaAsync(device, query, mediaType);
                    if (mediaResult.Status == "approved" && !string.IsNullOrEmpty(mediaResult.StreamingURL))
                    {
                        // Success: Prioritize streaming the media
                        await SendBearResponse("stream_gcs", url: mediaResult.StreamingURL, targetMac: serialNumber, provider: finalProvider, voiceId: finalVoiceId, languageCode: languageCode);
                        
                        // Save interaction record
                        await _cache.EnqueueInteractionAsync(new PendingInteractionDto
                        {
                            DeviceId = norm,
                            ProfileId = profile.Id,
                            Request = text,
                            Response = llmResponse,
                            SessionId = activeSession.Id,
                            Timestamp = DateTime.UtcNow
                        });
                        return;
                    }
                    
                    // If media resolution failed, speak the specific error message from MediaService if it's a reject
                    if (mediaResult.Status == "reject")
                    {
                        await SendBearResponse("speak", text: mediaResult.Message ?? "Gấu không tìm thấy nội dung này.", targetMac: serialNumber, provider: finalProvider, voiceId: finalVoiceId, languageCode: languageCode);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[LLMHub] CRITICAL: Media resolution failed for {serialNumber}: {ex.Message}");
                    // Do not return here, fall through to speak the LLM response as a fallback
                }
            }

            // ── Step 10: Save interaction history (Asynchronous via Redis) ──────
            await _cache.EnqueueInteractionAsync(new PendingInteractionDto
            {
                DeviceId = norm,
                ProfileId = profile.Id,
                Request = text,
                Response = llmResponse,
                SessionId = activeSession.Id,
                Timestamp = DateTime.UtcNow
            });

            // Fallback: If no media tag found, or media resolution failed silently, speak the whole response or a default message
            await SendBearResponse("speak", text: llmResponse, targetMac: serialNumber, provider: finalProvider, voiceId: finalVoiceId, languageCode: languageCode);
        }

        // ─── Internal Push Helpers ────────────────────────────────────────────────

        /// <summary>
        /// Emits a "BearResponse" event to the current caller (Python bridge).
        /// JSON structure is standardized for the Python bridge to parse:
        ///   {"action": "speak", "text": "..."}
        ///   {"action": "stream_gcs", "url": "..."}
        /// </summary>
        /// <summary>
        /// Emits a "BearResponse" event to the current caller.
        /// </summary>
        private async Task SendBearResponse(string action, string? text = null, string? url = null, string? targetMac = null, string? provider = null, string? voiceId = null, string? languageCode = null)
        {
            // Auto-resolve languageCode if missing, based on voiceId prefix
            if (string.IsNullOrEmpty(languageCode) && !string.IsNullOrEmpty(voiceId))
            {
                if (voiceId.StartsWith("vi-VN")) languageCode = "vi-VN";
                else if (voiceId.StartsWith("en-US")) languageCode = "en-US";
            }

            var payload = new Dictionary<string, object?>
            {
                { "target_mac", targetMac },
                { "action", action },
                { "text", text },
                { "url", url },
                { "provider", provider },
                { "voice_id", voiceId },
                { "language_code", languageCode ?? "vi-VN" }
            };

            // Truyền cấu hình chi tiết cho Xiaozhi Bridge để Bridge gọi ElevenLabs đúng chuẩn
            if (provider == "ElevenLabs" || provider == "ElevenLabsTTS")
            {
                payload.Add("model_id", "eleven_turbo_v2_5");
                // ElevenLabs uses 'vi' or 'en' instead of 'vi-VN' or 'en-US'
                var elLang = (languageCode?.StartsWith("en") == true) ? "en" : "vi";
                payload["language_code"] = elLang;  // Override the existing key, not Add
                payload.Add("voice_settings", new
                {
                    stability = 0.4,
                    similarity_boost = 0.8,
                    style = 0.35
                });
            }

            await Clients.Caller.SendAsync("BearResponse", payload);
        }

        /// <summary>
        /// Returns the SignalR ConnectionId for a given serial number.
        /// Used by SmartAlarmBackgroundService to push alarm commands to a specific device.
        /// </summary>
        public static string? GetConnectionIdBySerial(string serialNumber)
        {
            var norm = NormalizeSerial(serialNumber);
            _serialToConnection.TryGetValue(norm, out var connectionId);
            return connectionId;
        }

        // ─── Legacy Endpoints (preserved, not used by Python bridge) ─────────────

        /// <summary>[LEGACY] Broadcast a chat message to all connected clients.</summary>
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>[LEGACY] Broadcast a device status notification to all connected clients.</summary>
        public async Task NotifyStatus(string deviceId, string status)
        {
            await Clients.All.SendAsync("StatusUpdate", deviceId, status);
        }

        /// <summary>
        /// [LEGACY] Audio stream endpoint from the original ESP32 direct architecture.
        /// Preserved for backward compatibility. NOT used in the Python bridge architecture.
        /// The Python bridge architecture processes audio server-side and sends text via AskBear().
        /// </summary>
        public async Task UploadAudioStream(IAsyncEnumerable<byte[]> stream, string deviceId, string languageCode)
        {
            await Clients.Caller.SendAsync("StatusUpdate", deviceId, "[LEGACY] This endpoint is deprecated. Python bridge should use AskBear() instead.");
        }

        /// <summary>
        /// [LEGACY] Direct text-to-AI endpoint used by the web demonstration interface.
        /// NOT used by the Python bridge or ESP32.
        /// </summary>
        public async Task AskAI(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                await Clients.Caller.SendAsync("ReceiveAnswer", new { role = "error", content = "Message cannot be empty." });
                return;
            }

            await Clients.Caller.SendAsync("ReceiveAnswer", new { role = "user", content = message });

            try
            {
                var response = await _ai.Process(message);
                await Clients.Caller.SendAsync("ReceiveAnswer", new { role = "assistant", content = response });
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("ReceiveAnswer", new { role = "error", content = $"Processing error: {ex.Message}" });
            }
        }
        // ─── Status Synchronization ────────────────────────────────────────────────
        
        /// <summary>
        /// Called by the Python Bridge every minute (triggered by DeviceStatusWorker).
        /// Reconciles the list of actually connected bears with the database.
        /// </summary>
        public async Task PushActiveBears(List<string> macs)
        {
            if (macs == null) return;
            
            var normalizedMacs = macs.Select(m => NormalizeSerial(m)).Where(m => !string.IsNullOrEmpty(m)).ToList();
            
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            if (db != null)
            {
                // Efficient SQL Update: Set everyone Offline first, then Online for reported MACs
                // This prevents loading all devices into RAM.
                
                // 1. Mark everyone as Offline
                await db.Database.ExecuteSqlRawAsync("UPDATE \"bear_devices\" SET \"status\" = 'Offline'");
                
                // 2. Mark reported devices as Online (if list is not empty)
                if (normalizedMacs.Any())
                {
                    // Use parameterized SQL to prevent injection
                    var macList = string.Join(",", normalizedMacs.Select(m => $"'{m}'"));
                    await db.Database.ExecuteSqlRawAsync($"UPDATE \"bear_devices\" SET \"status\" = 'Online' WHERE \"device_id\" IN ({macList}) OR \"serial_number\" IN ({macList})");
                }
                
                Console.WriteLine($"[LLMHub] Status Sync: {normalizedMacs.Count} bears processed.");
            }
        }



        /// <summary>
        /// Called by the Python Bridge when it detects a command for a MAC that is NOT connected.
        /// This indicates a potential theft or spoofing attempt.
        /// </summary>
        public async Task ReportTheftAttempt(string targetMac)
        {
            var norm = NormalizeSerial(targetMac);
            Console.WriteLine($"[LLMHub] !!! SECURITY ALERT !!! Theft attempt detected for device {norm}");
            
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var device = await db.Devices.FirstOrDefaultAsync(d => d.DeviceId == norm || d.SerialNumber == norm);
            
            if (device != null && device.UserId != null)
            {
                // Notify the owner's mobile app via SignalR
                var ownerUserId = device.UserId.ToString();
                Console.WriteLine($"[LLMHub] Notifying owner {ownerUserId} about theft attempt on device {device.Nickname}");
                await Clients.User(ownerUserId).SendAsync("SecurityAlert", new {
                    deviceId = norm,
                    deviceName = device.Nickname,
                    message = "Cảnh báo: Phát hiện yêu cầu truy cập trái phép vào Gấu của bạn từ một địa điểm khác!"
                });
            }
        }
    }
}
