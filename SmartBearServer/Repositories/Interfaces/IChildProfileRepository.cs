using SmartBearServer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Interfaces
{
    public interface IChildProfileRepository
    {
        Task<List<ChildProfile>> GetAllAsync();
        Task<ChildProfile?> GetByIdAsync(string id);
        Task<ChildProfile> AddAsync(ChildProfile profile);
        Task UpdateAsync(ChildProfile profile);
        Task DeleteAsync(string id);
        Task DeleteAsync(ChildProfile profile);
        Task<List<ChildProfile>> GetForUserAsync(Guid userId);
    }
}
