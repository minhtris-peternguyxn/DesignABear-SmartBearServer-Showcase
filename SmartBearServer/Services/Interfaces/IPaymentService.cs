using Net.payOS.Types;
using SmartBearServer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<List<object>> GetActivePlansAsync();
        Task<object> GetUserSubscriptionStatusAsync(Guid userId);
        Task<CreatePaymentResult> CreatePaymentLinkAsync(Guid userId, string? voucherCode, int planType);
        Task FulfillOrderAsync(long orderCode);
        Task<object> ValidateVoucherAsync(string code, int originalAmount);
        Task<CreatePaymentResult> CreateCandyPaymentLinkAsync(Guid userId, int candyPackId, string? voucherCode);
    }
}
