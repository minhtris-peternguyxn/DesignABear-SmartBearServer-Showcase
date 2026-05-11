using Microsoft.EntityFrameworkCore;
using SmartBearServer.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace SmartBearServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ChildProfile> ChildProfiles { get; set; }
        public DbSet<InteractionHistory> InteractionHistories { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<DeviceCredential> DeviceCredentials { get; set; }
        public DbSet<DailyUsage> DailyUsages { get; set; }
        public DbSet<SmartAlarm> SmartAlarms { get; set; }
        public DbSet<BannedWord> BannedWords { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<PendingOrder> PendingOrders { get; set; }
        public DbSet<ChatSession> ChatSessions { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<DemoVoice> DemoVoices { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Global Query Filters for Soft Delete
            modelBuilder.Entity<ChildProfile>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Device>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Song>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Story>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<SmartAlarm>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<ChatSession>()
                .HasIndex(s => new { s.ProfileId, s.IsActive });

            modelBuilder.Entity<ChatSession>()
                .HasMany(s => s.Interactions)
                .WithOne(i => i.Session)
                .HasForeignKey(i => i.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Device>()
                .HasKey(d => d.DeviceId);

            modelBuilder.Entity<Device>()
                .HasOne(d => d.ParentUser)
                .WithMany(u => u.Devices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Device>()
                .HasOne(d => d.Profile)
                .WithMany()
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.SetNull);

            var stringListComparer = new ValueComparer<List<string>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());

            modelBuilder.Entity<ChildProfile>()
                .Property(e => e.BlockedTopics)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()
                )
                .Metadata.SetValueComparer(stringListComparer);


            modelBuilder.Entity<ChildProfile>()
                .HasMany(c => c.Interactions)
                .WithOne()
                .HasForeignKey(i => i.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChildProfile>()
                .HasOne(c => c.ParentUser)
                .WithMany(u => u.ChildProfiles)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<SubscriptionPlan>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<DeviceCredential>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<DeviceCredential>()
                .HasIndex(c => new { c.DeviceId, c.TokenHash })
                .IsUnique();

            modelBuilder.Entity<DailyUsage>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<DailyUsage>()
                .HasIndex(u => new { u.DeviceId, u.DateUtc })
                .IsUnique();

            modelBuilder.Entity<SubscriptionPlan>()
                .Property(p => p.PriceMonthly)
                .HasColumnType("numeric(18,2)");

            modelBuilder.Entity<SubscriptionPlan>().HasData(
                new SubscriptionPlan
                {
                    Id = 1,
                    PlanType = SubscriptionPlanType.Basic,
                    Name = "Gói Cơ Bản",
                    Description = "Trải nghiệm cơ bản: 10 viên kẹo mỗi ngày, nhạc miễn phí.",
                    CanPlayMusic = true,
                    CanTellStoriesOnUserSpeech = true,
                    CanUseLearningAI = false,
                    DailyCandyLimit = 10,
                    PriceMonthly = 0m,
                    IsActive = true
                },
                new SubscriptionPlan
                {
                    Id = 2,
                    PlanType = SubscriptionPlanType.Premium,
                    Name = "Gói Nâng Cao",
                    Description = "Không giới hạn kẹo, giọng đọc cao cấp, học Toán & Tiếng Anh.",
                    CanPlayMusic = true,
                    CanTellStoriesOnUserSpeech = true,
                    CanUseLearningAI = true,
                    DailyCandyLimit = 50,
                    PriceMonthly = 100000m,
                    IsActive = true
                },
                new SubscriptionPlan
                {
                    Id = 3,
                    PlanType = SubscriptionPlanType.VIP,
                    Name = "Gói Ultra",
                    Description = "Siêu cấp: 300 viên kẹo mỗi ngày, không giới hạn tính năng.",
                    CanPlayMusic = true,
                    CanTellStoriesOnUserSpeech = true,
                    CanUseLearningAI = true,
                    DailyCandyLimit = 300,
                    PriceMonthly = 350000m,
                    IsActive = true
                }
            );

            // Seed default global safety rules (Keywords & AI Safety Patterns)
            modelBuilder.Entity<BannedWord>().HasData(SmartBearServer.Data.SeedData.BannedWordSeed.GetSeedData());

            modelBuilder.Entity<Voucher>().HasData(
                new Voucher
                {
                    Id = 1,
                    Code = "DEMO67",
                    FixedDiscountValue = 98000,
                    IsActive = true,
                    MaxUsage = 1000,
                    CurrentUsage = 0,
                    ExpiryDate = new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Voucher
                {
                    Id = 2,
                    Code = "DEMODONGGIA2K",
                    Description = "Mã kiểm thử: Đồng giá tất cả dịch vụ 2,000đ",
                    FlatPriceValue = 2000,
                    IsActive = true,
                    MaxUsage = 9999,
                    CurrentUsage = 0,
                    ExpiryDate = new DateTime(2035, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = 1,
                    RoleName = "Master",
                    Description = "System Master Administrator with full access.",
                    IsSystem = true,
                    Priority = 0,
                    ColorHex = "#FF0000",
                    Icon = "admin_panel_settings"
                },
                new Role
                {
                    RoleId = 2,
                    RoleName = "User",
                    Description = "Standard regular user.",
                    IsSystem = true,
                    Priority = 10,
                    ColorHex = "#00FF00",
                    Icon = "person"
                }
            );

            // Seed system administrative and test accounts
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Email = "admin@admin.com",
                    FullName = "Master Administrator",
                    // Correct BCrypt hash for 'admin123'
                    PasswordHash = "$2a$11$wqjr5Joq.8ZCu0WFzKTWS..sobu6cqhPGXPNx4hdHUrkbfFxuuM9K", 
                    RoleId = 1,
                    RefreshToken = "",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    FreeDailyCreditsLastReset = DateTime.UtcNow
                }
            );

            // Seed Voice Catalog
            modelBuilder.Entity<DemoVoice>().HasData(
                new DemoVoice { Id = "voice-gcp-a", VoiceId = "vi-VN-Neural2-A", Name = "Gấu Chị A (Nữ)", Provider = "GCP", Description = "Giọng nữ chuẩn, vui vẻ.", IsPremium = false },
                new DemoVoice { Id = "voice-gcp-d", VoiceId = "vi-VN-Neural2-D", Name = "Gấu Anh D (Nam)", Provider = "GCP", Description = "Giọng nam ấm áp, rõ ràng.", IsPremium = false },
                new DemoVoice { Id = "voice-gcp-wavenet-a", VoiceId = "vi-VN-Wavenet-A", Name = "Gấu Mẹ A (Nữ)", Provider = "GCP", Description = "Giọng nữ ngọt ngào, truyền cảm.", IsPremium = false },
                new DemoVoice { Id = "voice-gcp-wavenet-b", VoiceId = "vi-VN-Wavenet-B", Name = "Gấu Bố B (Nam)", Provider = "GCP", Description = "Giọng nam trầm, vững chãi.", IsPremium = false },
                new DemoVoice { Id = "voice-gcp-wavenet-c", VoiceId = "vi-VN-Wavenet-C", Name = "Gấu Cô C (Nữ)", Provider = "GCP", Description = "Giọng nữ trầm, chững chạc.", IsPremium = false },
                new DemoVoice { Id = "voice-gcp-wavenet-d", VoiceId = "vi-VN-Wavenet-D", Name = "Gấu Chú D (Nam)", Provider = "GCP", Description = "Giọng nam truyền cảm, dễ nghe.", IsPremium = false },
                new DemoVoice { Id = "voice-eleven-adam", VoiceId = "pNInz6obpgnuPs397vXP", Name = "Gấu Adam (Nam)", Provider = "ElevenLabs", Description = "Giọng nam trầm ấm, chuyên nghiệp.", IsPremium = true },
                new DemoVoice { Id = "voice-eleven-liam", VoiceId = "TX3LPaxmHKxFfW646Sse", Name = "Gấu Liam (Nam)", Provider = "ElevenLabs", Description = "Giọng nam trẻ trung, đầy năng lượng.", IsPremium = true },
                new DemoVoice { Id = "voice-eleven-bella", VoiceId = "EXAVITQu4vr4xnSDxMaL", Name = "Gấu Bella (Nữ)", Provider = "ElevenLabs", Description = "Giọng nữ nhẹ nhàng, ấm áp.", IsPremium = true },
                new DemoVoice { Id = "voice-eleven-sarah", VoiceId = "Lcf7eeY9feD1p95OmDAn", Name = "Gấu Sarah (Nữ)", Provider = "ElevenLabs", Description = "Giọng nữ truyền cảm, dễ thương.", IsPremium = true },
                new DemoVoice { Id = "voice-eleven-rachel", VoiceId = "MF3mGyEYCl7XYW7L696t", Name = "Gấu Rachel (Nữ)", Provider = "ElevenLabs", Description = "Giọng nữ rõ ràng, rành mạch.", IsPremium = true },
                new DemoVoice { Id = "voice-eleven-antoni", VoiceId = "ErXw7ePBqOfDr909BvG6", Name = "Gấu Antoni (Nam)", Provider = "ElevenLabs", Description = "Giọng nam chững chạc, đáng tin cậy.", IsPremium = true },
                new DemoVoice { Id = "voice-eleven-charlie", VoiceId = "IKne3meq5pC9XdtgXx6M", Name = "Gấu Charlie (Nam)", Provider = "ElevenLabs", Description = "Giọng nam thân thiện, tự nhiên.", IsPremium = true }
            );
        }
    }
}
