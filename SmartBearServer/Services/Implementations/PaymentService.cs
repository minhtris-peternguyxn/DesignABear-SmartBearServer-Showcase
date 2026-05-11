using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Net.payOS.Types;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Services.Interfaces;
using SmartBearServer.Services.Strategies;
using SmartBearServer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SmartBearServer.Hubs;

namespace SmartBearServer.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly ISubscriptionPlanRepository _planRepo;
        private readonly IUserRepository _userRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IVoucherRepository _voucherRepo;
        private readonly IChildProfileRepository _childRepo;
        private readonly IDeviceRepository _deviceRepo;
        private readonly IPayOSService _payOSService;
        private readonly ICacheService _cache;
        private readonly IConfiguration _config;
        private readonly AppDbContext _db;
        private readonly VoucherStrategyFactory _voucherFactory;

        public PaymentService(
            ISubscriptionPlanRepository planRepo,
            IUserRepository userRepo,
            IOrderRepository orderRepo,
            IVoucherRepository voucherRepo,
            IChildProfileRepository childRepo,
            IDeviceRepository deviceRepo,
            IPayOSService payOSService,
            ICacheService cache,
            IConfiguration config,
            AppDbContext db,
            VoucherStrategyFactory voucherFactory)
        {
            _planRepo = planRepo;
            _userRepo = userRepo;
            _orderRepo = orderRepo;
            _voucherRepo = voucherRepo;
            _childRepo = childRepo;
            _deviceRepo = deviceRepo;
            _payOSService = payOSService;
            _cache = cache;
            _config = config;
            _db = db;
            _voucherFactory = voucherFactory;
        }

        public async Task<List<object>> GetActivePlansAsync()
        {
            var plans = await _planRepo.GetActivePlansAsync();
            return plans.Select(p => (object)new
            {
                p.Id,
                p.Name,
                p.Description,
                p.PriceMonthly,
                p.Price,
                p.CanPlayMusic,
                p.CanTellStoriesOnUserSpeech,
                p.CanUseLearningAI,
                PlanType = p.PlanType.ToString()
            }).ToList();
        }

        public async Task<object> GetUserSubscriptionStatusAsync(Guid userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found.");

            // Removed Polling Loop: Reconciliation is now handled by OrderReconciliationWorker

            // Expiration Check
            if (user.IsPro && user.ProExpiresAt.HasValue && user.ProExpiresAt.Value < DateTime.UtcNow)
            {
                user.IsPro = false;
                user.SubscriptionPlanId = null;
                user.PreferredTtsProvider = Constants.TtsProviders.Gcp;
                await _userRepo.UpdateAsync(user);
            }

            // Instant Reconciliation: Check for recent pending orders to provide immediate feedback
            var threshold = DateTime.UtcNow.AddHours(-1); // Only check very recent orders
            var pendingOrders = await _db.PendingOrders
                .Where(o => o.UserId == userId && !o.IsFulfilled && o.CreatedAtUtc > threshold)
                .ToListAsync();

            foreach (var pending in pendingOrders)
            {
                try
                {
                    var paymentInfo = await _payOSService.GetPaymentLinkInformation(pending.OrderCode);
                    if (paymentInfo.status == "PAID")
                    {
                        await FulfillOrderAsync(pending.OrderCode);
                        // Reload user to get fresh data after fulfillment
                        user = await _userRepo.GetByIdAsync(userId) ?? user;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[PaymentService] Instant reconciliation failed for order {pending.OrderCode}: {ex.Message}");
                }
            }

            return new
            {
                isPro = user.IsPro,
                planName = user.IsPro ? (user.SubscriptionPlan?.Name ?? "Thành viên Pro") : "Gói Cơ Bản",
                proExpiresAt = user.ProExpiresAt,
                preferredTtsProvider = user.PreferredTtsProvider,
                smartCandies = user.SmartCandies,
                smartCandiesDisplay = user.SmartCandies.ToString()
            };
        }

        public async Task<CreatePaymentResult> CreatePaymentLinkAsync(Guid userId, string? voucherCode, int planType)
        {
            var plan = (await _planRepo.GetActivePlansAsync())
                .FirstOrDefault(p => (int)p.PlanType == planType);
            
            if (plan == null) throw new Exception("Gói dịch vụ không khả dụng.");

            int amount = (int)plan.PriceMonthly;

            if (!string.IsNullOrEmpty(voucherCode))
            {
                var voucher = await _voucherRepo.GetActiveByCodeAsync(voucherCode);
                if (voucher == null) throw new Exception("Mã giảm giá không hợp lệ.");
                amount = CalculateDiscountedPrice(amount, voucher);
            }

            long orderCode = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() % 1_000_000_000L;
            
            // Dynamic description for PayOS (no accents/special chars recommended)
            string description = plan.PlanType switch
            {
                SubscriptionPlanType.VIP => "GoiUltra_SmartBear",
                SubscriptionPlanType.Premium => "GoiNangCao_SmartBear",
                _ => "GoiDichVu_SmartBear"
            };

            var item = new ItemData(plan.Name + " SmartBear", 1, amount);
            var paymentData = new PaymentData(
                orderCode,
                amount,
                description,
                new List<ItemData> { item },
                _config[Constants.ConfigurationKeys.PayOsCancelUrl] ?? "https://smartbear.vn/cancel",
                _config[Constants.ConfigurationKeys.PayOsSuccessUrl] ?? "https://smartbear.vn/success"
            );

            var result = await _payOSService.CreatePaymentLink(paymentData);

            await _orderRepo.AddAsync(new PendingOrder
            {
                OrderCode = orderCode,
                UserId = userId,
                PlanName = plan.Name,
                PlanType = (int)plan.PlanType,
                OrderType = "Plan",
                Amount = amount,
                VoucherCode = voucherCode 
            });

            return result;
        }

        public async Task<CreatePaymentResult> CreateCandyPaymentLinkAsync(Guid userId, int candyPackId, string? voucherCode)
        {
            var pack = CandyPackProvider.GetById(candyPackId);
            if (pack == null) throw new Exception("Gói kẹo không hợp lệ.");

            int amount = pack.Price;

            if (!string.IsNullOrEmpty(voucherCode))
            {
                var voucher = await _voucherRepo.GetActiveByCodeAsync(voucherCode);
                if (voucher == null) throw new Exception("Mã giảm giá không hợp lệ.");
                amount = CalculateDiscountedPrice(amount, voucher);
            }

            long orderCode = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() % 1_000_000_000L;
            var item = new ItemData(pack.Name, 1, amount);
            var paymentData = new PaymentData(
                orderCode,
                amount,
                "MuaKeo_SmartBear",
                new List<ItemData> { item },
                _config[Constants.ConfigurationKeys.PayOsCancelUrl] ?? "https://smartbear.vn/cancel",
                _config[Constants.ConfigurationKeys.PayOsSuccessUrl] ?? "https://smartbear.vn/success"
            );

            var result = await _payOSService.CreatePaymentLink(paymentData);

            await _orderRepo.AddAsync(new PendingOrder
            {
                OrderCode = orderCode,
                UserId = userId,
                PlanName = pack.Name,
                OrderType = "Candy",
                CandyQuantity = pack.Count,
                Amount = amount,
                VoucherCode = voucherCode // Save to decrement later
            });

            return result;
        }

        public async Task FulfillOrderAsync(long orderCode)
        {
            var pending = await _orderRepo.GetByCodeAsync(orderCode);
            if (pending == null) 
            {
                Console.WriteLine($"[PaymentService] Fulfillment skipped: Order {orderCode} not found.");
                return;
            }

            if (pending.IsFulfilled)
            {
                Console.WriteLine($"[PaymentService] Fulfillment skipped: Order {orderCode} is already fulfilled.");
                return;
            }

            var user = await _userRepo.GetByIdAsync(pending.UserId);
            if (user == null)
            {
                Console.WriteLine($"[PaymentService] Fulfillment FAILED: User {pending.UserId} for order {orderCode} not found.");
                return;
            }

            Console.WriteLine($"[PaymentService] Starting fulfillment for order {orderCode} (Type: {pending.OrderType}, User: {user.Email})");

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var cacheKeysToInvalidate = new List<string>();

                // 1. Process Order logic
                if (pending.OrderType == "Candy")
                {
                    // ATOMIC INCREMENT: Prevents race conditions with ChatPersistenceWorker
                    int amountToAdd = pending.CandyQuantity;
                    await _db.Database.ExecuteSqlInterpolatedAsync($"UPDATE users SET smart_candies = smart_candies + {amountToAdd} WHERE user_id = {user.UserId}");
                    
                    cacheKeysToInvalidate.Add($"quota:purchased:candy:{user.UserId}");
                    Console.WriteLine($"[PaymentService] Queued {amountToAdd} candies for user {user.UserId}");
                }
                else // Plan
                {
                    SubscriptionPlan? plan = null;
                    
                    // 1. Primary: Match by PlanType 
                    if (pending.PlanType.HasValue)
                    {
                        var activePlans = await _planRepo.GetActivePlansAsync();
                        plan = activePlans.FirstOrDefault(p => (int)p.PlanType == pending.PlanType.Value);
                    }
                    
                    // 2. Secondary: Match by exact Name (Legacy support)
                    if (plan == null)
                    {
                        plan = await _planRepo.GetByNameAsync(pending.PlanName);
                    }

                    // 3. Last Resort: Match by Price if possible, or fallback to first non-basic
                    if (plan == null)
                    {
                        var activePlans = await _planRepo.GetActivePlansAsync();
                        
                        // Try to find a plan with matching price (accounting for possible rounding)
                        plan = activePlans.FirstOrDefault(p => Math.Abs(p.PriceMonthly - pending.Amount) < 100);
                        
                        if (plan == null)
                        {
                            plan = activePlans.FirstOrDefault(p => p.PlanType != SubscriptionPlanType.Basic);
                            if (plan != null)
                                Console.WriteLine($"[PaymentService] Order {orderCode}: Plan '{pending.PlanName}' (Type={pending.PlanType}) not found. Falling back to plan: {plan.Name}");
                        }
                    }

                    if (plan != null)
                    {
                        int trialDays = int.Parse(_config[Constants.ConfigurationKeys.ProTrialDays] ?? "30");
                        
                        user.IsPro = true;
                        user.SubscriptionPlanId = plan.Id;

                        // Extension Logic: If already Pro and not expired, extend from existing expiry
                        var now = DateTime.UtcNow;
                        var renewalAnchor = (user.ProExpiresAt.HasValue && user.ProExpiresAt > now) 
                            ? user.ProExpiresAt.Value 
                            : now;
                        
                        user.ProExpiresAt = renewalAnchor.AddDays(trialDays);

                        // Update all children linked to this user
                        var children = await _childRepo.GetForUserAsync(user.UserId);
                        foreach (var c in children)
                        {
                            c.DailyCandyBalance = plan.DailyCandyLimit;
                            c.DailyCandyLimit = plan.DailyCandyLimit;
                            cacheKeysToInvalidate.Add($"quota:daily:candy:{c.Id}");
                        }
                        
                        await _userRepo.UpdateAsync(user);
                        Console.WriteLine($"[PaymentService] Activated/Extended plan {plan.Name} for user {user.UserId}. New expiry: {user.ProExpiresAt}");

                        // Invalidate bear:profile:config cache for all devices owned by this user
                   
                        var userDevices = await _deviceRepo.GetDevicesWithDetailsByUserAsync(user.UserId);
                        foreach (var d in userDevices)
                        {
                            var normSn = LLMHub.NormalizeSerial(d.SerialNumber);
                            var normId = LLMHub.NormalizeSerial(d.DeviceId);
                            cacheKeysToInvalidate.Add($"bear:profile:config:{normSn}");
                            cacheKeysToInvalidate.Add($"bear:profile:config:{normId}");
                            cacheKeysToInvalidate.Add($"device:config:sn:{normSn}");
                            cacheKeysToInvalidate.Add($"device:config:id:{normId}");
                        }
                    }
                }

                // 2. Decrement Voucher (Atomic increment of usage)
                if (!string.IsNullOrEmpty(pending.VoucherCode))
                {
                    var voucher = await _voucherRepo.GetActiveByCodeAsync(pending.VoucherCode);
                    if (voucher != null)
                    {
                        await _db.Database.ExecuteSqlInterpolatedAsync($"UPDATE vouchers SET current_usage = current_usage + 1 WHERE id = {voucher.Id}");
                        Console.WriteLine($"[PaymentService] Incremented usage for voucher {pending.VoucherCode}");
                    }
                }

                // 3. Mark Order as Fulfilled
                pending.IsFulfilled = true;
                await _orderRepo.UpdateAsync(pending);

                // Commit Transaction
                await transaction.CommitAsync();
                Console.WriteLine($"[PaymentService] Transaction committed for order {orderCode}");

                // 4. Invalidate Redis cache AFTER successful DB commit
                foreach (var key in cacheKeysToInvalidate)
                {
                    try 
                    { 
                        await _cache.RemoveAsync(key);
                        Console.WriteLine($"[PaymentService] Invalidated cache key: {key}");
                    }
                    catch (Exception ex) 
                    { 
                        Console.WriteLine($"[PaymentService] Warning: Failed to invalidate cache key {key}: {ex.Message}"); 
                    }
                }
                
                Console.WriteLine($"[PaymentService] Order {orderCode} fulfillment COMPLETED successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"[PaymentService] CRITICAL: Fulfillment failed for order {orderCode}. Transaction rolled back. Error: {ex.Message}");
                throw; // Rethrow to let caller (webhook/worker) handle/log
            }
        }

        public async Task<object> ValidateVoucherAsync(string code, int originalAmount)
        {
            var voucher = await _voucherRepo.GetActiveByCodeAsync(code);
            if (voucher == null) throw new Exception("Mã giảm giá không hợp lệ.");

            int finalAmount = CalculateDiscountedPrice(originalAmount, voucher);
            int discountAmount = originalAmount - finalAmount;

            return new
            {
                originalAmount,
                discountAmount,
                finalAmount,
                message = "Áp dụng mã thành công!"
            };
        }

        private int CalculateDiscountedPrice(int originalPrice, Voucher voucher)
        {
            if (voucher.MaxUsage.HasValue && voucher.CurrentUsage >= voucher.MaxUsage.Value)
                throw new Exception("Mã giảm giá đã hết lượt sử dụng.");

            if (voucher.ExpiryDate.HasValue && voucher.ExpiryDate.Value < DateTime.UtcNow)
                throw new Exception("Mã giảm giá đã hết hạn.");

            var strategy = _voucherFactory.GetStrategy(voucher);
            int finalPrice = strategy.CalculateDiscount(originalPrice, voucher);

            return Math.Max(Constants.Payment.MinPayOsAmount, finalPrice);
        }
    }
}
