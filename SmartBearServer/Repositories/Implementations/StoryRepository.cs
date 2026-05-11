using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Implementations
{
    /// <summary>
    /// Repository implementation for Story entity management using Entity Framework Core.
    /// Provides data access logic for media stories.
    /// </summary>
    public class StoryRepository : IStoryRepository
    {
        private readonly AppDbContext _context;

        public StoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Story>> GetAllAsync()
        {
            return await _context.Stories.ToListAsync();
        }

        public async Task<Story?> GetByIdAsync(string id)
        {
            return await _context.Stories.FindAsync(id);
        }

        public async Task<Story> AddAsync(Story story)
        {
            _context.Stories.Add(story);
            await _context.SaveChangesAsync();
            return story;
        }

        public async Task UpdateAsync(Story story)
        {
            _context.Entry(story).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var story = await _context.Stories.FindAsync(id);
            if (story != null)
            {
                _context.Stories.Remove(story);
                await _context.SaveChangesAsync();
            }
        }
    }
}
