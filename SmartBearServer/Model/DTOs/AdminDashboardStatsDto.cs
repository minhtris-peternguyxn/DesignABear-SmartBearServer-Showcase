using System;

namespace SmartBearServer.Model.DTOs
{
    /// <summary>
    /// Data Transfer Object for top-level administrative dashboard statistics.
    /// Aggregates key metrics from users, devices, and financial records.
    /// </summary>
    public class AdminDashboardStatsDto
    {
        public int TotalUsers { get; set; }
        public int ProUsers { get; set; }
        public int TotalDevices { get; set; }
        public int ActiveSessions { get; set; }
        public decimal TotalRevenueVnd { get; set; }
        public int TotalSongs { get; set; }
        public int TotalStories { get; set; }
        public double MusicStorageMb { get; set; }
        public double StoryStorageKb { get; set; }
        public int SuccessfulOrdersCount { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public decimal LastOrderAmount { get; set; }
        public DateTime LastSyncTime { get; set; } = DateTime.UtcNow;
    }
}
