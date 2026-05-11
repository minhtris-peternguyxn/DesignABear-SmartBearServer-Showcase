using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBearServer.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SmartBearServer.Services.Interfaces;

namespace SmartBearServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : BaseController
    {
        private readonly AppDbContext _context;

        public HistoryController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the list of chat sessions for a child profile.
        /// Grouped by date and sorted descending.
        /// </summary>
        [HttpGet("sessions/{profileId}")]
        public async Task<IActionResult> GetSessions(string profileId)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr)) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            // Verify the profile belongs to the user via Device link
            // Optimization: Normalize profileId
            var normProfileId = profileId.Trim();
            var ownsProfile = await _context.Devices
                .AnyAsync(d => d.UserId == Guid.Parse(userIdStr) && d.ProfileId == normProfileId);
            
            if (!ownsProfile)
            {
                return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Forbidden", "Access Denied")));
            }

            var sessions = await _context.ChatSessions
                .Where(s => s.ProfileId == normProfileId)
                .OrderByDescending(s => s.LastInteractionTime)
                .Select(s => new
                {
                    s.Id,
                    s.StartTime,
                    s.LastInteractionTime,
                    s.EndTime,
                    s.Title,
                    s.Summary,
                    s.Category,
                    s.IsActive,
                    InteractionCount = s.Interactions.Count
                })
                .ToListAsync();

            return HandleResult(Result<object>.Success(sessions));
        }

        /// <summary>
        /// Get the full transcript of a specific session.
        /// </summary>
        [HttpGet("session/{sessionId}")]
        public async Task<IActionResult> GetSessionDetails(Guid sessionId)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr)) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            var session = await _context.ChatSessions
                .Include(s => s.Interactions)
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("NotFound", "Session not found")));

            // Verify ownership
            var ownsProfile = await _context.Devices
                .AnyAsync(d => d.UserId == Guid.Parse(userIdStr) && d.ProfileId == session.ProfileId);
            
            if (!ownsProfile) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Forbidden", "Access Denied")));

            var result = new
            {
                session.Id,
                session.Title,
                session.Summary,
                session.Category,
                session.StartTime,
                session.EndTime,
                Interactions = session.Interactions
                    .OrderBy(i => i.Timestamp)
                    .Select(i => new
                    {
                        i.Id,
                        i.Request,
                        i.Response,
                        i.Timestamp,
                        i.IsSafe,
                        i.SafetyViolationCategory
                    })
            };

            return HandleResult(Result<object>.Success(result));
        }

        /// <summary>
        /// Delete a session and its history.
        /// </summary>
        [HttpDelete("session/{sessionId}")]
        public async Task<IActionResult> DeleteSession(Guid sessionId)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr)) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            var session = await _context.ChatSessions.FindAsync(sessionId);
            if (session == null) return HandleResult(Result.Failure(new Infrastructure.Common.Error("NotFound", "Session not found")));

            var ownsProfile = await _context.Devices
                .AnyAsync(d => d.UserId == Guid.Parse(userIdStr) && d.ProfileId == session.ProfileId);
            
            if (!ownsProfile) return HandleResult(Result.Failure(new Infrastructure.Common.Error("Forbidden", "Access Denied")));

            _context.ChatSessions.Remove(session);
            await _context.SaveChangesAsync();

            return HandleResult(Result<object>.Success(new { status = "ok" }));
        }
    }
}
