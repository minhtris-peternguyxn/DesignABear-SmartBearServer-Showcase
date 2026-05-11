using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBearServer.Model
{
    public class ChildProfile : ISoftDelete
    {
        public bool IsDeleted { get; set; } = false;
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        [Column("user_id")]
        public Guid? UserId { get; set; }

        [ForeignKey("UserId")]
        public User ParentUser { get; set; }

        public List<InteractionHistory> Interactions { get; set; } = new List<InteractionHistory>();

        public List<string> BlockedTopics { get; set; } = new List<string>();
        
        /// <summary>The name given to the bear, used by AI to refer to itself.</summary>
        public string? BearName { get; set; }
        
        // Nomal | Math | Bilingual
        public string CurrentMode { get; set; } = "Normal";

        /// <summary>
        /// Age category of the bear, determining AI complexity and available modes.
        /// Baby = under 6 y/o, Junior = 6-10 y/o.
        /// Calculated based on age to reduce redundant DB fields.
        /// </summary>
        [NotMapped]
        public BearCategory BearCategory => Age < 6 ? BearCategory.Baby : BearCategory.Junior;

        // --- Personalization & Aesthetics ---
        
        /// <summary>Child's gender: e.g., "Bé trai", "Bé gái".</summary>
        public string? Gender { get; set; }

        /// <summary>How the bear addresses the child: e.g., "Con", "Bé", "Bạn".</summary>
        public string? Honorific { get; set; }

        /// <summary>Bear's personality: e.g., "Thông thái", "Năng động", "Nhẹ nhàng", "Hài hước".</summary>
        public string? Personality { get; set; }

        /// <summary>Custom instructions for the bear's personality or specific habits.</summary>
        public string? PersonalityDescription { get; set; }

        /// <summary>Specific voice configuration for this bear profile.</summary>
        public string? PreferredVoiceId { get; set; }
        public string? PreferredTtsProvider { get; set; } = "GCP";

        /// <summary>
        /// How the bear reacts when a content safety filter (Banned Words/Topics) is triggered.
        /// </summary>
        [Column("safety_response_mode")]
        public SafetyResponseMode SafetyResponseMode { get; set; } = SafetyResponseMode.GentleWarning;

        [MaxLength(500)]
        [Column("safety_pretend_message")]
        public string SafetyPretendMessage { get; set; } = "Hả? Bé nói gì gấu nghe không rõ nhỉ? Bé nói lại được không?";

        [MaxLength(500)]
        [Column("safety_warning_message")]
        public string SafetyWarningMessage { get; set; } = "Bé ơi, mình nói chuyện khác vui hơn nhé!";

        // --- Candy Quota System ---
        
        /// <summary>Remaining candy for today (refilled at 0:00).</summary>
        [Column("daily_candy_balance")]
        public int DailyCandyBalance { get; set; } = 10;

        /// <summary>Last time the daily candy was refilled.</summary>
        [Column("last_quota_reset_utc")]
        public DateTime LastQuotaResetUtc { get; set; } = DateTime.MinValue;

        /// <summary>The maximum daily candy limit allowed by the current subscription plan.</summary>
        [Column("daily_candy_limit")]
        public int DailyCandyLimit { get; set; } = 10;

        /// <summary>Profile image URL or default image ID.</summary>
        public string? ProfileImageUrl { get; set; }

        // --- Personality Lab (Sliders 1-5) ---
        public int CreativityLevel { get; set; } = 3;
        public int EmotionLevel { get; set; } = 3;
        public int EnergyLevel { get; set; } = 3;
        public int ComplexityLevel { get; set; } = 3;
    }

    public enum SafetyResponseMode
    {
        /// <summary>
        /// The bear pretends not to hear (e.g., "Hả? Bé nói gì gấu nghe không rõ?").
        /// </summary>
        PretendNotToHear = 1,

        /// <summary>
        /// The bear gives a gentle warning and suggests a different topic.
        /// </summary>
        GentleWarning = 2
    }
}
