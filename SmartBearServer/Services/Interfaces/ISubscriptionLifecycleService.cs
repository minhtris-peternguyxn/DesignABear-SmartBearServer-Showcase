using SmartBearServer.Model;

namespace SmartBearServer.Services.Interfaces
{
    public interface ISubscriptionLifecycleService
    {
        void ActivateOrRenew(ChildProfile profile, int subscriptionPlanId = 2);
        SubscriptionStatus RefreshStatus(ChildProfile profile);
        bool IsAccessible(ChildProfile profile);
        Task<bool> UpdateTtsPreferenceAsync(Guid userId, string provider);
        Task<bool> CancelProSubscriptionAsync(Guid userId);
    }
}
