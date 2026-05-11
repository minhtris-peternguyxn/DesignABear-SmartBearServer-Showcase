using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using SmartBearServer.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartBearServer.Services
{
    public class CreditResetWorker : BackgroundService
    {
        private readonly ILogger<CreditResetWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public CreditResetWorker(ILogger<CreditResetWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await PerformResetTaskAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while resetting credits.");
                }

                // Run check every hour
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private async Task PerformResetTaskAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var nowUtc = DateTime.UtcNow;

            // Find free users who haven't received their daily reset today
            var usersToReset = await dbContext.Users
                .Where(u => !u.IsPro && u.FreeDailyCreditsLastReset.Date < nowUtc.Date)
                .ToListAsync(cancellationToken);

            if (usersToReset.Any())
            {
                foreach (var user in usersToReset)
                {
                    user.SmartCandies = 10;
                    user.FreeDailyCreditsLastReset = nowUtc;
                }

                await dbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"[CreditReset] Successfully reset credits for {usersToReset.Count} free users.");
            }
        }
    }
}
