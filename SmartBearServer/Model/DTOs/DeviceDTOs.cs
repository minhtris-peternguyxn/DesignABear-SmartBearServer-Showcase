using System.ComponentModel.DataAnnotations;
using SmartBearServer.Model;

namespace SmartBearServer.Model.DTOs
{
    // ─── Device Pairing ──────────────────────────────────────────────────────────

    /// <summary>Request payload to pair a physical bear device using its serial number.</summary>
    public class PairDeviceRequest
    {
        [Required]
        [MaxLength(100)]
        public string SerialNumber { get; set; } = string.Empty;

        /// <summary>Friendly display name chosen by the parent, e.g. "Gấu của Bé Sóc".</summary>
        [MaxLength(100)]
        public string? Nickname { get; set; }
    }

    /// <summary>
    /// Request payload for claiming a bear device via the 6-digit voice pairing code.
    /// The parent hears the code spoken by the bear after it connects to WiFi.
    /// Press the BOOT button on the bear to repeat the code.
    /// </summary>
    public class ClaimDeviceRequest
    {
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Pairing code must be exactly 6 digits.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pairing code must be 6 digits.")]
        public string Code { get; set; } = string.Empty;

        /// <summary>Optional friendly name for the bear, e.g. "Gấu của Bé Sóc".</summary>
        [MaxLength(100)]
        public string? Nickname { get; set; }

        /// <summary>Name of the child who will use this bear.</summary>
        [MaxLength(100)]
        public string? ChildName { get; set; }
    }

    /// <summary>Safe response DTO for a bear device — never exposes internal FKs or user IDs.</summary>
    public class DeviceDto
    {
        public string DeviceId { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string? Nickname { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsHardwareProtectionEnabled { get; set; }

        /// <summary>Child profile name assigned to this device, or null if not set.</summary>
        public string? ProfileName { get; set; }

        /// <summary>Active AI mode of the assigned profile.</summary>
        public string? CurrentMode { get; set; }

        public string? Gender { get; set; }
        public string? Personality { get; set; }
        public string? Honorific { get; set; }
        public int? Age { get; set; }
        public string? ProfileId { get; set; }
        public int DailyCandyBalance { get; set; }
        public int PurchasedCandies { get; set; }
        public string RemainingCandiesDisplay { get; set; } = string.Empty;
        public string? BearName { get; set; }
        public string? PreferredVoiceId { get; set; }
        public string? PreferredTtsProvider { get; set; }
        public string? PersonalityDescription { get; set; }
        public List<string>? BlockedTopics { get; set; }
        public int? SafetyResponseMode { get; set; }
        public string? SafetyPretendMessage { get; set; }
        public string? SafetyWarningMessage { get; set; }
        public string? ProfileImageUrl { get; set; }
        public int CreativityLevel { get; set; }
        public int EmotionLevel { get; set; }
        public int EnergyLevel { get; set; }
        public int ComplexityLevel { get; set; }


        public static DeviceDto From(Device d) => new()
        {
            DeviceId    = d.DeviceId,
            SerialNumber = d.SerialNumber,
            Nickname    = d.Nickname,
            Status      = d.Status,
            CreatedAt   = d.CreatedAt,
            IsHardwareProtectionEnabled = d.IsHardwareProtectionEnabled,
            ProfileName = d.Profile?.Name,
            CurrentMode = d.Profile?.CurrentMode,
            Gender      = d.Profile?.Gender,
            Personality = d.Profile?.Personality,
            Honorific   = d.Profile?.Honorific,
            Age         = d.Profile?.Age,
            ProfileId   = d.Profile?.Id,
            PreferredVoiceId = d.Profile?.PreferredVoiceId,
            PreferredTtsProvider = d.Profile?.PreferredTtsProvider,
            PersonalityDescription = d.Profile?.PersonalityDescription,
            DailyCandyBalance = d.Profile?.DailyCandyBalance ?? 10,
            PurchasedCandies = d.ParentUser?.SmartCandies ?? 0,
            RemainingCandiesDisplay = ((d.Profile?.DailyCandyBalance ?? 10) + (d.ParentUser?.SmartCandies ?? 0)).ToString(),
            BearName = d.Profile?.BearName,
            BlockedTopics = d.Profile?.BlockedTopics,
            SafetyResponseMode = (int?)d.Profile?.SafetyResponseMode,
            SafetyPretendMessage = d.Profile?.SafetyPretendMessage,
            SafetyWarningMessage = d.Profile?.SafetyWarningMessage,
            ProfileImageUrl = d.Profile?.ProfileImageUrl,
            CreativityLevel = d.Profile?.CreativityLevel ?? 3,
            EmotionLevel = d.Profile?.EmotionLevel ?? 3,
            EnergyLevel = d.Profile?.EnergyLevel ?? 3,
            ComplexityLevel = d.Profile?.ComplexityLevel ?? 3
        };

    }

    /// <summary>Request payload to toggle activation lock.</summary>
    public class ToggleProtectionRequest
    {
        [Required]
        public bool IsEnabled { get; set; }
    }
    /// <summary>Request payload to assign a child profile to a device.</summary>
    public class AssignProfileRequest
    {
        [Required]
        public string DeviceId { get; set; } = string.Empty;

        [Required]
        public string ProfileId { get; set; } = string.Empty;
    }

    /// <summary>Request payload to change the active AI mode on a device.</summary>
    public class SetDeviceModeRequest
    {
        [Required]
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>Accepted values: "Normal" | "Math" | "Bilingual"</summary>
        [Required]
        [RegularExpression("Normal|Math|Bilingual", ErrorMessage = "Mode must be one of: Normal, Math, Bilingual")]
        public string Mode { get; set; } = "Normal";
    }

    // ─── Child Profile ────────────────────────────────────────────────────────────

    /// <summary>Request payload for creating or updating a child profile.</summary>
    public class UpsertChildProfileRequest
    {
        public string? ProfileId { get; set; } // null = create, provided = update

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 18)]
        public int Age { get; set; }

        public List<string> BlockedTopics { get; set; } = new();
    }

    // ─── Smart Alarm ─────────────────────────────────────────────────────────────

    /// <summary>Request payload for setting a smart alarm on a device.</summary>
    public class SmartAlarmRequest
    {
        [Required]
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>Local hour to fire the alarm (0–23).</summary>
        [Range(0, 23)]
        public int Hour { get; set; }

        /// <summary>Local minute to fire the alarm (0–59).</summary>
        [Range(0, 59)]
        public int Minute { get; set; }

        /// <summary>Personalized wake-up message text injected to TTS via Python.</summary>
        [MaxLength(200)]
        public string? WakeUpMessage { get; set; }

        public bool IsEnabled { get; set; } = true;

        public bool UseVoice { get; set; } = false;

        [MaxLength(500)]
        public string? AudioUrl { get; set; }

        public string RepeatMode { get; set; } = "Once";

        public int RepeatCount { get; set; } = 1;
    }

    /// <summary>Safe response DTO for a smart alarm.</summary>
    public class SmartAlarmDto
    {
        public string AlarmId { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public string DeviceSerialNumber { get; set; } = string.Empty;
        public int Hour { get; set; }
        public int Minute { get; set; }
        public string? WakeUpMessage { get; set; }
        public bool IsEnabled { get; set; }
        public bool UseVoice { get; set; }
        public string? AudioUrl { get; set; }
        public string RepeatMode { get; set; } = "Once";
        public int RepeatCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>Operation result for smart alarm actions.</summary>
    public class SmartAlarmOperationResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public SmartAlarmDto? Alarm { get; set; }

        public static SmartAlarmOperationResult Ok(SmartAlarmDto? alarm = null)
            => new() { Success = true, Alarm = alarm };

        public static SmartAlarmOperationResult Fail(string message)
            => new() { Success = false, ErrorMessage = message };
    }

    /// <summary>Request to update an existing smart alarm.</summary>
    public class UpdateSmartAlarmRequest
    {
        [Range(0, 23)]
        public int Hour { get; set; }
        [Range(0, 59)]
        public int Minute { get; set; }
        [MaxLength(200)]
        public string? WakeUpMessage { get; set; }
        public bool IsEnabled { get; set; }
        public bool UseVoice { get; set; }
        [MaxLength(500)]
        public string? AudioUrl { get; set; }

        public string? RepeatMode { get; set; }
        public int? RepeatCount { get; set; }
    }

    // ─── Generic Result ───────────────────────────────────────────────────────────

    /// <summary>Generic result wrapper for all device service operations.</summary>
    public class DeviceOperationResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public Device? Device { get; set; }
        public ChildProfile? Profile { get; set; }

        public static DeviceOperationResult Ok(Device? device = null, ChildProfile? profile = null)
            => new() { Success = true, Device = device, Profile = profile };

        public static DeviceOperationResult Fail(string message)
            => new() { Success = false, ErrorMessage = message };
    }

    public class UpdateProfileRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? DeviceNickname { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Honorific { get; set; } = string.Empty;
        public string Personality { get; set; } = string.Empty;
        public string PersonalityDescription { get; set; } = string.Empty;
        public string PreferredVoiceId { get; set; } = string.Empty;
        public string PreferredTtsProvider { get; set; } = string.Empty;
        public string? BearName { get; set; }
        public List<string>? BlockedTopics { get; set; }
        public SafetyResponseMode? SafetyResponseMode { get; set; }
        public string? SafetyPretendMessage { get; set; }
        public string? SafetyWarningMessage { get; set; }
        public string? ProfileImageUrl { get; set; }
        public int? CreativityLevel { get; set; }
        public int? EmotionLevel { get; set; }
        public int? EnergyLevel { get; set; }
        public int? ComplexityLevel { get; set; }
    }

    public class UpdateProfileSafetyRequest
    {
        [Required]
        public string ProfileId { get; set; } = string.Empty;
        public List<string>? BlockedTopics { get; set; }
        public SafetyResponseMode? SafetyResponseMode { get; set; }
        public string? SafetyPretendMessage { get; set; }
        public string? SafetyWarningMessage { get; set; }
    }
}
