using SmartBearServer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for Story entity management.
    /// Handles database operations for child stories and narratives.
    /// </summary>
    public interface IStoryRepository
    {
        /// <summary>
        /// Retrieves all stories from the database.
        /// </summary>
        Task<List<Story>> GetAllAsync();

        /// <summary>
        /// Retrieves a story by its unique identifier.
        /// </summary>
        Task<Story?> GetByIdAsync(string id);

        /// <summary>
        /// Adds a new story record to the database.
        /// </summary>
        Task<Story> AddAsync(Story story);

        /// <summary>
        /// Updates an existing story record.
        /// </summary>
        Task UpdateAsync(Story story);

        /// <summary>
        /// Deletes a story record from the database.
        /// </summary>
        Task DeleteAsync(string id);
    }
}
