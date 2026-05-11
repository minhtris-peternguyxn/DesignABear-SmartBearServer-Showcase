using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartBearServer.Data;
using SmartBearServer.Hubs;
using SmartBearServer.Services.Interfaces;
using SmartBearServer.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartBearServer.Services
{
    /// <summary>
    /// Background service that polls the database for scheduled alarms and triggers them via SignalR.
    /// </summary>
    public class SmartAlarmBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SmartAlarmBackgroundService> _logger;

        public SmartAlarmBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<SmartAlarmBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("\x1b[35m[SmartAlarm] Background Service INITIALIZING...\x1b[0m");
            
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.UtcNow.AddHours(7);
                    Console.WriteLine($"\x1b[33m[SmartAlarm] Heartbeat: {now:HH:mm:ss} (UTC+7) - Polling...\x1b[0m");
                    
                    await CheckAndFireAlarmsAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\x1b[31m[SmartAlarm] CRITICAL ERROR in loop: {ex.Message}\x1b[0m");
                }

                // Sleep until the start of the next minute for better precision
                var sleepTime = 60 - DateTime.UtcNow.Second;
                if (sleepTime <= 0) sleepTime = 60;
                await Task.Delay(TimeSpan.FromSeconds(sleepTime), stoppingToken);
            }
            
            Console.WriteLine("\x1b[31m[SmartAlarm] Background Service STOPPED.\x1b[0m");
        }

        private async Task CheckAndFireAlarmsAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            // 1. Log Status Summary
            var onlineDevices = await db.Devices
                .Include(d => d.Profile)
                .Where(d => d.Status == "Online")
                .ToListAsync();

            if (onlineDevices.Any())
            {
                Console.WriteLine($"\x1b[36m[SmartAlarm] Status: {onlineDevices.Count} bear(s) online.\x1b[0m");
                foreach (var d in onlineDevices)
                {
                    var p = d.Profile;
                    if (p != null)
                    {
                        Console.WriteLine($"\x1b[36m             - {d.Nickname}: {p.Name} ({p.DailyCandyBalance}/{p.DailyCandyLimit} candies)\x1b[0m");
                    }
                }
            }

            // 2. Process Alarms
            var alarmService = scope.ServiceProvider.GetRequiredService<ISmartAlarmService>();
            try 
            {
                await alarmService.ProcessPendingAlarmsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\x1b[31m[SmartAlarm] Error processing alarms: {ex.Message}\x1b[0m");
            }
        }
    }
}
