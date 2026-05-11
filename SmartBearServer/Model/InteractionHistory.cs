using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBearServer.Model
{
    [Table("interaction_histories")]
    public class InteractionHistory
    {
        [Key]
        [Column("history_id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [Column("request")]
        public string Request { get; set; }

        [Required]
        [Column("response")]
        public string Response { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        [Column("device_id")]
        public string DeviceId { get; set; }

        [Column("profile_id")]
        public string ProfileId { get; set; }

        [Column("session_id")]
        public Guid? SessionId { get; set; }

        [Column("is_safe")]
        public bool IsSafe { get; set; } = true;

        [MaxLength(100)]
        [Column("safety_violation_category")]
        public string SafetyViolationCategory { get; set; } = "";

        // Navigation properties
        [ForeignKey("SessionId")]
        public ChatSession Session { get; set; }
    }
}
