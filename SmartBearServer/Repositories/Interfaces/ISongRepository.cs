using SmartBearServer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Interfaces
{
    public interface ISongRepository
    {
        Task<List<Song>> GetAllAsync();
        Task<Song> GetByIdAsync(string id);
        Task<Song?> GetByNameAsync(string name);
        Task<Song?> GetRandomAsync();
        Task<Song> AddAsync(Song song);
        Task UpdateAsync(Song song);
        Task DeleteAsync(string id);
    }
}
