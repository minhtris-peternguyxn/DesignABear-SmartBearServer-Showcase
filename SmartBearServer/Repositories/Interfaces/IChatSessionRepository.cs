using SmartBearServer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Interfaces
{
    /// <summary>
    /// Interface for managing chat sessions and their associated interactions.
    /// Provides methods for session retrieval, creation, and batch updates.
    /// </summary>
    public interface IChatSessionRepository
    {
        /// <summary>
        /// Retrieves an active session for a specific profile.
        /// </summary>
        /// <param name="profileId">The unique identifier of the child profile.</param>
        /// <returns>The active ChatSession or null if none found.</returns>
        Task<ChatSession?> GetActiveSessionAsync(string profileId);

        /// <summary>
        /// Retrieves a session by its unique ID, including its interactions.
        /// </summary>
        /// <param name="sessionId">The GUID of the session.</param>
        /// <returns>The ChatSession with interactions included.</returns>
        Task<ChatSession?> GetByIdAsync(Guid sessionId);

        /// <summary>
        /// Adds a new chat session to the database.
        /// </summary>
        /// <param name="session">The session object to create.</param>
        Task AddAsync(ChatSession session);

        /// <summary>
        /// Updates an existing chat session.
        /// </summary>
        /// <param name="session">The session object with updated fields.</param>
        Task UpdateAsync(ChatSession session);

        /// <summary>
        /// Saves a collection of interaction histories in a single transaction.
        /// </summary>
        /// <param name="interactions">The list of interactions to persist.</param>
        Task AddInteractionsAsync(IEnumerable<InteractionHistory> interactions);

        /// <summary>
        /// Persists all pending changes to the underlying data store.
        /// </summary>
        Task SaveChangesAsync();
    }
}
