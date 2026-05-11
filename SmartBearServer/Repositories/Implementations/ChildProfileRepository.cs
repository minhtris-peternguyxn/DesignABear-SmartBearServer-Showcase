using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Implementations
{
    public class ChildProfileRepository : IChildProfileRepository
    {
        private readonly AppDbContext _db;
        public ChildProfileRepository(AppDbContext db) => _db = db;

        public async Task<List<ChildProfile>> GetAllAsync() => await _db.ChildProfiles.Include(p => p.Interactions).ToListAsync();
        
        public async Task<ChildProfile?> GetByIdAsync(string id) => await _db.ChildProfiles
            .Include(p => p.Interactions)
            .Include(p => p.ParentUser)
                .ThenInclude(u => u.SubscriptionPlan)
            .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<ChildProfile> AddAsync(ChildProfile profile)
        {
            _db.ChildProfiles.Add(profile);
            await _db.SaveChangesAsync();
            return profile;
        }

        public async Task UpdateAsync(ChildProfile profile)
        {
            _db.ChildProfiles.Update(profile);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var p = await _db.ChildProfiles.FindAsync(id);
            if (p != null)
            {
                _db.ChildProfiles.Remove(p);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(ChildProfile profile)
        {
            _db.ChildProfiles.Remove(profile);
            await _db.SaveChangesAsync();
        }

        public async Task<List<ChildProfile>> GetForUserAsync(Guid userId)
        {
            return await _db.ChildProfiles
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }
    }
}
