using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBearServer.Model
{
    [Table("bear_devices")]
    public class Device : ISoftDelete
    {
        public bool IsDeleted { get; set; } = false;
        [Key]
        [Column("device_id")]
        public required string DeviceId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("serial_number")]
        public required string SerialNumber { get; set; }

        /// <summary>Parent-given friendly name for the bear, e.g. "Gấu của Bé Sóc".</summary>
        [MaxLength(100)]
        [Column("nickname")]
        public string? Nickname { get; set; }

        /// <summary>Is hardware protected by activation lock?</summary>
        [Column("is_protected")]
        public bool IsHardwareProtectionEnabled { get; set; } = false;
        // Active | Inactive
        [MaxLength(20)]
        [Column("status")]
        public required string Status { get; set; } = "Active";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("user_id")]
        public Guid? UserId { get; set; }

        [ForeignKey("UserId")]
        [System.Text.Json.Serialization.JsonIgnore]
        public User? ParentUser { get; set; }

        [Column("profile_id")]
        public string? ProfileId { get; set; }

        public ChildProfile? Profile { get; set; }

        /// <summary>Temporary 6-digit pairing code spoken by the bear. Null after claimed or expired.</summary>
        [MaxLength(6)]
        [Column("pairing_code")]
        public string? PairingCode { get; set; }

        /// <summary>UTC expiry time for the pairing code (10 minutes from generation).</summary>
        [Column("pairing_code_expires_at")]
        public DateTime? PairingCodeExpiresAt { get; set; }

        [Column("preferred_speed")]
        public float PreferredSpeed { get; set; } = 1.0f;

        [Column("preferred_volume")]
        public float PreferredVolume { get; set; } = 1.5f;
    }
}
