using SmartBearServer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    public interface IBannedWordService
    {
        Task<List<BannedWord>> GetBannedWordsAsync(Guid userId);
        Task<List<BannedWord>> GetForUserOnlyAsync(Guid userId);
        Task<List<BannedWord>> GetGlobalBannedWordsAsync();
        Task<List<BannedWord>> GetGlobalBannedWordsCachedAsync();
        Task SyncGlobalBannedWordsAsync();
        Task<BannedWord> AddBannedWordAsync(Guid userId, string word, string? category, bool isGlobal, bool isMaster);
        Task<bool> DeleteBannedWordAsync(Guid userId, int id, bool isMaster);
        Task<bool> UpdateSafetySettingsAsync(Guid userId, string profileId, List<string>? blockedTopics, int? responseMode);
        Task<bool> UpdateProfileBannedKeywordsAsync(Guid userId, string profileId, List<string> keywords);
    }
}
