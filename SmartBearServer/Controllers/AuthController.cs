using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Model.DTOs;
using SmartBearServer.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;

namespace SmartBearServer.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// Exposes POST endpoints for parent mobile app registration and login.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return HandleResult(Result<object>.Failure(new Error("BadRequest", "Dữ liệu yêu cầu không hợp lệ.")));
            }

            var response = await _authService.RegisterAsync(request);
            if (!response.Success)
            {
                return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("BadRequest", response.ErrorMessage ?? "Registration failed")));
            }

            return HandleResult(Result<object>.Success(response));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return HandleResult(Result<object>.Failure(new Error("BadRequest", "Dữ liệu yêu cầu không hợp lệ.")));
            }

            var response = await _authService.LoginAsync(request);
            if (!response.Success)
            {
                return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", response.ErrorMessage ?? "Login failed")));
            }

            return HandleResult(Result<object>.Success(response));
        }

        [HttpPost("google")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithGoogle([FromBody] GoogleAuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return HandleResult(Result<object>.Failure(new Error("BadRequest", "Dữ liệu yêu cầu không hợp lệ.")));
            }

            var response = await _authService.LoginWithGoogleAsync(request.IdToken);
            if (!response.Success)
            {
                return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", response.ErrorMessage ?? "Google login failed")));
            }

            return HandleResult(Result<object>.Success(response));
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return HandleResult(Result<object>.Failure(new Error("BadRequest", "Refresh token is required.")));
            }

            var response = await _authService.RefreshTokenAsync(refreshToken);
            if (!response.Success)
            {
                return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", response.ErrorMessage ?? "Token refresh failed")));
            }

            return HandleResult(Result<object>.Success(response));
        }
    }
}
