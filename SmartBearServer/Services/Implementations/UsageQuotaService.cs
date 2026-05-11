using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using System;
using System.Threading.Tasks;

using SmartBearServer.Services.Interfaces;

namespace SmartBearServer.Services.Implementations
{
    public class UsageQuotaService : IUsageQuotaService
    {
        private readonly AppDbContext _db;
        private readonly ICacheService _cache;

        public UsageQuotaService(AppDbContext db, ICacheService cache)
        {
            _db = db;
            _cache = cache;
        }

        public async Task WarmupQuotaAsync(Device device)
        {
            await CanConsumeAiAsync(device);
            await CanConsumeAudioAsync(device, 0);
        }

        private string GetDailyQuotaKey(string profileId) => $"quota:daily:candy:{profileId}";
        private string GetPurchasedQuotaKey(Guid userId) => $"quota:purchased:candy:{userId}";
        private string GetAudioQuotaKey(string deviceId, string profileId) => $"quota:audio:{deviceId}:{profileId}";

        public async Task<(bool Allowed, string Message, string? ConsumptionType)> CanConsumeAiAsync(Device device)
        {
            if (device.Profile == null) return (false, "Lỗi: Không tìm thấy hồ sơ của bé.", null);

            var profile = device.Profile;
            var dailyKey = GetDailyQuotaKey(profile.Id);
            
            // 1. Check Daily Balance from Redis
            var dailyBalance = await _cache.GetCounterAsync(dailyKey);
            
            // WARM-UP: Only populate Redis if key is completely missing.
            // DO NOT overwrite if Redis < DB, because DB is always stale (async queue sync).
            // Overwriting would grant phantom candy on reconnection.
            if (dailyBalance == null)
            {
                dailyBalance = profile.DailyCandyBalance;
                await _cache.SetCounterAsync(dailyKey, dailyBalance.Value, TimeSpan.FromHours(24));
                
                if (dailyBalance > 0) 
                    Console.WriteLine($"\x1b[32m[QuotaService] Syncing Redis with DB for {profile.Id}: {dailyBalance} candies\x1b[0m");
            }

            if (dailyBalance > 0)
            {
                return (true, string.Empty, "Daily");
            }

            // 2. Check Purchased Wallet from Redis
            if (device.UserId.HasValue)
            {
                var purchasedKey = GetPurchasedQuotaKey(device.UserId.Value);
                var purchasedBalance = await _cache.GetCounterAsync(purchasedKey);

                if (purchasedBalance == null)
                {
                    var parent = device.ParentUser ?? await _db.Users.FindAsync(device.UserId.Value);
                    purchasedBalance = parent?.SmartCandies ?? 0;
                    await _cache.SetCounterAsync(purchasedKey, purchasedBalance.Value, TimeSpan.FromHours(24));
                }

                if (purchasedBalance > 0)
                {
                    return (true, string.Empty, "Purchased");
                }
            }

            return (false, "Bé ơi, Gấu hết kẹo rồi. Bé bảo ba mẹ mua thêm kẹo cho Gấu nhé!", null);
        }

        public async Task<bool> ConsumeAiAsync(Device device, string consumptionType)
        {
            if (device.Profile == null) return false;

            if (consumptionType == "Daily")
            {
                var key = GetDailyQuotaKey(device.Profile.Id);
                var newBalance = await _cache.DecrementAsync(key);
                
                // Push event for background sync
                await _cache.EnqueueQuotaUpdateAsync(new { 
                    Type = "ConsumeCandyDaily", 
                    ProfileId = device.Profile.Id, 
                    Amount = 1,
                    Timestamp = DateTime.UtcNow
                });
                
                return newBalance >= 0;
            }
            else if (consumptionType == "Purchased" && device.UserId.HasValue)
            {
                var key = GetPurchasedQuotaKey(device.UserId.Value);
                var newBalance = await _cache.DecrementAsync(key);
                
                // Push event for background sync
                await _cache.EnqueueQuotaUpdateAsync(new { 
                    Type = "ConsumeCandyPurchased", 
                    UserId = device.UserId.Value, 
                    Amount = 1,
                    Timestamp = DateTime.UtcNow
                });
                
                return newBalance >= 0;
            }

            return false;
        }

        /// <summary>
        /// Global sweep to refill daily candies for all children.
        /// Primarily used by BackgroundService at 0:00 or on server startup.
        /// </summary>
        public async Task<int> ResetAllDailyQuotasAsync()
        {
            // Use UTC midnight with Kind=Utc for Npgsql timestamptz compatibility
            var todayVn = DateTime.UtcNow.AddHours(7).Date;
            var todayUtc = DateTime.SpecifyKind(todayVn.AddHours(-7), DateTimeKind.Utc);
            
            Console.WriteLine($"\x1b[34m[QuotaService] Starting Atomic Quota Reset Sweep (Date: {todayVn:yyyy-MM-dd})...\x1b[0m");

            // Simplified SQL: Use daily_candy_limit directly from ChildProfile.
            // daily_candy_limit is already kept in sync with the subscription plan by
            // PaymentService, AdminService, and PairingService whenever plans change.
            // This removes the dependency on user_id JOIN (which skipped orphan profiles)
            // and subscription_plan_id (which was often NULL).
            string sql = @"
                UPDATE ""ChildProfiles""
                SET ""daily_candy_balance"" = ""daily_candy_limit"",
                    ""last_quota_reset_utc"" = @p0
                WHERE ""IsDeleted"" = false
                  AND ""last_quota_reset_utc"" < @p0";

            int resetCount = await _db.Database.ExecuteSqlRawAsync(sql, todayUtc);
            Console.WriteLine($"\x1b[32m[QuotaService] Atomic sweep completed. Refilled {resetCount} profiles to their plan limits.\x1b[0m");

            // Invalidate ALL cached daily candy keys so warmup re-reads fresh DB values.
            // Critical for bears that stay online overnight without reconnecting.
            await _cache.RemoveByPatternAsync("quota:daily:candy:*");

            return resetCount;
        }

        public async Task<(bool Allowed, string Message)> CanConsumeAudioAsync(Device device, int additionalAudioSeconds)
        {
            if (device.ProfileId == null) return (false, "Lỗi: Gấu chưa được gán hồ sơ.");
            
            var key = GetAudioQuotaKey(device.DeviceId, device.ProfileId);
            var audioSecondsUsed = await _cache.GetCounterAsync(key);
            
            // WARM-UP
            if (audioSecondsUsed == null)
            {
                var usage = await GetOrCreateTodayUsageAsync(device.DeviceId, device.ProfileId);
                audioSecondsUsed = usage.AudioSecondsUsed;
                await _cache.SetCounterAsync(key, audioSecondsUsed.Value, TimeSpan.FromHours(24));
            }

            int maxAudio = (device.ParentUser?.IsPro == true) ? 7200 : 900;

            if (audioSecondsUsed + additionalAudioSeconds > maxAudio)
            {
                return (false, "Gấu hơi mệt rồi, bé để Gấu nghỉ ngơi chút rồi lát mình kể chuyện tiếp nhé!");
            }

            return (true, string.Empty);
        }

        public async Task ConsumeAudioAsync(string deviceId, int audioSeconds, string? profileId = null)
        {
            if (profileId == null) return;
            
            var key = GetAudioQuotaKey(deviceId, profileId);
            await _cache.IncrementAsync(key, Math.Max(0, audioSeconds));
            
            // Push event for background sync
            await _cache.EnqueueQuotaUpdateAsync(new { 
                Type = "ConsumeAudio", 
                DeviceId = deviceId,
                ProfileId = profileId,
                Amount = Math.Max(0, audioSeconds),
                Timestamp = DateTime.UtcNow
            });
        }

        public static int EstimateAudioSecondsFromMp3Bytes(long bytes)
        {
            // High-level estimate for 32kbps mono audio (common for XiaoZhi TTS/Music)
            // 32kbps = 4KB / sec
            return (int)(bytes / 4000);
        }

        private async Task<DailyUsage> GetOrCreateTodayUsageAsync(string deviceId, string? profileId = null)
        {
            var today = DateTime.UtcNow.Date;
            var usage = await _db.DailyUsages.FirstOrDefaultAsync(u => u.DeviceId == deviceId && u.DateUtc == today);

            if (usage != null)
            {
                if (usage.ProfileId != profileId && profileId != null) usage.ProfileId = profileId;
                return usage;
            }

            usage = new DailyUsage
            {
                DeviceId = deviceId,
                ProfileId = profileId ?? string.Empty,
                DateUtc = today,
                AiRequestCount = 0,
                AudioSecondsUsed = 0
            };

            _db.DailyUsages.Add(usage);
            await _db.SaveChangesAsync();
            return usage;
        }
        public async Task<(bool Success, string? ConsumptionType)> TryConsumeAiAsync(Device device)
        {
            if (device.Profile == null) return (false, null);

            var dailyKey = GetDailyQuotaKey(device.Profile.Id);
            
            // Inline warmup: if key expired or never set, DECR would create it as -1 and falsely reject
            var dailyExists = await _cache.GetCounterAsync(dailyKey);
            if (dailyExists == null)
            {
                var balanceToCache = device.Profile.DailyCandyBalance;
                await _cache.SetCounterAsync(dailyKey, balanceToCache, TimeSpan.FromHours(24));
                Console.WriteLine($"\x1b[32m[QuotaService] TryConsumeAi: Warmup Daily cache for {device.Profile.Id} with {balanceToCache} candies\x1b[0m");
            }
            else
            {
                Console.WriteLine($"[QuotaService] TryConsumeAi: Daily Cache exists: {dailyExists} candies");
            }

            // 1. Try Daily (Atomic)
            long daily = await _cache.DecrementAsync(dailyKey);
            if (daily >= 0)
            {
                Console.WriteLine($"\x1b[32m[QuotaService] TryConsumeAi: SUCCESS using Daily candy. Remaining: {daily}\x1b[0m");
                await _cache.EnqueueQuotaUpdateAsync(new { 
                    Type = "ConsumeCandyDaily", 
                    ProfileId = device.Profile.Id, 
                    Amount = 1,
                    Timestamp = DateTime.UtcNow
                });
                return (true, "Daily");
            }
            
            // Undo decrement if failed
            await _cache.IncrementAsync(dailyKey);
            Console.WriteLine($"[QuotaService] TryConsumeAi: FAILED Daily check (0 balance). Trying Purchased...");

            // 2. Try Purchased
            if (device.UserId.HasValue)
            {
                var purchasedKey = GetPurchasedQuotaKey(device.UserId.Value);
                
                // Inline warmup for purchased candy
                var purchasedExists = await _cache.GetCounterAsync(purchasedKey);
                if (purchasedExists == null)
                {
                    var parent = device.ParentUser ?? await _db.Users.FindAsync(device.UserId.Value);
                    var balance = parent?.SmartCandies ?? 0;
                    await _cache.SetCounterAsync(purchasedKey, balance, TimeSpan.FromHours(24));
                }

                long purchased = await _cache.DecrementAsync(purchasedKey);
                if (purchased >= 0)
                {
                    Console.WriteLine($"\x1b[32m[QuotaService] TryConsumeAi: SUCCESS using Purchased candy. Remaining: {purchased}\x1b[0m");
                    await _cache.EnqueueQuotaUpdateAsync(new { 
                        Type = "ConsumeCandyPurchased", 
                        UserId = device.UserId.Value, 
                        Amount = 1,
                        Timestamp = DateTime.UtcNow
                    });
                    return (true, "Purchased");
                }
                // Undo decrement if failed
                await _cache.IncrementAsync(purchasedKey);
                Console.WriteLine($"[QuotaService] TryConsumeAi: FAILED Purchased check (0 balance)");
            }

            return (false, null);
        }

        public async Task RefundAiAsync(Device device, string consumptionType)
        {
            if (device.Profile == null) return;

            if (consumptionType == "Daily")
            {
                var key = GetDailyQuotaKey(device.Profile.Id);
                await _cache.IncrementAsync(key);
                await _cache.EnqueueQuotaUpdateAsync(new { 
                    Type = "ConsumeCandyDaily", 
                    ProfileId = device.Profile.Id, 
                    Amount = -1, // Refund
                    Timestamp = DateTime.UtcNow
                });
            }
            else if (consumptionType == "Purchased" && device.UserId.HasValue)
            {
                var key = GetPurchasedQuotaKey(device.UserId.Value);
                await _cache.IncrementAsync(key);
                await _cache.EnqueueQuotaUpdateAsync(new { 
                    Type = "ConsumeCandyPurchased", 
                    UserId = device.UserId.Value, 
                    Amount = -1, // Refund
                    Timestamp = DateTime.UtcNow
                });
            }
        }
    }
}

