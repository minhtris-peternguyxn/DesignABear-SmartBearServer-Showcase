using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBearServer.Model
{
    /// <summary>
    /// Global banned word or phrase used for content filtering.
    /// </summary>
    [Table("banned_words")]
    public class BannedWord
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// The keyword or phrase to block.
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Column("word")]
        public string Word { get; set; } = string.Empty;

        /// <summary>
        /// Optional category label (e.g., "violence", "adult", "drug").
        /// </summary>
        [MaxLength(50)]
        [Column("category")]
        public string? Category { get; set; }

        /// <summary>
        /// Whether this entry is currently active.
        /// </summary>
        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("created_by")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// The user who owns this banned word. 
        /// If NULL, it is a global system-wide banned word managed by Admin.
        /// </summary>
        [Column("user_id")]
        public Guid? UserId { get; set; }
    }
}
