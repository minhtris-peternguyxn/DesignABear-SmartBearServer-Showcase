using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using SmartBearServer.Services.Interfaces;

namespace SmartBearServer.Services
{
    public class DeviceIdentityService : IDeviceIdentityService
    {
        private readonly AppDbContext _db;

        public DeviceIdentityService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IssueDeviceTokenResponse> IssueTokenAsync(string deviceId, int validDays = 365)
        {
            var deviceExists = await _db.Devices.AnyAsync(d => d.DeviceId == deviceId);
            if (!deviceExists)
            {
                return null;
            }

            var rawToken = CreateToken();
            var now = DateTime.UtcNow;
            var expiresAt = now.AddDays(validDays);

            var credential = new DeviceCredential
            {
                DeviceId = deviceId,
                TokenHash = HashToken(rawToken),
                CreatedAtUtc = now,
                ExpiresAtUtc = expiresAt,
                IsActive = true
            };

            _db.DeviceCredentials.Add(credential);
            await _db.SaveChangesAsync();

            return new IssueDeviceTokenResponse
            {
                TokenId = credential.Id,
                Token = rawToken,
                ExpiresAtUtc = expiresAt
            };
        }

        public async Task<bool> ValidateTokenAsync(string deviceId, string rawToken)
        {
            if (string.IsNullOrWhiteSpace(deviceId) || string.IsNullOrWhiteSpace(rawToken))
            {
                return false;
            }

            var tokenHash = HashToken(rawToken);
            var now = DateTime.UtcNow;

            return await _db.DeviceCredentials.AnyAsync(c =>
                c.DeviceId == deviceId
                && c.TokenHash == tokenHash
                && c.IsActive
                && c.RevokedAtUtc == null
                && c.ExpiresAtUtc > now);
        }

        public async Task<bool> RevokeTokenAsync(string deviceId, string tokenId)
        {
            var token = await _db.DeviceCredentials
                .FirstOrDefaultAsync(c => c.Id == tokenId && c.DeviceId == deviceId);

            if (token == null)
            {
                return false;
            }

            token.IsActive = false;
            token.RevokedAtUtc = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }

        private static string CreateToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(32);
            return Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
        }

        private static string HashToken(string token)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
            return Convert.ToHexString(bytes);
        }
    }
}
