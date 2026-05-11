using SmartBearServer.Model;
using SmartBearServer.Infrastructure;
using SmartBearServer.Model.DTOs;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Services.Interfaces;
using SmartBearServer.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using SmartBearServer.Hubs;

namespace SmartBearServer.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepo;
        private readonly IChildProfileRepository _profileRepo;
        private readonly IDeviceRepository _deviceRepo;
        private readonly ISongRepository _songRepo;
        private readonly IStoryRepository _storyRepo;
        private readonly GeminiClient _gemini;
        private readonly ILearningRecommendationPromptBuilder _promptBuilder;
        private readonly IDeviceIdentityService _deviceIdentity;
        private readonly ISubscriptionLifecycleService _subscriptionLifecycle;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        private readonly IVoucherRepository _voucherRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IMediaService _mediaService;
        private readonly ICacheService _cache;

        public AdminService(
            IUserRepository userRepo,
            IChildProfileRepository profileRepo,
            IDeviceRepository deviceRepo,
            ISongRepository songRepo,
            IStoryRepository storyRepo,
            GeminiClient gemini,
            ILearningRecommendationPromptBuilder promptBuilder,
            IDeviceIdentityService deviceIdentity,
            ISubscriptionLifecycleService subscriptionLifecycle,
            IConfiguration config,
            IVoucherRepository voucherRepo,
            IOrderRepository orderRepo,
            IMediaService mediaService,
            IStorageService storageService,
            ICacheService cache)
        {
            _mediaService = mediaService;
            _config = config;
            _voucherRepo = voucherRepo;
            _orderRepo = orderRepo;
            _storageService = storageService;
            _cache = cache;
            
            _userRepo = userRepo;
            _profileRepo = profileRepo;
            _deviceRepo = deviceRepo;
            _songRepo = songRepo;
            _storyRepo = storyRepo;
            _gemini = gemini;
            _promptBuilder = promptBuilder;
            _deviceIdentity = deviceIdentity;
            _subscriptionLifecycle = subscriptionLifecycle;
        }

        // --- ChildProfile CRUD ---
        public async Task<Result<List<ChildProfileDto>>> GetAllProfilesAsync()
        {
            var profiles = await _profileRepo.GetAllAsync();
            return Result<List<ChildProfileDto>>.Success(profiles.Select(MapToDto).ToList());
        }
        
        public async Task<Result<ChildProfileDto>> GetProfileAsync(string id)
        {
            var p = await _profileRepo.GetByIdAsync(id);
            if (p == null) return Result<ChildProfileDto>.Failure(new Error("NotFound", "Profile not found"));
            return Result<ChildProfileDto>.Success(MapToDto(p));
        }
        
        public async Task<Result<ChildProfileDto>> CreateProfileAsync(ChildProfile profile)
        {
            var p = await _profileRepo.AddAsync(profile);
            return Result<ChildProfileDto>.Success(MapToDto(p));
        }

        public async Task<Result> UpdateProfileAsync(ChildProfile profile)
        {
            await _profileRepo.UpdateAsync(profile);
            
            // Invalidate caches
            await _cache.RemoveAsync($"quota:daily:candy:{profile.Id}");
            var device = await _deviceRepo.GetByProfileIdAsync(profile.Id);
            if (device != null)
            {
                var norm = LLMHub.NormalizeSerial(device.SerialNumber);
                var normId = LLMHub.NormalizeSerial(device.DeviceId);
                await _cache.RemoveAsync($"bear:profile:config:{norm}");
                await _cache.RemoveAsync($"bear:profile:config:{normId}");
            }
            
            return Result.Success();
        }

        public async Task<Result> DeleteProfileAsync(string id)
        {
            var p = await _profileRepo.GetByIdAsync(id);
            if (p != null)
            {
                p.IsDeleted = true;
                await _profileRepo.UpdateAsync(p);
                
                // Invalidate caches
                await _cache.RemoveAsync($"quota:daily:candy:{id}");
                var device = await _deviceRepo.GetByProfileIdAsync(id);
                if (device != null)
                {
                    var norm = LLMHub.NormalizeSerial(device.SerialNumber);
                    await _cache.RemoveAsync($"bear:profile:config:{norm}");
                }
            }
            return Result.Success();
        }

        // --- Device CRUD ---
        public async Task<Result<List<DeviceDto>>> GetAllDevicesAsync()
        {
            var devices = await _deviceRepo.GetAllAsync();
            return Result<List<DeviceDto>>.Success(devices.Select(DeviceDto.From).ToList());
        }

        public async Task<Result<DeviceDto>> CreateDeviceAsync(Device device)
        {
            var d = await _deviceRepo.AddAsync(device);
            return Result<DeviceDto>.Success(DeviceDto.From(d));
        }

        public async Task<Result> UpdateDeviceAsync(Device device)
        {
            await _deviceRepo.UpdateAsync(device);
            
            var norm = LLMHub.NormalizeSerial(device.SerialNumber);
            var normId = LLMHub.NormalizeSerial(device.DeviceId);
            await _cache.RemoveAsync($"bear:profile:config:{norm}");
            await _cache.RemoveAsync($"bear:profile:config:{normId}");
            await _cache.RemoveAsync($"device:config:sn:{norm}");
            await _cache.RemoveAsync($"device:config:id:{normId}");
            
            return Result.Success();
        }

        public async Task<Result> DeleteDeviceAsync(string deviceId)
        {
            var d = await _deviceRepo.GetByIdAsync(deviceId);
            if (d != null)
            {
                d.IsDeleted = true;
                await _deviceRepo.UpdateAsync(d);
                
                var norm = LLMHub.NormalizeSerial(d.SerialNumber);
                await _cache.RemoveAsync($"bear:profile:config:{norm}");
                await _cache.RemoveAsync($"device:config:sn:{norm}");
            }
            return Result.Success();
        }

        public async Task<Result<IssueDeviceTokenResponse>> IssueDeviceTokenAsync(string deviceId)
        {
            var response = await _deviceIdentity.IssueTokenAsync(deviceId);
            if (response == null) return Result<IssueDeviceTokenResponse>.Failure(new Error("NotFound", "Device not found"));
            return Result<IssueDeviceTokenResponse>.Success(response);
        }

        public async Task<Result> RevokeDeviceTokenAsync(string deviceId, string tokenId)
        {
            var success = await _deviceIdentity.RevokeTokenAsync(deviceId, tokenId);
            return success ? Result.Success() : Result.Failure(new Error("NotFound", "Token not found or already revoked"));
        }

        // --- Song CRUD ---
        public async Task<Result<List<SongDto>>> GetSongsAsync()
        {
            var songs = await _songRepo.GetAllAsync();
            return Result<List<SongDto>>.Success(songs.Select(MapToDto).ToList());
        }

        public async Task<Result<SongDto>> CreateSongAsync(Song song)
        {
            var s = await _songRepo.AddAsync(song);
            return Result<SongDto>.Success(MapToDto(s));
        }

        public async Task<Result> DeleteSongAsync(string id)
        {
            var s = await _songRepo.GetByIdAsync(id);
            if (s != null)
            {
                s.IsDeleted = true;
                await _songRepo.UpdateAsync(s);
            }
            return Result.Success();
        }

        public async Task<Result> UpdateSongAsync(Song song)
        {
            var existing = await _songRepo.GetByIdAsync(song.Id);
            if (existing == null) return Result.Failure(new Error("NotFound", "Song not found"));

            existing.Name = song.Name;
            existing.Artist = song.Artist;
            if (!string.IsNullOrWhiteSpace(song.AudioUrl)) existing.AudioUrl = song.AudioUrl;
            if (!string.IsNullOrWhiteSpace(song.GcsPath)) existing.GcsPath = song.GcsPath;

            await _songRepo.UpdateAsync(existing);
            return Result.Success();
        }

        // --- Story CRUD ---
        public async Task<Result<List<StoryDto>>> GetStoriesAsync()
        {
            var stories = await _storyRepo.GetAllAsync();
            return Result<List<StoryDto>>.Success(stories.Select(MapToDto).ToList());
        }

        public async Task<Result<StoryDto>> CreateStoryAsync(Story story)
        {
            var s = await _storyRepo.AddAsync(story);
            return Result<StoryDto>.Success(MapToDto(s));
        }

        public async Task<Result> DeleteStoryAsync(string id)
        {
            var s = await _storyRepo.GetByIdAsync(id);
            if (s != null)
            {
                s.IsDeleted = true;
                await _storyRepo.UpdateAsync(s);
            }
            return Result.Success();
        }

        public async Task<Result> UpdateStoryAsync(Story story)
        {
            var existing = await _storyRepo.GetByIdAsync(story.Id);
            if (existing == null) return Result.Failure(new Error("NotFound", "Story not found"));

            existing.Name = story.Name;
            if (!string.IsNullOrWhiteSpace(story.GcsPath)) existing.GcsPath = story.GcsPath;
            if (!string.IsNullOrWhiteSpace(story.ContentType)) existing.ContentType = story.ContentType;

            await _storyRepo.UpdateAsync(existing);
            return Result.Success();
        }

        // --- Dashboard & Analytics ---
        public async Task<Result<AdminDashboardStatsDto>> GetDashboardStatsAsync()
        {
            try
            {
                var users = await _userRepo.GetAllAsync();
                var devices = await _deviceRepo.GetAllAsync();
                var songs = await _songRepo.GetAllAsync();
                var stories = await _storyRepo.GetAllAsync();
                var allOrders = await _orderRepo.GetAllOrdersAsync();
                
                long revenue = 0;
                try 
                {
                    revenue = await _orderRepo.GetFulfilledRevenueAsync();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"[AdminService] Revenue calculation failed: {ex.Message}");
                }

                var lastOrder = allOrders.OrderByDescending(o => o.CreatedAtUtc).FirstOrDefault();

                var musicBytes = await _storageService.CalculateBucketStorageAsync(_config["GCS:MusicBucket"] ?? "gau-bear-media-kho-1");
                var storyBytes = await _storageService.CalculateBucketStorageAsync(_config["GCS:StoryBucket"] ?? "gau_media_kechuyen");

                var stats = new AdminDashboardStatsDto
                {
                    TotalUsers = users.Count,
                    ProUsers = users.Count(u => u.IsPro),
                    TotalDevices = devices.Count,
                    TotalSongs = songs.Count,
                    TotalStories = stories.Count,
                    MusicStorageMb = Math.Round(musicBytes / (1024.0 * 1024.0), 2),
                    StoryStorageKb = Math.Round(storyBytes / 1024.0, 2),
                    SuccessfulOrdersCount = allOrders.Count(o => o.IsFulfilled),
                    LastOrderDate = lastOrder?.CreatedAtUtc,
                    LastOrderAmount = lastOrder?.Amount ?? 0,
                    ActiveSessions = 0,
                    TotalRevenueVnd = revenue,
                    LastSyncTime = DateTime.UtcNow
                };

                return Result<AdminDashboardStatsDto>.Success(stats);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AdminService] GetDashboardStatsAsync failed: {ex}");
                return Result<AdminDashboardStatsDto>.Failure(new Error("InternalServerError", ex.Message));
            }
        }

        public async Task<Result<List<PendingOrder>>> GetOrdersAsync()
        {
            var orders = await _orderRepo.GetAllOrdersAsync();
            return Result<List<PendingOrder>>.Success(orders);
        }

        public async Task<Result<List<Voucher>>> GetVouchersAsync()
        {
            var vouchers = await _voucherRepo.GetAllAsync();
            return Result<List<Voucher>>.Success(vouchers);
        }



        public async Task<Result<string>> UploadMediaAsync(Stream stream, string fileName, string category)
        {
            var type = category.ToLower() switch
            {
                "music" => MediaType.Music,
                "story" => MediaType.Story,
                "demovoice" => MediaType.DemoVoice,
                _ => (MediaType?)null
            };

            if (type == null) return Result<string>.Failure(new Error("BadRequest", "Invalid media category."));

            var success = await _mediaService.UploadMediaAsync(stream, fileName, type.Value);
            if (!success) return Result<string>.Failure(new Error("UploadError", "Failed to upload media."));

            // Auto-confirm to ensure DB record exists
            await _mediaService.ConfirmUploadAsync(fileName, type.Value);

            var bucket = GetBucketName(type.Value);
            var prefix = GetPrefix(type.Value);
            
            var extension = Path.GetExtension(fileName);
            var baseName = Path.GetFileNameWithoutExtension(fileName);
            var normalizedName = MediaService.Normalize(baseName) + extension;

            var fullPath = string.IsNullOrEmpty(prefix) ? normalizedName : $"{prefix}/{normalizedName}";

            return Result<string>.Success(fullPath);
        }



        public async Task<Result<string>> SpeakAsync(string text, string voiceId, string provider)
        {
            try 
            {
                if (string.IsNullOrWhiteSpace(text)) return Result<string>.Failure(new Error("ValidationError", "Văn bản không được để trống"));
                
                var wordCount = text.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
                if (wordCount > 20) return Result<string>.Failure(new Error("ValidationError", "Văn bản demo không được quá 20 chữ"));

                var url = await _mediaService.SpeakAsync(text, voiceId, provider);
                return Result<string>.Success(url);
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(new Error("TTSError", $"Lỗi phát âm: {ex.Message}"));
            }
        }

        public async Task<Result<string>> GetUploadUrlAsync(string fileName, string category)
        {
            var type = ResolveMediaType(category);
            if (type == null) return Result<string>.Failure(new Error("BadRequest", "Invalid media category."));
            var url = await _mediaService.GetUploadUrlAsync(fileName, type.Value);
            return Result<string>.Success(url);
        }

        public async Task<Result> ConfirmUploadAsync(string fileName, string category, string? name = null, string? displayInfo = null, string? id = null)
        {
            var type = ResolveMediaType(category);
            if (type == null) return Result.Failure(new Error("BadRequest", "Invalid media category."));
            var success = await _mediaService.ConfirmUploadAsync(fileName, type.Value, name, displayInfo, id);
            return success ? Result.Success() : Result.Failure(new Error("Error", "Confirmation failed."));
        }



        private MediaType? ResolveMediaType(string category) => category.ToLower() switch
        {
            "music" => MediaType.Music,
            "story" => MediaType.Story,
            "demovoice" => MediaType.DemoVoice,
            _ => (MediaType?)null
        };

        public async Task<Result> UpdateProfileSubscriptionAsync(string profileId, int subscriptionPlanId)
        {
            var profile = await _profileRepo.GetByIdAsync(profileId);
            if (profile == null) return Result.Failure(new Error("NotFound", "Profile not found"));

            if (profile.ParentUser == null) return Result.Failure(new Error("InvalidOperation", "Profile has no parent user"));

            var user = profile.ParentUser;
            user.IsPro = true;
            user.SubscriptionPlanId = subscriptionPlanId;
            user.ProExpiresAt = DateTime.UtcNow.AddMonths(1); // Default to 1 month for manual update
            
            await _userRepo.UpdateAsync(user);

            int candyLimit = subscriptionPlanId switch
            {
                1 => 10,
                2 => 50,
                3 => 300,
                _ => 10
            };

            var children = await _profileRepo.GetForUserAsync(user.UserId);
            foreach (var c in children)
            {
                c.DailyCandyLimit = candyLimit;
                c.DailyCandyBalance = candyLimit;
                await _profileRepo.UpdateAsync(c);
                
                // Invalidate quota cache
                await _cache.RemoveAsync($"quota:daily:candy:{c.Id}");
            }

            // Invalidate device config cache for all devices owned by this user
            var userDevices = await _deviceRepo.GetDevicesWithDetailsByUserAsync(user.UserId);
            foreach (var d in userDevices)
            {
                var normSn = LLMHub.NormalizeSerial(d.SerialNumber);
                var normId = LLMHub.NormalizeSerial(d.DeviceId);
                await _cache.RemoveAsync($"bear:profile:config:{normSn}");
                await _cache.RemoveAsync($"bear:profile:config:{normId}");
                await _cache.RemoveAsync($"device:config:sn:{normSn}");
                await _cache.RemoveAsync($"device:config:id:{normId}");
            }

            return Result.Success();
        }

        public async Task<Result<LearningRecommendationResponse>> GetLearningRecommendationAsync(string profileId)
        {
            var profile = await _profileRepo.GetByIdAsync(profileId);
            if (profile == null) return Result<LearningRecommendationResponse>.Failure(new Error("NotFound", "Profile not found"));

            var prompt = _promptBuilder.Build(profile);
            var recommendation = await _gemini.Generate(prompt);

            return Result<LearningRecommendationResponse>.Success(new LearningRecommendationResponse
            {
                ProfileId = profile.Id,
                ChildName = profile.Name,
                Recommendation = recommendation,
                GeneratedAt = DateTime.UtcNow
            });
        }

        // --- User Management ---
        public async Task<Result<List<UserDto>>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return Result<List<UserDto>>.Success(users.Select(MapToDto).ToList());
        }

        public async Task<Result<UserDto>> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return Result<UserDto>.Failure(new Error("NotFound", "User not found"));
            return Result<UserDto>.Success(MapToDto(user));
        }

        public async Task<Result> UpdateUserRoleAsync(Guid userId, int roleId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return Result.Failure(new Error("NotFound", "User not found"));

            user.RoleId = roleId;
            await _userRepo.UpdateAsync(user);
            return Result.Success();
        }

        private string? GetBucketName(MediaType type) => type switch
        {
            MediaType.Story => _config["GCS:StoryBucket"],
            MediaType.Music => _config["GCS:MusicBucket"],
            MediaType.DemoVoice => _config["GCS:DemoVoiceBucket"],
            _ => null
        };

        private string GetPrefix(MediaType type) => type switch
        {
            MediaType.Story => _config["GCS:StoryPrefix"] ?? "stories",
            MediaType.Music => _config["GCS:MusicPrefix"] ?? "music",
            MediaType.DemoVoice => _config["GCS:DemoVoicePrefix"] ?? "voices",
            _ => ""
        };

        // --- Mapping Helpers ---
        private ChildProfileDto MapToDto(ChildProfile p) => new()
        {
            Id = p.Id,
            Name = p.Name,
            Age = p.Age,
            Gender = p.Gender,
            CurrentMode = p.CurrentMode,
            SubscriptionEndUtc = p.ParentUser?.ProExpiresAt,
            SubscriptionStatus = p.ParentUser?.SubscriptionStatus ?? SubscriptionStatus.Trial,
            DailyCandyBalance = p.DailyCandyBalance
        };

        private UserDto MapToDto(User u) => new()
        {
            UserId = u.UserId,
            Email = u.Email,
            FullName = u.FullName,
            Provider = u.Provider,
            IsPro = u.IsPro,
            SmartCandies = u.SmartCandies,
            RoleId = u.RoleId,
            CreatedAt = u.CreatedAt
        };

        private SongDto MapToDto(Song s) => new()
        {
            Id = s.Id,
            Name = s.Name,
            Artist = s.Artist,
            AudioUrl = s.AudioUrl,
            GcsPath = s.GcsPath
        };

        private StoryDto MapToDto(Story s) => new()
        {
            Id = s.Id,
            Name = s.Name,
            ContentType = s.ContentType,
            GcsPath = s.GcsPath,
            AudioUrl = s.GcsPath, // Alias for dashboard consistency
            CreatedAt = s.CreatedAt
        };
    }
}
