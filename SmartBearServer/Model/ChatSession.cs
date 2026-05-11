using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBearServer.Model
{
    [Table("chat_sessions")]
    public class ChatSession
    {
        [Key]
        [Column("session_id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column("profile_id")]
        public string ProfileId { get; set; }

        [Column("start_time")]
        public DateTime StartTime { get; set; } = DateTime.UtcNow;

        [Column("last_interaction_time")]
        public DateTime LastInteractionTime { get; set; } = DateTime.UtcNow;

        [Column("end_time")]
        public DateTime? EndTime { get; set; }

        [MaxLength(100)]
        [Column("title")]
        public string? Title { get; set; } // e.g., "Trò chuyện buổi sáng", "Học tiếng anh"

        [MaxLength(500)]
        [Column("summary")]
        public string? Summary { get; set; } // AI-generated summary

        [MaxLength(50)]
        [Column("category")]
        public string? Category { get; set; } // e.g., "Education", "Fun", "Story"

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<InteractionHistory> Interactions { get; set; } = new List<InteractionHistory>();
    }
}
