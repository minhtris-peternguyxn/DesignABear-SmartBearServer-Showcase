using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Model;
using SmartBearServer.Services;
using SmartBearServer.Services.Interfaces;
using SmartBearServer.Services;

using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using SmartBearServer.Model.DTOs;

namespace SmartBearServer.Controllers
{
    /// <summary>
    /// Admin Controller
    /// Managed dashboard statistics and generic CRUD for system entities.
    /// Secured with Role=1 (Admin).
    /// </summary>
    public class MediaUploadRequest
    {
        public required IFormFile File { get; set; }
        public required string Category { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles="1")]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        private readonly IBannedWordService _bannedWordService;
        private readonly GcsSyncService _syncService;
        private readonly IVoiceService _voiceService;

        public AdminController(IAdminService adminService, IBannedWordService bannedWordService, GcsSyncService syncService, IVoiceService voiceService)
        {
            _adminService = adminService;
            _bannedWordService = bannedWordService;
            _syncService = syncService;
            _voiceService = voiceService;
        }

        [HttpPost("sync")]
        public async Task<IActionResult> SyncMedia()
        {
            await _syncService.SyncAllAsync();
            return Ok(new { Message = "Synchronization completed successfully." });
        }



        // --- Dashboard & Analytics ---
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats() => HandleResult(await _adminService.GetDashboardStatsAsync());

        // --- ChildProfile Management ---
        [HttpGet("profiles")]
        public async Task<IActionResult> GetProfiles() => HandleResult(await _adminService.GetAllProfilesAsync());

        [HttpGet("profiles/{id}")]
        public async Task<IActionResult> GetProfile(string id) => HandleResult(await _adminService.GetProfileAsync(id));

        [HttpPost("profiles")]
        public async Task<IActionResult> CreateProfile([FromBody] ChildProfile profile) => HandleResult(await _adminService.CreateProfileAsync(profile));

        [HttpPut("profiles/{id}")]
        public async Task<IActionResult> UpdateProfile(string id, [FromBody] ChildProfile profile)
        {
            if (id != profile.Id) return HandleResult(Result.Failure(new Error("BadRequest", "ID mismatch")));
            return HandleResult(await _adminService.UpdateProfileAsync(profile));
        }

        [HttpPut("profiles/{id}/subscription")]
        public async Task<IActionResult> UpdateProfileSubscription(string id, [FromBody] UpdateSubscriptionRequest request) 
            => HandleResult(await _adminService.UpdateProfileSubscriptionAsync(id, request.SubscriptionPlanId));

        [HttpGet("profiles/{id}/learning-recommendation")]
        public async Task<IActionResult> GetLearningRecommendation(string id) => HandleResult(await _adminService.GetLearningRecommendationAsync(id));

        [HttpDelete("profiles/{id}")]
        public async Task<IActionResult> DeleteProfile(string id) => HandleResult(await _adminService.DeleteProfileAsync(id));

        // --- Device Management ---
        [HttpGet("devices")]
        public async Task<IActionResult> GetDevices() => HandleResult(await _adminService.GetAllDevicesAsync());

        [HttpPost("devices")]
        public async Task<IActionResult> CreateDevice([FromBody] Device device) => HandleResult(await _adminService.CreateDeviceAsync(device));

        [HttpPut("devices/{id}")]
        public async Task<IActionResult> UpdateDevice(string id, [FromBody] Device device)
        {
            if (id != device.DeviceId) return HandleResult(Result.Failure(new Error("BadRequest", "Device ID mismatch")));
            return HandleResult(await _adminService.UpdateDeviceAsync(device));
        }

        [HttpDelete("devices/{id}")]
        public async Task<IActionResult> DeleteDevice(string id) => HandleResult(await _adminService.DeleteDeviceAsync(id));

        [HttpPost("devices/{id}/tokens")]
        public async Task<IActionResult> IssueDeviceToken(string id) => HandleResult(await _adminService.IssueDeviceTokenAsync(id));

        [HttpDelete("devices/{deviceId}/tokens/{tokenId}")]
        public async Task<IActionResult> RevokeDeviceToken(string deviceId, string tokenId) => HandleResult(await _adminService.RevokeDeviceTokenAsync(deviceId, tokenId));

        // --- Media Management ---
        [HttpGet("songs")]
        public async Task<IActionResult> GetSongs() => HandleResult(await _adminService.GetSongsAsync());

        [HttpPost("songs")]
        public async Task<IActionResult> CreateSong([FromBody] Song song) => HandleResult(await _adminService.CreateSongAsync(song));

        [HttpPut("songs/{id}")]
        public async Task<IActionResult> UpdateSong(string id, [FromBody] Song song)
        {
            if (id != song.Id) return HandleResult(Result.Failure(new Error("BadRequest", "ID mismatch")));
            return HandleResult(await _adminService.UpdateSongAsync(song));
        }

        [HttpDelete("songs/{id}")]
        public async Task<IActionResult> DeleteSong(string id) => HandleResult(await _adminService.DeleteSongAsync(id));

        [HttpGet("stories")]
        public async Task<IActionResult> GetStories() => HandleResult(await _adminService.GetStoriesAsync());

        [HttpPost("stories")]
        public async Task<IActionResult> CreateStory([FromBody] Story story) => HandleResult(await _adminService.CreateStoryAsync(story));

        [HttpPut("stories/{id}")]
        public async Task<IActionResult> UpdateStory(string id, [FromBody] Story story)
        {
            if (id != story.Id) return HandleResult(Result.Failure(new Error("BadRequest", "ID mismatch")));
            return HandleResult(await _adminService.UpdateStoryAsync(story));
        }

        [HttpDelete("stories/{id}")]
        public async Task<IActionResult> DeleteStory(string id) => HandleResult(await _adminService.DeleteStoryAsync(id));

        [HttpPost("media/upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadMedia([FromForm] MediaUploadRequest request)
        {
            if (request.File == null || request.File.Length == 0) return BadRequest("File is empty");
            
            using var stream = request.File.OpenReadStream();
            var result = await _adminService.UploadMediaAsync(stream, request.File.FileName, request.Category);
            return HandleResult(result);
        }

        [HttpGet("check-route")]
        public IActionResult CheckRoute() => Ok("Admin Controller is Alive and Updated!");

        [HttpPost("/api/Admin/request-upload")]
        public async Task<IActionResult> RequestUpload([FromBody] RequestUploadRequest request)
        {
            var result = await _adminService.GetUploadUrlAsync(request.FileName, request.Category);
            return HandleResult(result);
        }

        [HttpPost("/api/Admin/confirm-upload")]
        public async Task<IActionResult> ConfirmUpload([FromBody] ConfirmUploadRequest request)
        {
            var result = await _adminService.ConfirmUploadAsync(request.FileName, request.Category, request.Name, request.DisplayInfo, request.Id);
            return HandleResult(result);
        }

        // --- Voice Catalog Management ---
        [HttpGet("voices")]
        public async Task<IActionResult> GetVoices()
        {
            var voices = await _voiceService.GetAllVoicesAsync();
            return Ok(new { value = voices });
        }

        [HttpPost("voices")]
        public async Task<IActionResult> CreateVoice([FromBody] DemoVoice voice)
        {
            var created = await _voiceService.AddVoiceAsync(voice);
            return Ok(new { value = created });
        }

        [HttpPut("voices/{id}")]
        public async Task<IActionResult> UpdateVoice(string id, [FromBody] DemoVoice voice)
        {
            var success = await _voiceService.UpdateVoiceAsync(id, voice);
            if (!success) return NotFound(new { error = new { description = "Voice not found" } });
            return Ok(new { message = "Updated" });
        }

        [HttpDelete("voices/{id}")]
        public async Task<IActionResult> DeleteVoice(string id)
        {
            var success = await _voiceService.DeleteVoiceAsync(id);
            if (!success) return NotFound(new { error = new { description = "Voice not found" } });
            return Ok(new { message = "Deleted" });
        }

        // --- User Management ---
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers() => HandleResult(await _adminService.GetAllUsersAsync());

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(Guid id) => HandleResult(await _adminService.GetUserByIdAsync(id));

        [HttpPut("users/{id}/role")]
        public async Task<IActionResult> UpdateUserRole(Guid id, [FromBody] int roleId) => HandleResult(await _adminService.UpdateUserRoleAsync(id, roleId));

        // --- Global Safety Management (Level 1) ---
        [HttpGet("safety")]
        public async Task<IActionResult> GetGlobalSafety()
        {
            var words = await _bannedWordService.GetGlobalBannedWordsAsync();
            return HandleResult(Result<object>.Success(words));
        }

        [HttpPost("safety")]
        public async Task<IActionResult> AddGlobalWord([FromBody] AdminAddBannedWordRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier)!);
            var entry = await _bannedWordService.AddBannedWordAsync(userId, request.Word, request.Category, true, true);
            return HandleResult(Result<object>.Success(entry));
        }

        [HttpDelete("safety/{id}")]
        public async Task<IActionResult> DeleteGlobalWord(int id)
        {
            var userId = Guid.Parse(User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier)!);
            var success = await _bannedWordService.DeleteBannedWordAsync(userId, id, true);
            if (!success) return HandleResult(Result.Failure(new Error("NotFound", "Global keyword not found.")));
            return HandleResult(Result<object>.Success(new { Message = "Global keyword deleted." }));
        }
    }

    public class AdminAddBannedWordRequest
    {
        public string Word { get; set; } = string.Empty;
        public string? Category { get; set; }
    }

}
