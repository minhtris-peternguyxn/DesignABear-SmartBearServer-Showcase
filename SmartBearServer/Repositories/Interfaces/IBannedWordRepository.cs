using SmartBearServer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Interfaces
{
    public interface IBannedWordRepository
    {
        Task<List<BannedWord>> GetAllAsync();
        Task<BannedWord> GetByIdAsync(int id);
        Task<BannedWord> AddAsync(BannedWord word);
        Task UpdateAsync(BannedWord word);
        Task DeleteAsync(BannedWord word);
        Task<bool> ExistsAsync(string word);


        Task<List<BannedWord>> GetForUserWithGlobalAsync(System.Guid userId);
        Task<List<BannedWord>> GetForUserOnlyAsync(System.Guid userId);
        Task<List<BannedWord>> GetGlobalAsync();
        Task<bool> ExistsInScopeAsync(string word, System.Guid? userId);
    }
}
