using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Implementations
{
    public class BannedWordRepository : IBannedWordRepository
    {
        private readonly AppDbContext _db;

        public BannedWordRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<BannedWord>> GetAllAsync()
        {
            return await _db.BannedWords.ToListAsync();
        }

        public async Task<BannedWord> GetByIdAsync(int id)
        {
            return await _db.BannedWords.FindAsync(id);
        }

        public async Task<BannedWord> AddAsync(BannedWord word)
        {
            await _db.BannedWords.AddAsync(word);
            await _db.SaveChangesAsync();
            return word;
        }

        public async Task UpdateAsync(BannedWord word)
        {
            _db.BannedWords.Update(word);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(BannedWord word)
        {
            _db.BannedWords.Remove(word);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string word)
        {
            return await _db.BannedWords.AnyAsync(bw => bw.Word.ToLower() == word.ToLower());
        }

        public async Task<List<BannedWord>> GetForUserWithGlobalAsync(Guid userId)
        {
            return await _db.BannedWords
                .Where(b => b.UserId == null || b.UserId == userId)
                .OrderBy(b => b.UserId == null ? 0 : 1)
                .ThenBy(b => b.Word)
                .ToListAsync();
        }

        public async Task<List<BannedWord>> GetForUserOnlyAsync(Guid userId)
        {
            return await _db.BannedWords
                .Where(b => b.UserId == userId)
                .OrderBy(b => b.Word)
                .ToListAsync();
        }

        public async Task<List<BannedWord>> GetGlobalAsync()
        {
            return await _db.BannedWords
                .Where(b => b.UserId == null)
                .OrderBy(b => b.Word)
                .ToListAsync();
        }

        public async Task<bool> ExistsInScopeAsync(string word, Guid? userId)
        {
            return await _db.BannedWords.AnyAsync(b => b.Word == word && b.UserId == userId);
        }
    }
}
