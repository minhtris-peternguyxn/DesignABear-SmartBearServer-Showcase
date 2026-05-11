using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBearServer.Model
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(120)]
        [Column("email")]
        public required string Email { get; set; }

        [MaxLength(255)]
        [Column("password_hash")]
        public required string PasswordHash { get; set; }

        [MaxLength(120)]
        [Column("full_name")]
        public required string FullName { get; set; }

        [MaxLength(30)]
        [Column("provider")]
        public string? Provider { get; set; } = "Local";

        [MaxLength(255)]
        [Column("provider_id")]
        public string? ProviderId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(512)]
        [Column("refresh_token")]
        public string? RefreshToken { get; set; }

        [Column("refresh_token_expiry_time")]
        public DateTime? RefreshTokenExpiryTime { get; set; }

        [Column("is_pro")]
        public bool IsPro { get; set; } = false;

        [Column("smart_candies")]
        public int SmartCandies { get; set; } = 10; 

        [Column("free_daily_credits_last_reset")]
        public DateTime FreeDailyCreditsLastReset { get; set; } = DateTime.UtcNow;

        [MaxLength(20)]
        [Column("preferred_tts_provider")]
        public string PreferredTtsProvider { get; set; } = "GCP";

        [MaxLength(50)]
        [Column("preferred_voice_id")]
        public string PreferredVoiceId { get; set; } = "vi-VN-Neural2-A";

        [Column("pro_expires_at")]
        public DateTime? ProExpiresAt { get; set; }

        [Column("subscription_plan_id")]
        public int? SubscriptionPlanId { get; set; }

        [ForeignKey("SubscriptionPlanId")]
        public virtual SubscriptionPlan? SubscriptionPlan { get; set; }

        [NotMapped]
        public SubscriptionStatus SubscriptionStatus
        {
            get
            {
                if (!IsPro) return SubscriptionStatus.Trial; // Or a new 'Free' status
                if (ProExpiresAt == null) return SubscriptionStatus.Active;
                var now = DateTime.UtcNow;
                if (now <= ProExpiresAt.Value) return SubscriptionStatus.Active;
                if (now <= ProExpiresAt.Value.AddDays(3)) return SubscriptionStatus.Grace;
                return SubscriptionStatus.Expired;
            }
        }

        [Column("role_id")]
        public int RoleId { get; set; } = 2; // Default to User role

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Device> Devices { get; set; } = new List<Device>();

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<ChildProfile> ChildProfiles { get; set; } = new List<ChildProfile>();
    }
}
