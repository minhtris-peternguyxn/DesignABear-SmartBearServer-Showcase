using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBearServer.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SmartBearServer.Controllers
{
    /// <summary>
    /// Manages banned words across three safety categories: System (Global), Parent (Account), and Child (Profile).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BannedWordsController : BaseController
    {
        private readonly IBannedWordService _bannedWordService;
        private readonly AppDbContext _db;

        public BannedWordsController(IBannedWordService bannedWordService, AppDbContext db)
        {
            _bannedWordService = bannedWordService;
            _db = db;
        }

        /// <summary>
        /// Returns banned words visible to the authenticated user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetMyBannedWords()
        {
            var userId = GetUserId();
            var words = await _bannedWordService.GetForUserOnlyAsync(userId);
            return HandleResult(Result<object>.Success(words));
        }

        /// <summary>
        /// Adds a new banned word to the system or user-specific list.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddBannedWord([FromBody] AddBannedWordRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetUserId();
            var isMaster = await IsMasterUser();

            try
            {
                var entry = await _bannedWordService.AddBannedWordAsync(
                    userId, 
                    request.Word, 
                    request.Category, 
                    false, // Parents cannot add global words
                    false);
                
                return HandleResult(Result<object>.Success(entry));
            }
            catch (Exception ex)
            {
                return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("BadRequest", ex.Message)));
            }
        }

        /// <summary>
        /// Deletes a specific banned word entry.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWord(int id)
        {
            var userId = GetUserId();
            var success = await _bannedWordService.DeleteBannedWordAsync(userId, id, false);
            if (!success)
            {
                return HandleResult(Result.Failure(new Infrastructure.Common.Error("Forbidden", "You do not have permission to delete this entry or it does not exist.")));
            }

            return HandleResult(Result<object>.Success(new { Message = "Successfully deleted." }));
        }


        private Guid GetUserId()
        {
            var val = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(val);
        }

        private async Task<bool> IsMasterUser()
        {
            var userId = GetUserId();
            var user = await _db.Users.FindAsync(userId);
            return user?.RoleId == 1; // 1 is Master role
        }
    }

    public class AddBannedWordRequest
    {
        [Required]
        [MaxLength(200)]
        public string Word { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Category { get; set; }

        public bool IsGlobal { get; set; } = false;
    }
}
