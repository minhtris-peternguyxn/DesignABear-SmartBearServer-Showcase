using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartBearServer.Data;
using SmartBearServer.Infrastructure;
using SmartBearServer.Services.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartBearServer.Services
{
    public class OrderReconciliationWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OrderReconciliationWorker> _logger;

        public OrderReconciliationWorker(IServiceProvider serviceProvider, ILogger<OrderReconciliationWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("OrderReconciliationWorker is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ReconcileOrdersAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred during order reconciliation.");
                }

                // Chạy định kỳ mỗi 5 phút
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        private async Task ReconcileOrdersAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var payOSService = scope.ServiceProvider.GetRequiredService<IPayOSService>();
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

            var threshold = DateTime.UtcNow.AddHours(-Constants.Payment.OrderExpiryThresholdHours);
            var pendingOrders = await context.PendingOrders
                .Where(o => !o.IsFulfilled && o.CreatedAtUtc > threshold)
                .ToListAsync();

            foreach (var pending in pendingOrders)
            {
                try
                {
                    var paymentInfo = await payOSService.GetPaymentLinkInformation(pending.OrderCode);
                    if (paymentInfo.status == "PAID")
                    {
                        Console.WriteLine($"[ReconciliationWorker] Order {pending.OrderCode} is PAID on PayOS. Triggering fulfillment...");
                        await paymentService.FulfillOrderAsync(pending.OrderCode);
                        _logger.LogInformation("Order {OrderCode} fulfilled via Reconciliation Worker.", pending.OrderCode);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to reconcile order {OrderCode}", pending.OrderCode);
                }
            }
        }
    }
}
