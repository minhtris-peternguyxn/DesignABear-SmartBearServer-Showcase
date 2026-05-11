using Microsoft.Extensions.Caching.Distributed;
using SmartBearServer.Model;
using SmartBearServer.Services.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Implementations
{
    /// <summary>
    /// Implementation of ICacheService using Redis.
    /// Manages application cache and persistent interaction queues.
    /// </summary>
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase? _db;
        private readonly string _queueKey = "chat:interaction:queue";
        private readonly string _failedQueueKey = "chat:interaction:failed";

        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
        };

        public RedisCacheService(IDistributedCache cache, IConnectionMultiplexer redis)
        {
            _cache = cache;
            _redis = redis;
            
            // Subscribe to connection events for real-time status logging
            _redis.ConnectionRestored += (s, e) => 
            {
                Console.WriteLine("\x1b[38;5;22m[Redis] Connection Restored: System back online\x1b[0m");
            };
            
            _redis.ConnectionFailed += (s, e) => 
            {
                Console.WriteLine("\x1b[38;5;88m[Redis] Connection Failed: Check if Docker/Redis is running.\x1b[0m");
            };

            try 
            {
                _db = _redis.GetDatabase();
                Console.WriteLine("\x1b[38;5;214m[Redis] Cache Service Initialized\x1b[0m");
                
                if (_redis.IsConnected)
                {
                    Console.WriteLine("\x1b[38;5;22m[Redis] Connected to instance at initialization\x1b[0m");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\x1b[38;5;88m[Redis] Failed to initialize database: {ex.Message}\x1b[0m");
            }
        }

        /// <summary>
        /// Retrieves a serialized item from the distributed cache.
        /// </summary>
        public async Task<T?> GetAsync<T>(string key)
        {
            var data = await _cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(data)) 
            {
                Console.WriteLine($"\x1b[38;5;240m[Redis] GET MISS: {key}\x1b[0m");
                return default;
            }

            Console.WriteLine($"\x1b[38;5;39m[Redis] GET HIT: {key}\x1b[0m");
            return JsonSerializer.Deserialize<T>(data, _serializerOptions);
        }

        /// <summary>
        /// Persists an item to the distributed cache.
        /// </summary>
        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options = new DistributedCacheEntryOptions();
            if (expiration.HasValue)
            {
                options.AbsoluteExpirationRelativeToNow = expiration;
            }

            var data = JsonSerializer.Serialize(value, _serializerOptions);
            await _cache.SetStringAsync(key, data, options);
            Console.WriteLine($"\x1b[38;5;40m[Redis] SET: {key} (Exp: {expiration?.TotalMinutes}m)\x1b[0m");
        }

        /// <summary>
        /// Removes an item from the cache.
        /// </summary>
        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
            Console.WriteLine($"\x1b[38;5;203m[Redis] REMOVE: {key}\x1b[0m");
        }

        /// <summary>
        /// Pushes an interaction record to the end of the Redis list (Queue).
        /// </summary>
        public async Task EnqueueInteractionAsync(object interaction)
        {
            if (!_redis.IsConnected || _db == null) 
                throw new InvalidOperationException("Redis is disconnected. Cannot enqueue interaction.");
            
            var data = JsonSerializer.Serialize(interaction, _serializerOptions);
            await _db.ListLeftPushAsync(_queueKey, data);
            Console.WriteLine($"\x1b[38;5;214m[Redis] ENQUEUE: {interaction.GetType().Name} -> {_queueKey}\x1b[0m");
        }

        public async Task<List<string>> DequeueInteractionsAsync(int batchSize)
        {
            var items = new List<string>();
            if (!_redis.IsConnected || _db == null) return items;
            
            for (int i = 0; i < batchSize; i++)
            {
                var item = await _db.ListRightPopAsync(_queueKey);
                if (item.IsNull) break;
                items.Add(item.ToString());
            }
            return items;
        }

        public async Task EnqueueFailedInteractionAsync(string json)
        {
            if (!_redis.IsConnected || _db == null)
                 throw new InvalidOperationException("Redis is disconnected. Cannot enqueue failed interaction.");
            await _db.ListLeftPushAsync(_failedQueueKey, json);
        }

        public async Task<long?> GetCounterAsync(string key)
        {
            if (!_redis.IsConnected || _db == null) return null;
            var value = await _db.StringGetAsync(key);
            if (value.IsNull) return null;
            if (long.TryParse(value.ToString(), out long result)) return result;
            return null;
        }

        public async Task SetCounterAsync(string key, long value, TimeSpan? expiration = null)
        {
            if (!_redis.IsConnected || _db == null) 
                throw new InvalidOperationException("Redis is disconnected. Cannot set counter.");
            await _db.StringSetAsync(key, value.ToString());
            if (expiration.HasValue)
            {
                await _db.KeyExpireAsync(key, expiration);
            }
        }

        public async Task<long> IncrementAsync(string key, long value = 1)
        {
            if (!_redis.IsConnected || _db == null) 
                throw new InvalidOperationException("Redis is disconnected. Cannot increment.");
            return await _db.StringIncrementAsync(key, value);
        }

        public async Task<long> DecrementAsync(string key, long value = 1)
        {
            if (!_redis.IsConnected || _db == null) 
                throw new InvalidOperationException("Redis is disconnected. Cannot decrement.");
            return await _db.StringDecrementAsync(key, value);
        }

        private readonly string _quotaQueueKey = "quota:updates:queue";

        public async Task EnqueueQuotaUpdateAsync(object updateEvent)
        {
            if (!_redis.IsConnected || _db == null) 
                throw new InvalidOperationException("Redis is disconnected. Cannot enqueue quota update.");
                
            var data = JsonSerializer.Serialize(updateEvent, _serializerOptions);
            await _db.ListLeftPushAsync(_quotaQueueKey, data);
        }

        public async Task EnqueueQuotaUpdateRawAsync(string json)
        {
            if (!_redis.IsConnected || _db == null) 
                throw new InvalidOperationException("Redis is disconnected. Cannot enqueue quota update.");
            
            await _db.ListLeftPushAsync(_quotaQueueKey, json);
        }

        public async Task<List<string>> DequeueQuotaUpdatesAsync(int batchSize)
        {
            var items = new List<string>();
            if (!_redis.IsConnected || _db == null) return items;
            
            for (int i = 0; i < batchSize; i++)
            {
                var item = await _db.ListRightPopAsync(_quotaQueueKey);
                if (item.IsNull) break;
                items.Add(item.ToString());
            }
            return items;
        }

        public async Task<List<T>> GetRecentHistoryAsync<T>(string profileId, int limit = 5)
        {
            var results = new List<T>();
            if (!_redis.IsConnected || _db == null) return results;

            var key = $"history:profile:{profileId}";
            var rawItems = await _db.ListRangeAsync(key, 0, limit - 1);
            
            foreach (var item in rawItems)
            {
                if (item.IsNull) continue;
                var obj = JsonSerializer.Deserialize<T>(item.ToString(), _serializerOptions);
                if (obj != null) results.Add(obj);
            }
            return results;
        }

        public async Task PushRecentHistoryAsync<T>(string profileId, T interaction, int maxCount = 5)
        {
            if (!_redis.IsConnected || _db == null) return;

            var key = $"history:profile:{profileId}";
            var data = JsonSerializer.Serialize(interaction, _serializerOptions);
            
            await _db.ListLeftPushAsync(key, data);
            await _db.ListTrimAsync(key, 0, maxCount - 1);
            await _db.KeyExpireAsync(key, TimeSpan.FromHours(1)); // History expires if inactive for 1 hour
        }
        public async Task RemoveByPatternAsync(string pattern)
        {
            if (!_redis.IsConnected || _db == null) return;

            var server = _redis.GetServers().FirstOrDefault(s => s.IsConnected);
            if (server == null) return;

            int deletedCount = 0;
            await foreach (var key in server.KeysAsync(pattern: pattern))
            {
                await _db.KeyDeleteAsync(key);
                deletedCount++;
            }
            
            Console.WriteLine($"\x1b[38;5;203m[Redis] PATTERN DELETE: {pattern} ({deletedCount} keys removed)\x1b[0m");
        }
    }
}
