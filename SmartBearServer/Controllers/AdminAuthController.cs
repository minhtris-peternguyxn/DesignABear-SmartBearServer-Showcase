using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SmartBearServer.Model.DTOs;
using SmartBearServer.Services.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartBearServer.Controllers
{
    /// <summary>
    /// Handles cookie-based authentication for the Blazor admin dashboard.
    /// Operates independently from the JWT-based API authentication scheme.
    /// </summary>
    [ApiController]
    [Route("api/admin/auth")]
    public class AdminAuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AdminAuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Processes administrator sign-in and issues an admin session cookie.
        /// </summary>
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromForm] string email, [FromForm] string password)
        {
            var response = await _authService.LoginAsync(new LoginRequest
            {
                Email = email,
                Password = password
            });

            // Validate credentials and enforce that only Role 1 (Master Admin) can access the dashboard.
            if (response != null && response.Success && response.RoleId == 1)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Role, response.RoleId.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, response.UserId ?? email)
                };

                var identity = new ClaimsIdentity(claims, "AdminCookies");
                await HttpContext.SignInAsync("AdminCookies", new ClaimsPrincipal(identity));

                return Redirect("/admin/dashboard");
            }

            return Redirect("/admin/login?error=invalid");
        }

        /// <summary>
        /// Signs the administrator out and clears the session cookie.
        /// </summary>
        [HttpGet("signout")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync("AdminCookies");
            return Redirect("/admin/login");
        }
    }
}
