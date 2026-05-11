using SmartBearServer.Model;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SmartBearServer.Services.Interfaces;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Infrastructure;

namespace SmartBearServer.Services.Implementations
{
    public class SubscriptionLifecycleService : ISubscriptionLifecycleService
    {
        private readonly IUserRepository _userRepo;
        private readonly IChildProfileRepository _profileRepo;
        private readonly IConfiguration _config;

        public SubscriptionLifecycleService(IUserRepository userRepo, IChildProfileRepository profileRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _profileRepo = profileRepo;
            _config = config;
        }

        public void ActivateOrRenew(ChildProfile profile, int subscriptionPlanId = 2)
        {
            if (profile.ParentUser == null) return;
            
            var user = profile.ParentUser;
            user.IsPro = true;
            user.SubscriptionPlanId = subscriptionPlanId;
            var now = DateTime.UtcNow;
            
            var renewalAnchor = user.ProExpiresAt.HasValue && user.ProExpiresAt > now
                ? user.ProExpiresAt.Value
                : now;

            user.ProExpiresAt = renewalAnchor.AddDays(30);
        }

        public SubscriptionStatus RefreshStatus(ChildProfile profile)
        {
            if (profile.ParentUser == null) return SubscriptionStatus.Trial;
            return profile.ParentUser.SubscriptionStatus;
        }

        public bool IsAccessible(ChildProfile profile)
        {
            if (profile.ParentUser == null) return false;
            var status = profile.ParentUser.SubscriptionStatus;
            return status == SubscriptionStatus.Active || status == SubscriptionStatus.Grace;
        }

        public async Task<bool> UpdateTtsPreferenceAsync(Guid userId, string provider)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return false;

            if (!user.IsPro && provider == Constants.TtsProviders.ElevenLabs) return false;

            user.PreferredTtsProvider = provider;
            await _userRepo.UpdateAsync(user);
            return true;
        }

        public async Task<bool> CancelProSubscriptionAsync(Guid userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return false;

            user.IsPro = false;
            user.ProExpiresAt = null;
            user.SubscriptionPlanId = null;
            user.PreferredTtsProvider = Constants.TtsProviders.Gcp;
            
            // Revert credits using external configuration
            user.SmartCandies = int.Parse(_config[Constants.ConfigurationKeys.InitialSmartCandies] ?? "50");

            // Reset all children to Basic plan candy limit
            var children = await _profileRepo.GetForUserAsync(userId);
            foreach (var c in children)
            {
                c.DailyCandyLimit = 10;
                c.DailyCandyBalance = 10;
                await _profileRepo.UpdateAsync(c);
            }

            await _userRepo.UpdateAsync(user);
            return true;
        }
    }
}
