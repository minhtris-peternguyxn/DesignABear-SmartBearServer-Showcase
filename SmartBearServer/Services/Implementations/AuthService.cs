using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Model.DTOs;
using SmartBearServer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SmartBearServer.Services.Interfaces;

namespace SmartBearServer.Services.Implementations
{
    /// <summary>
    /// Authentication Service handles user registration, login, and JWT generation.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // 1. Check if email already exists
            var existingUser = await _userRepo.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new AuthResponse { Success = false, ErrorMessage = "Email already mapped to an account." };
            }

            // 2. Hash the password securely using BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // 3. Create a new user entity
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = request.Email,
                FullName = request.FullName,
                PasswordHash = passwordHash,
                Provider = "Local",
                RefreshToken = "", // Set to empty to satisfy database NOT NULL constraint
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userRepo.AddAsync(user);

            // 4. Generate JWT tokens
            return await GenerateAuthResponse(user);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            // 1. Retrieve the user
            var user = await _userRepo.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return new AuthResponse { Success = false, ErrorMessage = "Invalid credentials." };
            }

            // 2. Verify password
            Console.WriteLine($"[LoginDebug] Attempting login for: {request.Email}");
            bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            Console.WriteLine($"[LoginDebug] Password verification result: {isValid}");
            
            if (!isValid)
            {
                return new AuthResponse { Success = false, ErrorMessage = "Invalid credentials." };
            }

            // 3. Generate tokens
            return await GenerateAuthResponse(user);
        }

        public async Task<AuthResponse> LoginWithGoogleAsync(string idToken)
        {
            try
            {
                var googleConfig = _config.GetSection("Google");
                var clientIds = googleConfig.GetSection("ClientIds").Get<List<string>>() ?? new List<string>();
                
                Console.WriteLine($"[GoogleAuthDebug] Loaded {clientIds.Count} Client IDs from config.");
                foreach (var id in clientIds)
                {
                    Console.WriteLine($"[GoogleAuthDebug] Allowed ID: {id}");
                }

                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = clientIds
                };

                // Validate the Google ID token
                GoogleJsonWebSignature.Payload payload;
                try 
                {
                    payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                    Console.WriteLine($"[GoogleAuthDebug] Validation Success! Audience: {payload.Audience}, Email: {payload.Email}");
                }
                catch (Exception ex)
                {
                    // If validation fails, inspect the token without validation to identify the audience
                    Console.WriteLine($"[GoogleAuthDebug] Validation Failed: {ex.Message}");
                    try 
                    {
                        var unsafePayload = await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings { Audience = null });
                        Console.WriteLine($"[GoogleAuthDebug] Token contains Audience: {unsafePayload.Audience}");
                        Console.WriteLine($"[GoogleAuthDebug] Recommendation: Add the identified audience to appsettings.json.");
                    }
                    catch { /* ignore unsafe check errors */ }
                    throw;
                }

                // 1. Find user by email
                var user = await _userRepo.GetByEmailAsync(payload.Email);

                if (user == null)
                {
                    // 2. Create new user if not exists
                    user = new User
                    {
                        UserId = Guid.NewGuid(),
                        Email = payload.Email,
                        FullName = payload.Name,
                        PasswordHash = "", // External login, no local password
                        Provider = "Google",
                        ProviderId = payload.Subject,
                        RefreshToken = "", // Set to empty to satisfy DB NOT NULL constraint before GenerateAuthResponse sets it
                        RoleId = 2, // Default to User role
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    await _userRepo.AddAsync(user);
                }

                // 3. Generate tokens
                return await GenerateAuthResponse(user);
            }
            catch (InvalidJwtException e)
            {
                return new AuthResponse { Success = false, ErrorMessage = "Invalid Google token: " + e.Message };
            }
            catch (Exception e)
            {
                return new AuthResponse { Success = false, ErrorMessage = "Google Authentication failed: " + e.Message };
            }
        }

        public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepo.GetByRefreshTokenAsync(refreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new AuthResponse { Success = false, ErrorMessage = "Invalid or expired refresh token." };
            }

            return await GenerateAuthResponse(user);
        }

        private async Task<AuthResponse> GenerateAuthResponse(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtConfig = _config.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Key"] ?? throw new ArgumentException("Missing Jwt:Key"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,   user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Name,  user.FullName ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString()),
                    new Claim("role",                        user.RoleId.ToString()),
                    new Claim("isPro",                       user.IsPro.ToString().ToLower())
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtConfig["ExpireMinutes"] ?? "60")),
                Issuer = jwtConfig["Issuer"],
                Audience = jwtConfig["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            // Generate a secure refresh token
            var refreshToken = GenerateSecureToken();
            
            // Save refresh token to user record
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // 7 days expiration for refresh token
            
            await _userRepo.UpdateAsync(user);

            return new AuthResponse
            {
                Success = true,
                Token = jwtToken,
                RefreshToken = refreshToken,
                UserId = user.UserId.ToString(),
                RoleId = user.RoleId
            };
        }

        private string GenerateSecureToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
