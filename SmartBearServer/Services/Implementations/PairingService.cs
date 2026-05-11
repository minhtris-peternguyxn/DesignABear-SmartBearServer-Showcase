using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using SmartBearServer.Data;
using SmartBearServer.Hubs;
using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Model;
using SmartBearServer.Model.DTOs;
using SmartBearServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SmartBearServer.Services.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Implementations
{
    public class PairingService : IPairingService
    {
        private readonly IDeviceRepository _deviceRepo;
        private readonly IChildProfileRepository _profileRepo;
        private readonly IUserRepository _userRepo;
        private readonly IHubContext<LLMHub> _hubContext;
        private readonly IDistributedCache _cache;

        public PairingService(
            IDeviceRepository deviceRepo,
            IChildProfileRepository profileRepo,
            IUserRepository userRepo,
            IHubContext<LLMHub> hubContext,
            IDistributedCache cache)
        {
            _deviceRepo = deviceRepo;
            _profileRepo = profileRepo;
            _userRepo = userRepo;
            _hubContext = hubContext;
            _cache = cache;
        }

        public async Task<Result<string>> RequestPairingCodeAsync(string mac)
        {
            var normalizedMac = LLMHub.NormalizeSerial(mac);
            if (string.IsNullOrEmpty(normalizedMac))
                return Result<string>.Failure(new Error("Device.InvalidMac", "Địa chỉ MAC không hợp lệ."));
            
            // 1. Check if device exists
            var device = await _deviceRepo.GetBySerialNumberAsync(normalizedMac);
            if (device != null && device.UserId != null)
                return Result<string>.Failure(new Error("Device.AlreadyPaired", "Thiết bị này đã có chủ sở hữu. Vui lòng gỡ thiết bị khỏi tài khoản cũ trước khi ghép nối lại."));

            if (device == null)
            {
                device = new Device
                {
                    DeviceId = normalizedMac,
                    SerialNumber = normalizedMac,
                    Status = "ReadyToPair",
                    CreatedAt = DateTime.UtcNow
                };
                await _deviceRepo.AddAsync(device);
            }

            // 2. Cleanup old codes for this device to ensure "Request again" generates a fresh, unique active code
            var oldCode = await _cache.GetStringAsync($"sb_otp_v3:{normalizedMac}");
            if (!string.IsNullOrEmpty(oldCode))
            {
                await _cache.RemoveAsync($"sb_code_map_v3:{oldCode}");
            }

            // 3. Generate secure 6-digit code
            var code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

            // 4. Store in Redis (10 minutes)
            await _cache.SetStringAsync($"sb_code_map_v3:{code}", normalizedMac, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
            
            await _cache.SetStringAsync($"sb_otp_v3:{normalizedMac}", code, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

            // 5. Update device
            device.PairingCode = code;
            device.PairingCodeExpiresAt = DateTime.UtcNow.AddMinutes(10);
            await _deviceRepo.UpdateAsync(device);

            // 6. Speak it - Using standard BearResponse + target_mac for Python bridge
            await _hubContext.Clients.All.SendAsync("BearResponse", new {
                target_mac = normalizedMac,
                action = "trigger_alarm",
                text = $"Mã xác nhận là {string.Join(" ", code.ToCharArray())}",
                voice_id = "vi-VN-Neural2-D",
                provider = "GCP",
                language_code = "vi-VN"
            });

            return Result<string>.Success(code);
        }

        public async Task<Result<string>> RequestOtpAsync(string macOrSerial, Guid requestingUserId)
        {
            return await RequestPairingCodeAsync(macOrSerial);
        }

        public async Task<Result<Device>> ClaimDeviceAsync(Guid userId, string code, string? nickname, string? childName = null)
        {
            var normalizedMac = await _cache.GetStringAsync($"sb_code_map_v3:{code}");
            if (string.IsNullOrEmpty(normalizedMac))
                return Result<Device>.Failure(new Error("Pairing.InvalidCode", "Mã không đúng."));

            var device = await _deviceRepo.GetBySerialNumberAsync(normalizedMac);
            if (device == null) return Result<Device>.Failure(new Error("Device.NotFound", "Không tìm thấy thiết bị."));

            if (device.UserId != null)
                return Result<Device>.Failure(new Error("Device.AlreadyPaired", "Thiết bị này đã có chủ sở hữu. Vui lòng gỡ thiết bị khỏi tài khoản cũ trước khi ghép nối lại."));

            // Handle Profile
            if (!string.IsNullOrWhiteSpace(childName))
            {
                // Resolve candy limit: SubscriptionPlan > IsPro fallback > Basic default
                var parentUser = await _userRepo.GetByIdAsync(userId);
                int candyLimit = parentUser?.SubscriptionPlan?.DailyCandyLimit 
                    ?? (parentUser?.IsPro == true ? 50 : 10);

                var profile = new ChildProfile
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = childName.Trim(),
                    UserId = userId,
                    DailyCandyBalance = candyLimit,
                    DailyCandyLimit = candyLimit,
                    LastQuotaResetUtc = DateTime.UtcNow
                };
                await _profileRepo.AddAsync(profile);
                device.ProfileId = profile.Id;
            }

            device.UserId = userId;
            device.Nickname = string.IsNullOrWhiteSpace(nickname) ? "Gấu AI" : nickname.Trim();
            device.Status = "Online";
            device.PairingCode = null;
            device.PairingCodeExpiresAt = null;

            await _deviceRepo.UpdateAsync(device);
            await _cache.RemoveAsync($"sb_code_map_v3:{code}");
            
            // Invalidate caches to ensure AI agent sees the new owner and profile
            var norm = LLMHub.NormalizeSerial(device.SerialNumber);
            var normId = LLMHub.NormalizeSerial(device.DeviceId);
            await _cache.RemoveAsync($"bear:profile:config:{norm}");
            await _cache.RemoveAsync($"bear:profile:config:{normId}");
            await _cache.RemoveAsync($"device:config:sn:{norm}");
            await _cache.RemoveAsync($"device:config:id:{normId}");
            
            // Invalidate quota caches to force warmup from fresh DB
            if (!string.IsNullOrEmpty(device.ProfileId))
                await _cache.RemoveAsync($"quota:daily:candy:{device.ProfileId}");
            
            await _cache.RemoveAsync($"quota:purchased:candy:{userId}");

            return Result<Device>.Success(device);
        }

        public async Task RegisterExternalOtpAsync(string mac, string code)
        {
            var normalizedMac = mac.Replace(":", "").ToUpper();
            await _cache.SetStringAsync($"sb_code_map_v3:{code}", normalizedMac, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
        }
    }
}
