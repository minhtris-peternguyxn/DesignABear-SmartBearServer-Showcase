using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBearServer.Model
{
    /// <summary>
    /// Represents a scheduled smart alarm configured by a parent for their child's bear device.
    /// When the alarm fires, the .NET backend pushes a TTS command to the Python bridge via SignalR.
    /// </summary>
    [Table("smart_alarms")]
    public class SmartAlarm : ISoftDelete
    {
        public bool IsDeleted { get; set; } = false;

        [Key]
        [Column("alarm_id")]
        public string AlarmId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [Column("device_id")]
        public string DeviceId { get; set; } = string.Empty;

        [ForeignKey("DeviceId")]
        public Device Device { get; set; } = null!;

        /// <summary>Local hour (0–23) to fire the alarm.</summary>
        [Column("hour")]
        public int Hour { get; set; }

        /// <summary>Local minute (0–59) to fire the alarm.</summary>
        [Column("minute")]
        public int Minute { get; set; }

        /// <summary>Personalized wake-up message sent to TTS.</summary>
        [MaxLength(200)]
        [Column("wake_up_message")]
        public string? WakeUpMessage { get; set; }

        [Column("is_enabled")]
        public bool IsEnabled { get; set; } = true;

        [Column("use_voice")]
        public bool UseVoice { get; set; } = false; // Mặc định tắt voice tạm thời theo yêu cầu

        [MaxLength(500)]
        [Column("audio_url")]
        public string? AudioUrl { get; set; }

        [Column("repeat_mode")]
        public string RepeatMode { get; set; } = "Once"; // Once, Count, Forever

        [Column("repeat_count")]
        public int RepeatCount { get; set; } = 1;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
