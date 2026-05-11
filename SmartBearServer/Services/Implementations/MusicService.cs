using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Services
{
    public class MusicService
    {
        private readonly ISongRepository _songRepo;

        public MusicService(ISongRepository songRepo)
        {
            _songRepo = songRepo;
        }

        public async Task<Song?> FindSongByNameAsync(string name)
        {
            return await _songRepo.GetByNameAsync(name);
        }

        public async Task<Song?> GetRandomSongAsync()
        {
            return await _songRepo.GetRandomAsync();
        }

        public async Task<IEnumerable<Song>> GetAllSongsAsync()
        {
            return await _songRepo.GetAllAsync();
        }
    }
}
