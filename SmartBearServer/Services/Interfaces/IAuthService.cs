using SmartBearServer.Model.DTOs;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> LoginWithGoogleAsync(string idToken);
        Task<AuthResponse> RefreshTokenAsync(string refreshToken);
    }
}
