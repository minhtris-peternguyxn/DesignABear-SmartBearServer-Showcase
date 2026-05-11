using SmartBearServer.Model;
using SmartBearServer.Model.DTOs;
using SmartBearServer.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace SmartBearServer.Services.Interfaces
{
    public interface IAdminService
    {
        // --- ChildProfile Management ---
        Task<Result<List<ChildProfileDto>>> GetAllProfilesAsync();
        Task<Result<ChildProfileDto>> GetProfileAsync(string id);
        Task<Result<ChildProfileDto>> CreateProfileAsync(ChildProfile profile);
        Task<Result> UpdateProfileAsync(ChildProfile profile);
        Task<Result> DeleteProfileAsync(string id);
        Task<Result> UpdateProfileSubscriptionAsync(string profileId, int subscriptionPlanId);
        Task<Result<LearningRecommendationResponse>> GetLearningRecommendationAsync(string profileId);

        // --- Device Management ---
        Task<Result<List<DeviceDto>>> GetAllDevicesAsync();
        Task<Result<DeviceDto>> CreateDeviceAsync(Device device);
        Task<Result> UpdateDeviceAsync(Device device);
        Task<Result> DeleteDeviceAsync(string deviceId);
        Task<Result<IssueDeviceTokenResponse>> IssueDeviceTokenAsync(string deviceId);
        Task<Result> RevokeDeviceTokenAsync(string deviceId, string tokenId);

        // --- Story Management ---
        Task<Result<List<StoryDto>>> GetStoriesAsync();
        Task<Result<StoryDto>> CreateStoryAsync(Story story);
        Task<Result> UpdateStoryAsync(Story story);
        Task<Result> DeleteStoryAsync(string id);

        // --- Dashboard & Analytics ---
        Task<Result<AdminDashboardStatsDto>> GetDashboardStatsAsync();
        Task<Result<string>> UploadMediaAsync(Stream stream, string fileName, string category);
        Task<Result<string>> SpeakAsync(string text, string voiceId, string provider);
        Task<Result<string>> GetUploadUrlAsync(string fileName, string category);
        Task<Result> ConfirmUploadAsync(string fileName, string category, string? name = null, string? displayInfo = null, string? id = null);

        // --- Order & Financials ---
        Task<Result<List<PendingOrder>>> GetOrdersAsync();
        Task<Result<List<Voucher>>> GetVouchersAsync();

        // --- Song Management ---
        Task<Result<List<SongDto>>> GetSongsAsync();
        Task<Result<SongDto>> CreateSongAsync(Song song);
        Task<Result> UpdateSongAsync(Song song);
        Task<Result> DeleteSongAsync(string id);

        // --- User Management ---
        Task<Result<List<UserDto>>> GetAllUsersAsync();
        Task<Result<UserDto>> GetUserByIdAsync(Guid userId);
        Task<Result> UpdateUserRoleAsync(Guid userId, int roleId);
    }
}
