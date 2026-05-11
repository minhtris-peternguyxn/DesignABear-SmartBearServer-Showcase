using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartBearServer.Services.Interfaces;
using SmartBearServer.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartBearServer.Services
{
    /// <summary>
    /// Handles the daily refill of "Kẹo ngày" for all children.
    /// Runs once at startup (Sweep) and then exactly at 0:00 AM daily.
    /// </summary>
    public class QuotaResetBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<QuotaResetBackgroundService> _logger;

        public QuotaResetBackgroundService(IServiceProvider services, ILogger<QuotaResetBackgroundService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("QuotaResetBackgroundService started.");

            // 1. Startup Sweep
            Console.WriteLine("\x1b[35m[Startup] Triggering initial Quota Reset Sweep...\x1b[0m");
            await PerformResetAsync();

            // 2. Continuous Loop for 0:00 Daily Reset
            while (!stoppingToken.IsCancellationRequested)
            {
                var nowVn = DateTime.UtcNow.AddHours(7);
                var nextRunVn = nowVn.Date.AddDays(1); // Next midnight in VN
                var delay = nextRunVn - nowVn;

                _logger.LogInformation("QuotaResetBackgroundService sleeping for {Delay} until next midnight (VN time).", delay);

                try
                {
                    await Task.Delay(delay, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                if (!stoppingToken.IsCancellationRequested)
                {
                    await PerformResetAsync();
                }
            }
        }

        private async Task PerformResetAsync()
        {
            _logger.LogInformation("Starting Quota Reset Sweep...");
            
            try
            {
                using var scope = _services.CreateScope();
                var quotaService = scope.ServiceProvider.GetRequiredService<IUsageQuotaService>();
                
                int count = await quotaService.ResetAllDailyQuotasAsync();
                
                _logger.LogInformation("Quota Reset Sweep completed. Refilled {Count} profiles.", count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Quota Reset Sweep.");
            }
        }
    }
}
