using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBearServer.Model
{
    /// <summary>
    /// Represents a voice option in the voice catalog.
    /// Each entry maps to a TTS provider voice that users can select.
    /// </summary>
    public class DemoVoice
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Provider's voice identifier (e.g. "vi-VN-Neural2-A" for GCP, "TX3LPaxmHKxFdv7VOQHJ" for ElevenLabs)
        /// </summary>
        [Required]
        public string VoiceId { get; set; } = string.Empty;

        /// <summary>
        /// Display name (e.g. "Gấu Chị A (Nữ)")
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// TTS Provider: "GCP" or "ElevenLabs"
        /// </summary>
        [Required]
        public string Provider { get; set; } = "GCP";

        /// <summary>
        /// Whether this voice requires a Pro subscription
        /// </summary>
        public bool IsPremium { get; set; } = false;

        /// <summary>
        /// User-facing description of the voice
        /// </summary>
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
