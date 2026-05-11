using SmartBearServer.Services.Interfaces;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Model;
using SmartBearServer.Data.SeedData;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Implementations
{
    public class BannedWordService : IBannedWordService
    {
        private readonly IBannedWordRepository _repo;
        private readonly IDeviceRepository _deviceRepo;
        private readonly ICacheService _cache;
        private const string GlobalBannedCacheKey = "safety:banned:global";

        public BannedWordService(IBannedWordRepository repo, IDeviceRepository deviceRepo, ICacheService cache)
        {
            _repo = repo;
            _deviceRepo = deviceRepo;
            _cache = cache;
        }

        public async Task<List<BannedWord>> GetBannedWordsAsync(Guid userId)
        {
            return await _repo.GetForUserWithGlobalAsync(userId);
        }

        public async Task<List<BannedWord>> GetGlobalBannedWordsAsync()
        {
            return await _repo.GetGlobalAsync();
        }

        public async Task<List<BannedWord>> GetGlobalBannedWordsCachedAsync()
        {
            try
            {
                var cached = await _cache.GetAsync<List<BannedWord>>(GlobalBannedCacheKey);
                if (cached != null) return cached;

                var global = await GetGlobalBannedWordsAsync();
                await _cache.SetAsync(GlobalBannedCacheKey, global, TimeSpan.FromHours(1));
                return global;
            }
            catch
            {
                return await GetGlobalBannedWordsAsync();
            }
        }

        public async Task SyncGlobalBannedWordsAsync()
        {
            var seedData = BannedWordSeed.GetSeedData();
            var currentGlobal = await GetGlobalBannedWordsAsync();
            
            bool changed = false;
            foreach (var seed in seedData)
            {
                if (!currentGlobal.Any(g => g.Word.ToLower() == seed.Word.ToLower()))
                {
                    await _repo.AddAsync(seed);
                    changed = true;
                }
            }

            if (changed)
            {
                await _cache.RemoveAsync(GlobalBannedCacheKey);
            }
        }

        public async Task<List<BannedWord>> GetForUserOnlyAsync(Guid userId)
        {
            return await _repo.GetForUserOnlyAsync(userId);
        }

        public async Task<BannedWord> AddBannedWordAsync(Guid userId, string word, string? category, bool isGlobal, bool isMaster)
        {
            var wordStr = word.Trim().ToLower();

            // Scope check: global if master requested, otherwise tied to user
            Guid? targetUserId = (isGlobal && isMaster) ? null : userId;
            
            bool exists = await _repo.ExistsInScopeAsync(wordStr, targetUserId);
            if (exists)
            {
                throw new Exception($"The word '{wordStr}' is already in the list for this scope.");
            }

            var entry = new BannedWord
            {
                Word = wordStr,
                Category = category?.Trim().ToLower(),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = isMaster ? "master" : "parent",
                UserId = targetUserId
            };

            await _repo.AddAsync(entry);

            return entry;
        }

        public async Task<bool> DeleteBannedWordAsync(Guid userId, int id, bool isMaster)
        {
            var entry = await _repo.GetByIdAsync(id);
            if (entry == null) return false;

            // Permission: must own it or be master deleting system word
            if (entry.UserId != userId && !(entry.UserId == null && isMaster))
            {
                return false;
            }

            await _repo.DeleteAsync(entry);
            return true;
        }

        public async Task<bool> UpdateSafetySettingsAsync(Guid userId, string profileId, List<string>? blockedTopics, int? responseMode)
        {
            var device = await _deviceRepo.GetByProfileIdWithProfileAsync(profileId, userId);
            if (device == null || device.Profile == null) return false;

            if (blockedTopics != null)
            {
                device.Profile.BlockedTopics = blockedTopics;
            }

            if (responseMode.HasValue)
            {
                device.Profile.SafetyResponseMode = (SafetyResponseMode)responseMode.Value;
            }

            await _deviceRepo.UpdateAsync(device);
            return true;
        }

        public async Task<bool> UpdateProfileBannedKeywordsAsync(Guid userId, string profileId, List<string> keywords)
        {
            var device = await _deviceRepo.GetByProfileIdWithProfileAsync(profileId, userId);
            if (device == null || device.Profile == null) return false;

            // Map to BlockedTopics instead
            device.Profile.BlockedTopics = keywords
                .Where(kw => !string.IsNullOrWhiteSpace(kw))
                .Select(kw => kw.Trim().ToLower())
                .Distinct()
                .ToList();

            await _deviceRepo.UpdateAsync(device);
            return true;
        }
    }
}
