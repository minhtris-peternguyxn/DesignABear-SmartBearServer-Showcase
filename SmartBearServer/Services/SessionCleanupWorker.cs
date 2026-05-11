using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Infrastructure;
using SmartBearServer.Services.Interfaces;
using SmartBearServer.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartBearServer.Services
{
    public class SessionCleanupWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SessionCleanupWorker> _logger;

        public SessionCleanupWorker(IServiceProvider serviceProvider, ILogger<SessionCleanupWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessTimedOutSessionsAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred during session cleanup and summarization.");
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Check every 5 minutes
            }
        }

        private async Task ProcessTimedOutSessionsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // TODO: Re-enable AI summarization when budget allows
            // var ai = scope.ServiceProvider.GetRequiredService<IAIService>();
            // var gemini = scope.ServiceProvider.GetRequiredService<GeminiClient>();

            var now = DateTime.UtcNow;
            var timeout = TimeSpan.FromMinutes(20);

            // 1. Mark idle sessions as inactive
            var idleSessions = await context.ChatSessions
                .Where(s => s.IsActive && (now - s.LastInteractionTime) > timeout)
                .ToListAsync();

            foreach (var session in idleSessions)
            {
                session.IsActive = false;
                session.EndTime = session.LastInteractionTime;
                _logger.LogInformation("Marked session {SessionId} as inactive due to timeout.", session.Id);
            }

            if (idleSessions.Any()) await context.SaveChangesAsync();

            // 2. Process sessions that need summary (IsActive=false, Summary=null)
            var sessionsToSummarize = await context.ChatSessions
                .Where(s => !s.IsActive && s.Summary == null)
                .Include(s => s.Interactions)
                .OrderBy(s => s.StartTime)
                .Take(5) // Process a few at a time
                .ToListAsync();

            foreach (var session in sessionsToSummarize)
            {
                if (!session.Interactions.Any())
                {
                    session.Summary = "Không có tương tác nào được ghi nhận.";
                    session.Category = "Trống";
                    continue;
                }

                // TODO: Re-enable AI summarization when budget allows
                // try
                // {
                //     await GenerateSessionSummaryAsync(session, gemini);
                //     _logger.LogInformation("Generated AI summary for session {SessionId}.", session.Id);
                // }
                // catch (Exception ex)
                // {
                //     _logger.LogWarning(ex, "Failed to summarize session {SessionId}.", session.Id);
                // }

                // Fallback: assign generic metadata without calling AI
                session.Title = "Cuộc trò chuyện của bé";
                session.Summary = "Bé và Gấu đã có một cuộc trò chuyện vui vẻ.";
                session.Category = "Trò chuyện";
                _logger.LogInformation("Assigned fallback summary for session {SessionId} (AI disabled).", session.Id);
            }

            if (sessionsToSummarize.Any()) await context.SaveChangesAsync();
        }

        private async Task GenerateSessionSummaryAsync(ChatSession session, GeminiClient gemini)
        {
            var transcript = string.Join("\n", session.Interactions.OrderBy(i => i.Timestamp)
                .Select(i => $"Child: {i.Request}\nBear: {i.Response}"));

            var prompt = $@"
Analyze the following transcript of a conversation between a child and a smart companion bear. 
Generate a JSON output with the following fields:
- title: A short, catchy title in Vietnamese (max 10 words).
- summary: A brief summary in Vietnamese of what they talked about or learned (max 2 sentences).
- category: A one-word category in Vietnamese (e.g., 'Học tập', 'Kể chuyện', 'Giải trí', 'Hỏi đáp').

Transcript:
{transcript}

Response ONLY with valid JSON.
";

            var result = await gemini.Generate(prompt);
            
            // Basic cleaning of Markdown JSON blocks if present
            if (result.Contains("```json"))
            {
                result = result.Split("```json")[1].Split("```")[0];
            }

            try
            {
                var summaryObj = System.Text.Json.JsonSerializer.Deserialize<SessionSummaryResult>(result.Trim(), new System.Text.Json.JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true 
                });

                session.Title = summaryObj.Title;
                session.Summary = summaryObj.Summary;
                session.Category = summaryObj.Category;
            }
            catch
            {
                // Fallback if AI fails to return valid JSON
                session.Title = "Cuộc trò chuyện của bé";
                session.Summary = "Bé và Gấu đã có một cuộc trò chuyện vui vẻ.";
                session.Category = "Trò chuyện";
            }
        }

        private class SessionSummaryResult
        {
            public string Title { get; set; }
            public string Summary { get; set; }
            public string Category { get; set; }
        }
    }
}
