using System.ComponentModel.DataAnnotations;

namespace SmartBearServer.Model.DTOs
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }
    }

    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string ErrorMessage { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }
    }

    public class GoogleAuthRequest
    {
        [Required]
        public string IdToken { get; set; }
    }
}
