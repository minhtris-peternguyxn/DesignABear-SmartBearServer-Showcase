using SmartBearServer.Services.Interfaces;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Model.DTOs;
using SmartBearServer.Model;
using SmartBearServer.Hubs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SmartBearServer.Services.Implementations
{
    /// <summary>
    /// Implementation of IDeviceService for managing device lifecycles and child profiles.
    /// Follows the 3-layer architecture by delegating data access to repositories.
    /// </summary>
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepo;
        private readonly IChildProfileRepository _profileRepo;
        private readonly ICacheService _cache;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private static readonly System.Net.Http.HttpClient _httpClient = new();

        public DeviceService(
            IDeviceRepository deviceRepo, 
            IChildProfileRepository profileRepo, 
            ICacheService cache,
            Microsoft.Extensions.Configuration.IConfiguration config)
        {
            _deviceRepo = deviceRepo;
            _profileRepo = profileRepo;
            _cache = cache;
            _config = config;
        }

        /// <inheritdoc/>
        public async Task<Result<List<DeviceDto>>> GetDevicesByUserAsync(Guid userId)
        {
            // Logic: Fetch devices and their daily usage to calculate remaining "candies"
            var devices = await _deviceRepo.GetDevicesWithDetailsByUserAsync(userId);

            var result = devices.Select(d =>
            {
                var dto = DeviceDto.From(d);
                // The new system uses DailyCandyBalance and PurchasedCandies from the DTO's From method
                return dto;
            }).ToList();

            return Result<List<DeviceDto>>.Success(result);
        }

        /// <inheritdoc/>
        public async Task<Result<Device>> GetBySerialNumberAsync(string serialNumber)
        {
            var norm = LLMHub.NormalizeSerial(serialNumber);
            return await GetCachedDeviceAsync($"device:config:sn:{norm}", () => _deviceRepo.GetBySerialNumberAsync(norm));
        }

        /// <inheritdoc/>
        public async Task<Result<Device>> GetByDeviceIdAsync(string deviceId)
        {
            var norm = LLMHub.NormalizeSerial(deviceId);
            return await GetCachedDeviceAsync($"device:config:id:{norm}", () => _deviceRepo.GetByDeviceIdAsync(norm));
        }

        /// <inheritdoc/>
        public async Task<Result<BearProfileConfig>> GetBearProfileConfigAsync(string? serialNumber = null, string? deviceId = null)
        {
            string identifier = serialNumber ?? deviceId ?? string.Empty;
            if (string.IsNullOrEmpty(identifier)) return Result<BearProfileConfig>.Failure(new Error("BadRequest", "Identifier is required."));

            var norm = LLMHub.NormalizeSerial(identifier);
            var cacheKey = $"bear:profile:config:{norm}";

            try
            {
                var cached = await _cache.GetAsync<BearProfileConfig>(cacheKey);
                if (cached != null) return Result<BearProfileConfig>.Success(cached);

                // Fallback to DB
                Device? device = null;
                if (!string.IsNullOrEmpty(serialNumber)) device = await _deviceRepo.GetBySerialNumberAsync(norm);
                else if (!string.IsNullOrEmpty(deviceId)) device = await _deviceRepo.GetByDeviceIdAsync(norm);

                if (device == null) return Result<BearProfileConfig>.Failure(new Error("Device.NotFound", "Device not found."));

                // IMPORTANT: Ensure interactions are loaded for the agent!
                // Since our repo might not have them, we might need a separate load or update the repo.
                // But ChildProfileRepository already includes them.
                if (device.Profile != null && (device.Profile.Interactions == null || !device.Profile.Interactions.Any()))
                {
                    // Optionally reload profile with interactions if needed, but repo usually does it.
                }

                var config = BearProfileConfig.FromDevice(device);
                await _cache.SetAsync(cacheKey, config, TimeSpan.FromMinutes(15));
                
                return Result<BearProfileConfig>.Success(config);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DeviceService] BearProfileConfig cache error: {ex.Message}");
                return Result<BearProfileConfig>.Failure(new Error("InternalError", ex.Message));
            }
        }

        private async Task<Result<Device>> GetCachedDeviceAsync(string cacheKey, Func<Task<Device?>> fallback)
        {
            try
            {
                var cached = await _cache.GetAsync<Device>(cacheKey);
                if (cached != null) return Result<Device>.Success(cached);

                var device = await fallback();
                if (device == null) return Result<Device>.Failure(new Error("Device.NotFound", "Device not found."));

                await _cache.SetAsync(cacheKey, device, TimeSpan.FromMinutes(30));
                
                return Result<Device>.Success(device);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DeviceService] Cache error for {cacheKey}: {ex.Message}");
                var device = await fallback();
                return device != null 
                    ? Result<Device>.Success(device) 
                    : Result<Device>.Failure(new Error("Device.NotFound", "Device not found."));
            }
        }

        private async Task InvalidateDeviceCacheAsync(string? serialNumber, string? deviceId = null)
        {
            if (!string.IsNullOrEmpty(serialNumber))
            {
                var norm = LLMHub.NormalizeSerial(serialNumber);
                await _cache.RemoveAsync($"device:config:sn:{norm}");
                await _cache.RemoveAsync($"bear:profile:config:{norm}");
            }
            if (!string.IsNullOrEmpty(deviceId))
            {
                var norm = LLMHub.NormalizeSerial(deviceId);
                await _cache.RemoveAsync($"device:config:id:{norm}");
                await _cache.RemoveAsync($"bear:profile:config:{norm}");
            }
        }

        /// <inheritdoc/>
        public async Task<Result> UnpairDeviceAsync(Guid userId, string deviceId)
        {
            var device = await _deviceRepo.GetByIdAsync(deviceId);
            if (device == null || device.UserId != userId)
                return Result.Failure(new Error("Device.NotFound", "Device not found or permission denied."));

            // Dissociate user and profile
            device.ProfileId = null;
            device.UserId = null;
            device.Nickname = null;
            device.Status = "Inactive";
            
            await _deviceRepo.UpdateAsync(device);
            await InvalidateDeviceCacheAsync(device.SerialNumber, device.DeviceId);
            return Result.Success();
        }

        /// <inheritdoc/>
        public async Task<Result<Device>> AssignProfileAsync(Guid userId, AssignProfileRequest request)
        {
            var device = await _deviceRepo.GetByIdAsync(request.DeviceId);
            if (device == null || device.UserId != userId)
                return Result<Device>.Failure(new Error("Device.NotFound", "Device not found or permission denied."));

            var profile = await _profileRepo.GetByIdAsync(request.ProfileId);
            if (profile == null)
                return Result<Device>.Failure(new Error("Profile.NotFound", "Child profile not found."));

            device.ProfileId = request.ProfileId;
            await _deviceRepo.UpdateAsync(device);
            await InvalidateDeviceCacheAsync(device.SerialNumber, device.DeviceId);
            
            return Result<Device>.Success(device);
        }

        /// <inheritdoc/>
        public async Task<Result<Device>> SetDeviceModeAsync(Guid userId, SetDeviceModeRequest request)
        {
            var device = await _deviceRepo.GetByIdAsync(request.DeviceId);
            if (device == null || device.UserId != userId)
                return Result<Device>.Failure(new Error("Device.NotFound", "Device not found or permission denied."));

            if (device.Profile == null)
                return Result<Device>.Failure(new Error("Device.NoProfile", "Please assign a profile first."));

            device.Profile.CurrentMode = request.Mode;
            await _profileRepo.UpdateAsync(device.Profile);
            await InvalidateDeviceCacheAsync(device.SerialNumber, device.DeviceId);

            return Result<Device>.Success(device);
        }

        /// <inheritdoc/>
        public async Task<Result> ToggleHardwareProtectionAsync(Guid userId, string deviceId, bool isEnabled)
        {
            var device = await _deviceRepo.GetByIdAsync(deviceId);
            if (device == null || device.UserId != userId)
                return Result.Failure(new Error("Device.NotFound", "Device not found or permission denied."));

            device.IsHardwareProtectionEnabled = isEnabled;
            await _deviceRepo.UpdateAsync(device);
            return Result.Success();
        }

        /// <inheritdoc/>
        public async Task<Result<ChildProfile>> CreateProfileAsync(Guid userId, string deviceId, UpdateProfileRequest request)
        {
            var device = await _deviceRepo.GetByIdAsync(deviceId);
            if (device == null || device.UserId != userId)
                return Result<ChildProfile>.Failure(new Error("Device.NotFound", "Device not found or permission denied."));

            var profile = new ChildProfile
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                Name = request.Name ?? "Bạn Nhỏ",
                Age = request.Age > 0 ? request.Age : 5,
                Gender = request.Gender ?? "Chưa xác định",
                Honorific = request.Honorific ?? "Bạn",
                Personality = request.Personality ?? "Vui vẻ",
                PersonalityDescription = request.PersonalityDescription ?? "",
                PreferredVoiceId = request.PreferredVoiceId ?? "vi-VN-Neural2-A",
                PreferredTtsProvider = request.PreferredTtsProvider ?? "GCP",
                SafetyResponseMode = request.SafetyResponseMode ?? SafetyResponseMode.GentleWarning,
                SafetyPretendMessage = request.SafetyPretendMessage ?? "Hả? Bé nói gì gấu nghe không rõ nhỉ?",
                SafetyWarningMessage = request.SafetyWarningMessage ?? "Bé ơi, mình nói chuyện khác vui hơn nhé!",
                BlockedTopics = request.BlockedTopics ?? new List<string>(),
                BearName = request.BearName ?? request.DeviceNickname ?? "Gấu SmartBear",
                ProfileImageUrl = request.ProfileImageUrl,
                CreativityLevel = request.CreativityLevel ?? 3,
                EmotionLevel = request.EmotionLevel ?? 3,
                EnergyLevel = request.EnergyLevel ?? 3,
                ComplexityLevel = request.ComplexityLevel ?? 3
            };


            await _profileRepo.AddAsync(profile);
            device.ProfileId = profile.Id;
            if (!string.IsNullOrWhiteSpace(request.DeviceNickname))
            {
                device.Nickname = request.DeviceNickname;
            }
            await _deviceRepo.UpdateAsync(device);
            await InvalidateDeviceCacheAsync(device.SerialNumber, device.DeviceId);

            return Result<ChildProfile>.Success(profile);
        }

        /// <inheritdoc/>
        public async Task<Result<ChildProfile>> UpdateProfileAsync(Guid userId, string profileId, UpdateProfileRequest request)
        {
            try
            {
                var profile = await _profileRepo.GetByIdAsync(profileId);
                if (profile == null)
                    return Result<ChildProfile>.Failure(new Error("Profile.NotFound", "Profile not found."));

                // Verify ownership: At least one device of this user must be linked to this profile
                var ownsProfile = await _deviceRepo.ExistsForUserAsync(userId, profileId);
                if (!ownsProfile)
                    return Result<ChildProfile>.Failure(new Error("Profile.Unauthorized", "You do not have permission to edit this profile."));

                // Apply updates (explicitly mapping all fields)
                if (request.Name != null) profile.Name = request.Name;
                if (request.Age > 0) profile.Age = request.Age;
                if (request.Gender != null) profile.Gender = request.Gender;
                if (request.Honorific != null) profile.Honorific = request.Honorific;
                if (request.Personality != null) profile.Personality = request.Personality;
                if (request.PersonalityDescription != null) profile.PersonalityDescription = request.PersonalityDescription;
                if (request.PreferredVoiceId != null) profile.PreferredVoiceId = request.PreferredVoiceId;
                if (request.PreferredTtsProvider != null) profile.PreferredTtsProvider = request.PreferredTtsProvider;
                if (request.SafetyResponseMode != null) profile.SafetyResponseMode = request.SafetyResponseMode.Value;
                if (request.SafetyPretendMessage != null) profile.SafetyPretendMessage = request.SafetyPretendMessage;
                if (request.SafetyWarningMessage != null) profile.SafetyWarningMessage = request.SafetyWarningMessage;
                if (request.BlockedTopics != null) profile.BlockedTopics = request.BlockedTopics;
                if (request.BearName != null) profile.BearName = request.BearName;
                else if (request.DeviceNickname != null) profile.BearName = request.DeviceNickname;

                if (request.ProfileImageUrl != null) profile.ProfileImageUrl = request.ProfileImageUrl;
                if (request.CreativityLevel.HasValue) profile.CreativityLevel = request.CreativityLevel.Value;
                if (request.EmotionLevel.HasValue) profile.EmotionLevel = request.EmotionLevel.Value;
                if (request.EnergyLevel.HasValue) profile.EnergyLevel = request.EnergyLevel.Value;
                if (request.ComplexityLevel.HasValue) profile.ComplexityLevel = request.ComplexityLevel.Value;

                await _profileRepo.UpdateAsync(profile);

                // Update device nickname if provided
                if (!string.IsNullOrWhiteSpace(request.DeviceNickname))
                {
                    // Find all devices linked to this profile and update their nickname
                    var devices = await _deviceRepo.GetDevicesWithDetailsByUserAsync(userId);
                    var targetDevice = devices.FirstOrDefault(d => d.ProfileId == profileId);
                    if (targetDevice != null)
                    {
                        targetDevice.Nickname = request.DeviceNickname;
                        await _deviceRepo.UpdateAsync(targetDevice);
                        await InvalidateDeviceCacheAsync(targetDevice.SerialNumber, targetDevice.DeviceId);
                        Console.WriteLine($"\x1b[32m[DeviceService] Updated bear nickname to: {request.DeviceNickname} for device {targetDevice.DeviceId}\x1b[0m");
                    }
                }

                // Also invalidate cache for any device using this profile
                var devicesUsingProfile = await _deviceRepo.GetDevicesWithDetailsByUserAsync(userId);
                foreach (var d in devicesUsingProfile.Where(d => d.ProfileId == profileId))
                {
                    await InvalidateDeviceCacheAsync(d.SerialNumber, d.DeviceId);
                }

                return Result<ChildProfile>.Success(profile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\x1b[31m[DeviceService] Error updating profile {profileId}: {ex.Message}\x1b[0m");
                return Result<ChildProfile>.Failure(new Error("Profile.UpdateError", "An unexpected error occurred while saving profile."));
            }
        }

        /// <inheritdoc/>
        public async Task<Result<ChildProfile>> UpdateProfileSafetyAsync(Guid userId, UpdateProfileSafetyRequest request)
        {
            var profile = await _profileRepo.GetByIdAsync(request.ProfileId);
            if (profile == null)
                return Result<ChildProfile>.Failure(new Error("Profile.NotFound", "Profile not found."));

            var ownsDevice = await _deviceRepo.ExistsForUserAsync(userId, profile.Id);
            if (!ownsDevice)
                return Result<ChildProfile>.Failure(new Error("Profile.Unauthorized", "Permission denied."));

            if (request.BlockedTopics != null) profile.BlockedTopics = request.BlockedTopics;
            if (request.SafetyResponseMode.HasValue) profile.SafetyResponseMode = request.SafetyResponseMode.Value;

            await _profileRepo.UpdateAsync(profile);
            return Result<ChildProfile>.Success(profile);
        }

        /// <inheritdoc/>
        public async Task<Result<bool>> IsDeviceOnlineAsync(string mac)
        {
            try
            {
                var baseUrl = _config["AppConfiguration:BridgeBaseUrl"] ?? "http://localhost:8000";
                var url = $"{baseUrl.TrimEnd('/')}/check-mac/{mac}";
                
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                var response = await _httpClient.GetAsync(url, cts.Token);
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(content);
                    bool online = data?["online"]?.GetValue<bool>() ?? false;
                    return Result<bool>.Success(online);
                }
                
                return Result<bool>.Success(false);
            }
            catch (Exception ex)
            {
                // Log error but return false to avoid crashing the UI
                Console.WriteLine($"[BridgeProxy] Error checking status for {mac}: {ex.Message}");
                return Result<bool>.Success(false);
            }
        }
    }
}
