using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Model;
using SmartBearServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartBearServer.Controllers
{
    /// <summary>
    /// Controller for managing individual child profile safety settings (Level 3).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SafetyController : BaseController
    {
        private readonly IBannedWordService _bannedWordService;

        public SafetyController(IBannedWordService bannedWordService)
        {
            _bannedWordService = bannedWordService;
        }

        /// <summary>
        /// Updates the safety response mode for a specific child profile (e.g., Pretend vs Warning).
        /// Expected by FE at: api/safety/mode/{profileId}
        /// </summary>
        [HttpPost("mode/{profileId}")]
        public async Task<IActionResult> UpdateResponseMode(string profileId, [FromQuery] SafetyResponseMode mode)
        {
            var userId = GetUserId();
            var success = await _bannedWordService.UpdateSafetySettingsAsync(userId, profileId, null, (int)mode);
            
            if (!success)
            {
                return HandleResult(Result<object>.Failure(new Error("NotFound", "Profile not found or access denied.")));
            }

            return HandleResult(Result<object>.Success(new 
            { 
                ProfileId = profileId, 
                ResponseMode = mode.ToString(),
                ModeValue = (int)mode
            }));
        }

        /// <summary>
        /// Updates the specific blocked topics for a child profile.
        /// </summary>
        [HttpPut("topics/{profileId}")]
        public async Task<IActionResult> UpdateBlockedTopics(string profileId, [FromBody] List<string> topics)
        {
            var userId = GetUserId();
            var success = await _bannedWordService.UpdateSafetySettingsAsync(userId, profileId, topics, null);
            
            if (!success)
            {
                return HandleResult(Result<object>.Failure(new Error("NotFound", "Profile not found or access denied.")));
            }

            return HandleResult(Result<object>.Success(new { ProfileId = profileId, BlockedTopics = topics }));
        }

        /// <summary>
        /// Updates the Level-3 banned keywords specifically for this bear profile.
        /// These are checked in addition to Global and Parent-level lists.
        /// </summary>
        [HttpPut("keywords/{profileId}")]
        public async Task<IActionResult> UpdateBearKeywords(string profileId, [FromBody] List<string> keywords)
        {
            var userId = GetUserId();
            var success = await _bannedWordService.UpdateProfileBannedKeywordsAsync(userId, profileId, keywords);
            
            if (!success)
            {
                return HandleResult(Result<object>.Failure(new Error("NotFound", "Profile not found or access denied.")));
            }

            return HandleResult(Result<object>.Success(new { ProfileId = profileId, BannedKeywords = keywords }));
        }

        private Guid GetUserId()
        {
            var val = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(val)) return Guid.Empty;
            return Guid.Parse(val);
        }
    }
}
