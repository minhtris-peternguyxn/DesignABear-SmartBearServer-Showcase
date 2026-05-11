using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Implementations
{
    /// <summary>
    /// Implementation of IChatSessionRepository using Entity Framework Core.
    /// Handles database operations for chat sessions and interaction logs.
    /// </summary>
    public class ChatSessionRepository : IChatSessionRepository
    {
        private readonly AppDbContext _context;

        public ChatSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves the most recent active session for a profile.
        /// </summary>
        public async Task<ChatSession?> GetActiveSessionAsync(string profileId)
        {
            return await _context.ChatSessions
                .Where(s => s.ProfileId == profileId && s.IsActive)
                .OrderByDescending(s => s.LastInteractionTime)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves a session by ID including interaction history.
        /// </summary>
        public async Task<ChatSession?> GetByIdAsync(Guid sessionId)
        {
            return await _context.ChatSessions
                .Include(s => s.Interactions)
                .FirstOrDefaultAsync(s => s.Id == sessionId);
        }

        /// <summary>
        /// Adds a new session to the database.
        /// </summary>
        public async Task AddAsync(ChatSession session)
        {
            await _context.ChatSessions.AddAsync(session);
        }

        /// <summary>
        /// Updates an existing session's metadata.
        /// </summary>
        public async Task UpdateAsync(ChatSession session)
        {
            _context.ChatSessions.Update(session);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Bulk adds interaction histories to the database.
        /// </summary>
        public async Task AddInteractionsAsync(IEnumerable<InteractionHistory> interactions)
        {
            await _context.InteractionHistories.AddRangeAsync(interactions);
        }

        /// <summary>
        /// Commits all changes to the database.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
