using SmartBearServer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Interfaces
{
    public interface ISubscriptionPlanRepository
    {
        Task<List<SubscriptionPlan>> GetActivePlansAsync();
        Task<SubscriptionPlan?> GetByNameAsync(string planName);
        Task<SubscriptionPlan?> GetByIdAsync(int planId);
    }
}
