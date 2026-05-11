using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    /// <summary>
    /// Abstraction for distributed caching and queuing services.
    /// Provides methods for standard key-value operations and interaction queuing.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Retrieves a cached item by its key.
        /// </summary>
        Task<T?> GetAsync<T>(string key);

        /// <summary>
        /// Stores an item in the cache with an optional expiration period.
        /// </summary>
        Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);

        /// <summary>
        /// Removes an item from the cache by its key.
        /// </summary>
        Task RemoveAsync(string key);

        /// <summary>
        /// Pushes a chat interaction into the Redis persistent queue.
        /// </summary>
        /// <param name="interaction">The interaction data to be queued.</param>
        Task EnqueueInteractionAsync(object interaction);

        /// <summary>
        /// Retrieves and removes a batch of interactions from the queue.
        /// </summary>
        /// <param name="batchSize">Maximum number of items to retrieve.</param>
        /// <returns>A collection of interaction objects in JSON format.</returns>
        Task<List<string>> DequeueInteractionsAsync(int batchSize);

        /// <summary>
        /// Pushes a failed chat interaction JSON to the dead-letter queue (DLQ).
        /// </summary>
        Task EnqueueFailedInteractionAsync(string json);

        /// <summary>
        /// Retrieves an atomic counter value from Redis. Returns null if not exists.
        /// </summary>
        Task<long?> GetCounterAsync(string key);

        /// <summary>
        /// Sets an atomic counter value in Redis.
        /// </summary>
        Task SetCounterAsync(string key, long value, TimeSpan? expiration = null);

        /// <summary>
        /// Atomically increments a counter in Redis.
        /// </summary>
        Task<long> IncrementAsync(string key, long value = 1);

        /// <summary>
        /// Atomically decrements a counter in Redis.
        /// </summary>
        Task<long> DecrementAsync(string key, long value = 1);

        /// <summary>
        /// Pushes a quota update event into the Redis persistent queue.
        /// </summary>
        Task EnqueueQuotaUpdateAsync(object updateEvent);

        /// <summary>
        /// Pushes a raw pre-serialized JSON string directly to the quota update queue.
        /// Used for re-enqueue on transaction rollback to avoid double-serialization.
        /// </summary>
        Task EnqueueQuotaUpdateRawAsync(string json);

        /// <summary>
        /// Retrieves and removes a batch of quota updates from the queue.
        /// </summary>
        Task<List<string>> DequeueQuotaUpdatesAsync(int batchSize);

        /// <summary>
        /// Removes all keys matching the given pattern (e.g., "quota:daily:candy:*").
        /// Used to invalidate cached quotas after daily reset or plan changes.
        /// </summary>
        Task RemoveByPatternAsync(string pattern);

        /// <summary>
        /// Retrieves the short-term chat history for a specific profile from Redis.
        /// Used for instant memory in AI prompts.
        /// </summary>
        Task<List<T>> GetRecentHistoryAsync<T>(string profileId, int limit = 5);

        /// <summary>
        /// Pushes a new interaction to the short-term history list in Redis.
        /// </summary>
        Task PushRecentHistoryAsync<T>(string profileId, T interaction, int maxCount = 5);
    }
}
