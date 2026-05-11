using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartBearServer.Model;
using SmartBearServer.Services.Implementations;
using SmartBearServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SmartBearServer.Services
{
    /// <summary>
    /// Background service that synchronizes chat interactions from Redis queue to PostgreSQL.
    /// Operates on a periodic interval to perform batch persistence.
    /// </summary>
    public class ChatPersistenceWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ChatPersistenceWorker> _logger;
        private readonly TimeSpan _pollingInterval = TimeSpan.FromSeconds(15);
        private const int BatchSize = 100;

        public ChatPersistenceWorker(IServiceProvider serviceProvider, ILogger<ChatPersistenceWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ChatPersistenceWorker initialized and starting execution.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessQueueAsync(stoppingToken);
                    await ProcessQuotaQueueAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error occurred in ChatPersistenceWorker loop.");
                }

                await Task.Delay(_pollingInterval, stoppingToken);
            }

            _logger.LogInformation("ChatPersistenceWorker is shutting down.");
        }

        /// <summary>
        /// Retrieves pending interactions from Redis and persists them using a scoped SessionService.
        /// </summary>
        private async Task ProcessQueueAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var cache = scope.ServiceProvider.GetRequiredService<ICacheService>();
            var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();

            var rawItems = await cache.DequeueInteractionsAsync(BatchSize);
            if (rawItems == null || rawItems.Count == 0)
            {
                return;
            }

            Console.WriteLine($"\x1b[38;5;22m[Redis] Syncing {rawItems.Count} interactions to database...\x1b[0m");

            foreach (var json in rawItems)
            {
                if (stoppingToken.IsCancellationRequested) break;

                try
                {
                    var interaction = JsonSerializer.Deserialize<PendingInteractionDto>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (interaction != null)
                    {
                        await sessionService.SaveInteractionAsync(interaction);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to process interaction record: {Json}. Moving to DLQ.", json);
                    
                    try
                    {
                        // Push to Dead Letter Queue (DLQ) for later manual review/recovery
                        await cache.EnqueueFailedInteractionAsync(json);
                    }
                    catch (Exception redisEx)
                    {
                        _logger.LogCritical(redisEx, "CRITICAL: Could not even push to DLQ. Data lost: {Json}", json);
                    }
                }
            }

            _logger.LogInformation("Batch persistence completed for {Count} items.", rawItems.Count);
        }

        private async Task ProcessQuotaQueueAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var cache = scope.ServiceProvider.GetRequiredService<ICacheService>();
            var db = scope.ServiceProvider.GetRequiredService<SmartBearServer.Data.AppDbContext>();

            var rawItems = await cache.DequeueQuotaUpdatesAsync(BatchSize);
            if (rawItems == null || rawItems.Count == 0) return;

            Console.WriteLine($"\x1b[38;5;22m[Redis] Syncing {rawItems.Count} quota updates to database...\x1b[0m");

            // Wrap all updates in a single transaction to prevent partial commits
            using var transaction = await db.Database.BeginTransactionAsync(stoppingToken);
            try
            {
                bool hasChanges = false;
                foreach (var json in rawItems)
                {
                    if (stoppingToken.IsCancellationRequested) break;

                    try
                    {
                        using var doc = JsonDocument.Parse(json);
                        var type = doc.RootElement.GetProperty("type").GetString();
                        var amount = doc.RootElement.GetProperty("amount").GetInt32();

                        if (type == "ConsumeCandyDaily")
                        {
                            var profileId = doc.RootElement.GetProperty("profileId").GetString();
                            // Atomic SQL Update to prevent lost updates
                            await db.Database.ExecuteSqlInterpolatedAsync($"UPDATE \"ChildProfiles\" SET \"daily_candy_balance\" = GREATEST(0, \"daily_candy_balance\" - {amount}) WHERE \"Id\" = {profileId}");
                            hasChanges = true;
                        }
                        else if (type == "ConsumeCandyPurchased")
                        {
                            var userId = doc.RootElement.GetProperty("userId").GetGuid();
                            // Atomic SQL Update to prevent lost updates
                            await db.Database.ExecuteSqlInterpolatedAsync($"UPDATE users SET smart_candies = GREATEST(0, smart_candies - {amount}) WHERE user_id = {userId}");
                            hasChanges = true;
                        }
                        else if (type == "ConsumeAudio")
                        {
                            var deviceId = doc.RootElement.GetProperty("deviceId").GetString();
                            var profileId = doc.RootElement.GetProperty("profileId").GetString();
                            var today = DateTime.UtcNow.Date;
                            
                            var usage = await db.DailyUsages.FirstOrDefaultAsync(u => u.DeviceId == deviceId && u.DateUtc == today);
                            if (usage == null)
                            {
                                usage = new DailyUsage
                                {
                                    DeviceId = deviceId!,
                                    ProfileId = profileId ?? string.Empty,
                                    DateUtc = today,
                                    AiRequestCount = 0,
                                    AudioSecondsUsed = amount
                                };
                                db.DailyUsages.Add(usage);
                            }
                            else
                            {
                                usage.AudioSecondsUsed += amount;
                            }
                            hasChanges = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to process quota record: {Json}. Moving to DLQ.", json);
                        
                        try
                        {
                            // Push to Dead Letter Queue (DLQ) for later manual review/recovery
                            await cache.EnqueueFailedInteractionAsync(json);
                        }
                        catch (Exception redisEx)
                        {
                            _logger.LogCritical(redisEx, "CRITICAL: Could not push quota update to DLQ. Data lost: {Json}", json);
                        }
                    }
                }

                if (hasChanges)
                {
                    await db.SaveChangesAsync();
                    await transaction.CommitAsync(stoppingToken);
                    _logger.LogInformation("Quota batch persistence completed.");
                }
                else
                {
                    await transaction.CommitAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Quota batch transaction failed. Rolling back.");
                await transaction.RollbackAsync(stoppingToken);
                
                // Re-enqueue all items for retry since the whole batch failed
                foreach (var json in rawItems)
                {
                    try 
                    { 
                        await cache.EnqueueQuotaUpdateRawAsync(json); 
                    }
                    catch (Exception reqEx) 
                    { 
                        _logger.LogCritical(reqEx, "CRITICAL: Failed to re-enqueue quota update after rollback. DATA LOST: {Json}", json);
                    }
                }
            }
        }
    }
}
