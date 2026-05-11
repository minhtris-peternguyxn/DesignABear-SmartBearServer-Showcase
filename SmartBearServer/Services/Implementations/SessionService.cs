using Microsoft.EntityFrameworkCore;
using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace SmartBearServer.Services.Implementations
{
    /// <summary>
    /// Implements chat session management and persistence logic.
    /// Follows the 3-layer architecture by delegating data operations to IChatSessionRepository.
    /// </summary>
    public class SessionService : ISessionService
    {
        private readonly IChatSessionRepository _sessionRepo;
        private readonly IAIService _ai;
        private readonly ICacheService _cache;
        private readonly IServiceScopeFactory _scopeFactory;
        private static readonly TimeSpan SessionTimeout = TimeSpan.FromMinutes(5);

        public SessionService(IChatSessionRepository sessionRepo, IAIService ai, ICacheService cache, IServiceScopeFactory scopeFactory)
        {
            _sessionRepo = sessionRepo;
            _ai = ai;
            _cache = cache;
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        /// Retrieves an existing active session or initializes a new one if the previous session expired.
        /// </summary>
        public async Task<ChatSession> GetOrCreateActiveSessionAsync(string profileId)
        {
            var cacheKey = $"session:active:v2:{profileId}";
            var now = DateTime.UtcNow;

            try
            {
                var cached = await _cache.GetAsync<ChatSession>(cacheKey);
                if (cached != null)
                {
                    // Check local timeout
                    if (now - cached.LastInteractionTime <= SessionTimeout)
                    {
                        return cached;
                    }
                    // If expired in cache, fall through to DB check
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SessionService] Cache error: {ex.Message}");
            }

            var activeSession = await _sessionRepo.GetActiveSessionAsync(profileId);

            if (activeSession != null)
            {
                if (now - activeSession.LastInteractionTime > SessionTimeout)
                {
                    activeSession.IsActive = false;
                    activeSession.EndTime = activeSession.LastInteractionTime;
                    await _sessionRepo.UpdateAsync(activeSession);
                    await _sessionRepo.SaveChangesAsync();
                    activeSession = null;
                }
            }

            if (activeSession == null)
            {
                activeSession = new ChatSession
                {
                    ProfileId = profileId,
                    StartTime = now,
                    LastInteractionTime = now,
                    IsActive = true,
                    Title = "Phiên trò chuyện mới"
                };
                await _sessionRepo.AddAsync(activeSession);
                await _sessionRepo.SaveChangesAsync();
            }

            // Sync to cache
            await _cache.SetAsync(cacheKey, activeSession, SessionTimeout);
            return activeSession;
        }

        /// <summary>
        /// Persists a single interaction to the database and updates the session activity.
        /// Triggers asynchronous summarization at specific message milestones.
        /// </summary>
        public async Task SaveInteractionAsync(PendingInteractionDto interaction)
        {
            var history = new InteractionHistory
            {
                DeviceId = interaction.DeviceId,
                ProfileId = interaction.ProfileId,
                Request = interaction.Request,
                Response = interaction.Response,
                SessionId = interaction.SessionId,
                Timestamp = interaction.Timestamp,
                IsSafe = interaction.IsSafe,
                SafetyViolationCategory = interaction.SafetyCategory
            };

            var session = await _sessionRepo.GetByIdAsync(interaction.SessionId);
            if (session != null)
            {
                session.LastInteractionTime = DateTime.UtcNow;
                session.Interactions.Add(history);

                // Update cache as well to extend the session and keep LastInteractionTime fresh
                var cacheKey = $"session:active:v2:{interaction.ProfileId}";
                await _cache.SetAsync(cacheKey, session, SessionTimeout);

                // TODO: Re-enable AI summarization when budget allows
                // Real-time AI summarization disabled to save API costs
                // int msgCount = session.Interactions.Count;
                // if (msgCount == 3 || msgCount == 5 || msgCount == 10 || msgCount == 20)
                // {
                //     _ = Task.Run(async () =>
                //     {
                //         using var scope = _scopeFactory.CreateScope();
                //         var repo = scope.ServiceProvider.GetRequiredService<IChatSessionRepository>();
                //         var ai = scope.ServiceProvider.GetRequiredService<IAIService>();
                //         var cache = scope.ServiceProvider.GetRequiredService<ICacheService>();
                //         try
                //         {
                //             var s = await repo.GetByIdAsync(interaction.SessionId);
                //             if (s == null) return;
                //             var (title, summary, category) = await ai.SummarizeSessionAsync(s.Interactions);
                //             s.Title = title;
                //             s.Summary = summary;
                //             s.Category = category;
                //             await repo.UpdateAsync(s);
                //             await repo.SaveChangesAsync();
                //             await cache.SetAsync($"session:active:v2:{s.ProfileId}", s, SessionTimeout);
                //             Console.WriteLine($"[SessionService] Background summary updated for session {s.Id}");
                //         }
                //         catch (Exception ex)
                //         {
                //             Console.WriteLine($"[SessionService] Async summary error: {ex.Message}");
                //         }
                //     });
                // }

                await _sessionRepo.UpdateAsync(session);
                await _sessionRepo.SaveChangesAsync();
            }
        }
    }
}
