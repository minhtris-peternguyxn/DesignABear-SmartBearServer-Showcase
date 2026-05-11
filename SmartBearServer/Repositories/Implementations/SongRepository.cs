using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartBearServer.Repositories.Interfaces;

namespace SmartBearServer.Repositories.Implementations
{
    public class SongRepository : ISongRepository
    {
        private readonly AppDbContext _db;
        public SongRepository(AppDbContext db) => _db = db;

        public async Task<List<Song>> GetAllAsync() => await _db.Songs.ToListAsync();

        public async Task<Song> GetByIdAsync(string id) => await _db.Songs.FindAsync(id);
        public async Task<Song?> GetByNameAsync(string name) => await _db.Songs.FirstOrDefaultAsync(s => s.Name.Contains(name));
        public async Task<Song?> GetRandomAsync() => await _db.Songs.OrderBy(s => Guid.NewGuid()).FirstOrDefaultAsync();

        public async Task<Song> AddAsync(Song song)
        {
            _db.Songs.Add(song);
            await _db.SaveChangesAsync();
            return song;
        }

        public async Task UpdateAsync(Song song)
        {
            _db.Songs.Update(song);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var s = await _db.Songs.FindAsync(id);
            if (s != null)
            {
                _db.Songs.Remove(s);
                await _db.SaveChangesAsync();
            }
        }
    }
}
