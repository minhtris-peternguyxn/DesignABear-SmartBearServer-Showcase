using SmartBearServer.Model;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    public interface IUsageQuotaService
    {
        Task<(bool Allowed, string Message, string? ConsumptionType)> CanConsumeAiAsync(Device device);
        Task<bool> ConsumeAiAsync(Device device, string consumptionType);
        Task<(bool Success, string? ConsumptionType)> TryConsumeAiAsync(Device device);
        Task RefundAiAsync(Device device, string consumptionType);
        
        /// <summary>
        /// Warms up the Redis cache for quota to ensure zero DB hits during requests.
        /// </summary>
        Task WarmupQuotaAsync(Device device);

        Task<int> ResetAllDailyQuotasAsync();
        Task<(bool Allowed, string Message)> CanConsumeAudioAsync(Device device, int additionalAudioSeconds);
        Task ConsumeAudioAsync(string deviceId, int audioSeconds, string? profileId = null);
    }
}
