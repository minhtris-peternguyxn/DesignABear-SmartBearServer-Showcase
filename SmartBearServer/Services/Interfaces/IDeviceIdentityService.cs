using SmartBearServer.Model;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    public interface IDeviceIdentityService
    {
        Task<IssueDeviceTokenResponse> IssueTokenAsync(string deviceId, int validDays = 365);
        Task<bool> ValidateTokenAsync(string deviceId, string rawToken);
        Task<bool> RevokeTokenAsync(string deviceId, string tokenId);
    }
}
