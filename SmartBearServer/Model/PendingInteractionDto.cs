using System;

namespace SmartBearServer.Model
{
    /// <summary>
    /// Data Transfer Object representing a pending chat interaction to be persisted.
    /// This object is queued in Redis and processed asynchronously.
    /// </summary>
    public class PendingInteractionDto
    {
        public string DeviceId { get; set; } = string.Empty;
        public string ProfileId { get; set; } = string.Empty;
        public string Request { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public Guid SessionId { get; set; }
        public bool IsSafe { get; set; } = true;
        public string SafetyCategory { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
