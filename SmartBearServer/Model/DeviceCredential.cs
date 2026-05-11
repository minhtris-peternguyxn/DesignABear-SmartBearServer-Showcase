using System;

namespace SmartBearServer.Model
{
    public class DeviceCredential
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string DeviceId { get; set; }
        public string TokenHash { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAtUtc { get; set; }
        public DateTime? RevokedAtUtc { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
