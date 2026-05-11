using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Services.Interfaces;
using SmartBearServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SmartBearServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoiceController : BaseController
    {
        private readonly IMediaService _mediaService;
        private readonly IVoiceService _voiceService;
        private readonly AppDbContext _db;

        public VoiceController(IMediaService mediaService, IVoiceService voiceService, AppDbContext db)
        {
            _mediaService = mediaService;
            _voiceService = voiceService;
            _db = db;
        }

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<IActionResult> GetVoiceList()
        {
            var voices = await _voiceService.GetVoiceListAsync();
            return Ok(voices);
        }

        [HttpPost("preference")]
        public async Task<IActionResult> UpdatePreference([FromBody] VoicePreferenceRequest request)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return NotFound("User not found");

            user.PreferredTtsProvider = request.Provider;
            user.PreferredVoiceId = request.VoiceId;
            user.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return Ok(new { Message = "Preference updated" });
        }

        [HttpPost("speak")]
        [AllowAnonymous]
        public async Task<IActionResult> Speak([FromBody] SpeakRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Text)) return BadRequest("Text is required");
            var wordCount = request.Text.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
            if (wordCount > 20) return BadRequest("Văn bản demo không được quá 20 chữ");

            var url = await _mediaService.SpeakAsync(request.Text, request.VoiceId, request.Provider);
            return Ok(new { Value = url });
        }
    }

    public class SpeakRequest
    {
        public string Text { get; set; }
        public string VoiceId { get; set; }
        public string Provider { get; set; }
    }

    public class VoicePreferenceRequest
    {
        public string Provider { get; set; }
        public string VoiceId { get; set; }
    }
}
