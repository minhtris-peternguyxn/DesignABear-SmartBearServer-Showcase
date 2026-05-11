using Microsoft.AspNetCore.Mvc;
using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Model.DTOs;
using SmartBearServer.Services.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartBearServer.Controllers
{
    /// <summary>
    /// Handles smart bear alarm operations including scheduling and status management.
    /// Inherits from <see cref="BaseController"/> for standardized Result-to-HTTP mapping.
    /// </summary>
    [Route("api/[controller]")]
    public class SmartAlarmController : BaseController
    {
        private readonly ISmartAlarmService _alarmService;

        public SmartAlarmController(ISmartAlarmService alarmService)
        {
            _alarmService = alarmService;
        }

        /// <summary>
        /// Retrieves all alarms configured for devices owned by the authenticated user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetMyAlarms()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            var result = await _alarmService.GetMyAlarmsAsync(userId.Value);
            return HandleResult(result);
        }

        /// <summary>
        /// Retrieves a specific alarm by its unique identifier.
        /// </summary>
        [HttpGet("{alarmId}")]
        public async Task<IActionResult> GetAlarmById(string alarmId)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            var result = await _alarmService.GetMyAlarmByIdAsync(userId.Value, alarmId);
            return HandleResult(result);
        }

        /// <summary>
        /// Creates a new alarm for a bear device.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAlarm([FromBody] SmartAlarmRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            var result = await _alarmService.CreateAlarmAsync(userId.Value, request);
            return HandleResult(result);
        }

        /// <summary>
        /// Updates the settings (time, message, voice) for an existing alarm.
        /// </summary>
        [HttpPut("{alarmId}")]
        public async Task<IActionResult> UpdateAlarm(string alarmId, [FromBody] UpdateSmartAlarmRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            var result = await _alarmService.UpdateAlarmAsync(userId.Value, alarmId, request);
            return HandleResult(result);
        }

        /// <summary>
        /// Toggles an alarm between enabled and disabled states.
        /// </summary>
        [HttpPatch("{alarmId}/toggle")]
        public async Task<IActionResult> ToggleAlarm(string alarmId)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            var result = await _alarmService.ToggleAlarmAsync(userId.Value, alarmId);
            return HandleResult(result);
        }

        /// <summary>
        /// Deletes a specific alarm.
        /// </summary>
        [HttpDelete("{alarmId}")]
        public async Task<IActionResult> DeleteAlarm(string alarmId)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result<object>.Failure(new Infrastructure.Common.Error("Unauthorized", "User not found")));

            var result = await _alarmService.DeleteAlarmAsync(userId.Value, alarmId);
            return HandleResult(result);
        }

        private Guid? GetCurrentUserId()
        {
            var sub = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
            return Guid.TryParse(sub, out var guid) ? guid : null;
        }
    }
}
