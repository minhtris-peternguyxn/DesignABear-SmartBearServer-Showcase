using System.Collections.Generic;

namespace SmartBearServer.Model.DTOs
{
    /// <summary>
    /// A "neat" DTO representing the full configuration and context of a Bear Profile,
    /// optimized for Redis caching and consumption by AI Agents.
    /// </summary>
    public class BearProfileConfig
    {
        // Device Identity
        public string DeviceId { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string? Nickname { get; set; }
        public Guid? UserId { get; set; }

        // Child Identity
        public string ProfileId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? Honorific { get; set; }
        public string? BearName { get; set; }
        public string? CurrentMode { get; set; }

        // Personality Lab
        public string? Personality { get; set; }
        public string? PersonalityDescription { get; set; }
        public int CreativityLevel { get; set; } = 3;
        public int EmotionLevel { get; set; } = 3;
        public int EnergyLevel { get; set; } = 3;
        public int ComplexityLevel { get; set; } = 3;

        // Preferences & Safety
        public string? PreferredVoiceId { get; set; }
        public string? PreferredTtsProvider { get; set; }
        public List<string> BlockedTopics { get; set; } = new();
        public SafetyResponseMode SafetyResponseMode { get; set; }
        public string? SafetyPretendMessage { get; set; }
        public string? SafetyWarningMessage { get; set; }

        // Subscription & Quota
        public bool IsPro { get; set; }
        public int DailyCandyBalance { get; set; }
        public int PurchasedCandies { get; set; }

        // Contextual History (Latest 5 turns)
        public List<InteractionHistoryDto> RecentHistory { get; set; } = new();

        public static BearProfileConfig FromDevice(Device d)
        {
            var config = new BearProfileConfig
            {
                DeviceId = d.DeviceId,
                SerialNumber = d.SerialNumber,
                Nickname = d.Nickname,
                ProfileId = d.Profile?.Id ?? string.Empty,
                Name = d.Profile?.Name ?? "Bạn Nhỏ",
                Age = d.Profile?.Age ?? 5,
                Gender = d.Profile?.Gender,
                Honorific = d.Profile?.Honorific,
                BearName = d.Profile?.BearName,
                CurrentMode = d.Profile?.CurrentMode,
                Personality = d.Profile?.Personality,
                PersonalityDescription = d.Profile?.PersonalityDescription,
                CreativityLevel = d.Profile?.CreativityLevel ?? 3,
                EmotionLevel = d.Profile?.EmotionLevel ?? 3,
                EnergyLevel = d.Profile?.EnergyLevel ?? 3,
                ComplexityLevel = d.Profile?.ComplexityLevel ?? 3,
                PreferredVoiceId = d.Profile?.PreferredVoiceId,
                PreferredTtsProvider = d.Profile?.PreferredTtsProvider,
                BlockedTopics = d.Profile?.BlockedTopics ?? new List<string>(),
                SafetyResponseMode = d.Profile?.SafetyResponseMode ?? SafetyResponseMode.GentleWarning,
                SafetyPretendMessage = d.Profile?.SafetyPretendMessage,
                SafetyWarningMessage = d.Profile?.SafetyWarningMessage,
                IsPro = d.ParentUser?.IsPro ?? false,
                DailyCandyBalance = d.Profile?.DailyCandyBalance ?? 0,
                PurchasedCandies = d.ParentUser?.SmartCandies ?? 0,
                UserId = d.UserId
            };

            if (d.Profile?.Interactions != null)
            {
                config.RecentHistory = d.Profile.Interactions
                    .OrderByDescending(i => i.Timestamp)
                    .Take(5)
                    .OrderBy(i => i.Timestamp)
                    .Select(i => new InteractionHistoryDto
                    {
                        Request = i.Request,
                        Response = i.Response,
                        Timestamp = i.Timestamp
                    }).ToList();
            }

            return config;
        }

        /// <summary>
        /// Converts the cached DTO back to a hydrated Device object for compatibility with existing services.
        /// Note: The returned object is for read-only or temporary use (not tracked by EF Core).
        /// </summary>
        public Device ToDevice()
        {
            var profile = new ChildProfile
            {
                Id = ProfileId,
                Name = Name,
                Age = Age,
                Gender = Gender,
                Honorific = Honorific,
                BearName = BearName,
                CurrentMode = CurrentMode,
                Personality = Personality,
                PersonalityDescription = PersonalityDescription,
                CreativityLevel = CreativityLevel,
                EmotionLevel = EmotionLevel,
                EnergyLevel = EnergyLevel,
                ComplexityLevel = ComplexityLevel,
                PreferredVoiceId = PreferredVoiceId,
                PreferredTtsProvider = PreferredTtsProvider,
                BlockedTopics = BlockedTopics ?? new List<string>(),
                SafetyResponseMode = SafetyResponseMode,
                SafetyPretendMessage = SafetyPretendMessage ?? string.Empty,
                SafetyWarningMessage = SafetyWarningMessage ?? string.Empty,
                DailyCandyBalance = DailyCandyBalance
            };

            var user = new User
            {
                Email = string.Empty,
                PasswordHash = string.Empty,
                FullName = string.Empty,
                IsPro = IsPro,
                SmartCandies = PurchasedCandies
            };

            var device = new Device
            {
                DeviceId = DeviceId,
                SerialNumber = SerialNumber,
                Nickname = Nickname,
                ProfileId = ProfileId,
                UserId = UserId,
                Profile = profile,
                ParentUser = user,
                Status = "Active"
            };

            if (RecentHistory != null)
            {
                profile.Interactions = RecentHistory.Select(h => new InteractionHistory
                {
                    Request = h.Request,
                    Response = h.Response,
                    Timestamp = h.Timestamp,
                    DeviceId = DeviceId,
                    ProfileId = ProfileId
                }).ToList();
            }

            return device;
        }
    }

    public class InteractionHistoryDto
    {
        public string Request { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
