using Microsoft.AspNetCore.Authorization;
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
    /// Manages parent-facing device operations (profile assignment, modes, etc.).
    /// Inherits from <see cref="BaseController"/> to support standardized Result handling.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class DeviceController : BaseController
    {
        private readonly IDeviceService _deviceService;
        private readonly IPairingService _pairingService;

        public DeviceController(IDeviceService deviceService, IPairingService pairingService)
        {
            _deviceService = deviceService;
            _pairingService = pairingService;
        }

        /// <summary>
        /// Returns all bear devices owned by the authenticated parent user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetMyDevices()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result.Failure(new Error("Unauthorized", "User identity not found in token.")));

            var result = await _deviceService.GetDevicesByUserAsync(userId.Value);
            return HandleResult(result);
        }

        /// <summary>
        /// Pairs a bear device using its serial number (legacy/direct flow).
        /// </summary>
        [HttpPost("pair")]
        public async Task<IActionResult> PairDevice([FromBody] PairDeviceRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result.Failure(new Error("Unauthorized", "User identity not found in token.")));

            // Note: New flow uses PairingController. This is kept for compatibility.
            // We map to ClaimDeviceAsync or a new simplified pair method in PairingService if needed.
            // For now, using IPairingService for consistency.
            var result = await _pairingService.RequestPairingCodeAsync(request.SerialNumber);
            return HandleResult(result);
        }

        /// <summary>
        /// Removes the link between a device and the parent account.
        /// </summary>
        [HttpDelete("unpair/{deviceId}")]
        public async Task<IActionResult> UnpairDevice(string deviceId)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result.Failure(new Error("Unauthorized", "User identity not found in token.")));

            var result = await _deviceService.UnpairDeviceAsync(userId.Value, deviceId);
            return HandleResult(result);
        }

        /// <summary>
        /// Assigns a child profile to a device.
        /// </summary>
        [HttpPost("profile/assign")]
        public async Task<IActionResult> AssignProfile([FromBody] AssignProfileRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result.Failure(new Error("Unauthorized", "User identity not found in token.")));

            var result = await _deviceService.AssignProfileAsync(userId.Value, request);
            return HandleResult(result);
        }

        /// <summary>
        /// Sets the active AI interaction mode (Normal, Math, Bilingual).
        /// </summary>
        [HttpPost("mode")]
        public async Task<IActionResult> SetMode([FromBody] SetDeviceModeRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result.Failure(new Error("Unauthorized", "User identity not found in token.")));

            var result = await _deviceService.SetDeviceModeAsync(userId.Value, request);
            return HandleResult(result);
        }

        /// <summary>
        /// Toggles the hardware protection (activation lock) on an owned device.
        /// </summary>
        [HttpPost("{deviceId}/protection")]
        public async Task<IActionResult> ToggleProtection(string deviceId, [FromBody] ToggleProtectionRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result.Failure(new Error("Unauthorized", "User identity not found in token.")));

            var result = await _deviceService.ToggleHardwareProtectionAsync(userId.Value, deviceId, request.IsEnabled);
            return HandleResult(result);
        }

        /// <summary>
        /// Updates a child profile's personalization settings.
        /// </summary>
        [HttpPut("profile/{profileId}")]
        public async Task<IActionResult> UpdateProfile(string profileId, [FromBody] UpdateProfileRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result.Failure(new Error("Unauthorized", "User identity not found in token.")));

            var result = await _deviceService.UpdateProfileAsync(userId.Value, profileId, request);
            if (!result.IsSuccess) return HandleResult(result);

            // Fetch the device to return a complete DeviceDto
            var deviceResult = await _deviceService.GetDevicesByUserAsync(userId.Value);
            var deviceDto = deviceResult.Value?.FirstOrDefault(d => d.ProfileId == profileId);
            
            return Ok(deviceDto);
        }

        /// <summary>
        /// Creates a new child profile and assigns it to the specified device.
        /// </summary>
        [HttpPost("profile/{deviceId}")]
        public async Task<IActionResult> CreateProfile(string deviceId, [FromBody] UpdateProfileRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result.Failure(new Error("Unauthorized", "User identity not found in token.")));

            var result = await _deviceService.CreateProfileAsync(userId.Value, deviceId, request);
            if (!result.IsSuccess) return HandleResult(result);

            // Fetch the device to return a complete DeviceDto
            var deviceResult = await _deviceService.GetDevicesByUserAsync(userId.Value);
            var deviceDto = deviceResult.Value?.FirstOrDefault(d => d.DeviceId == deviceId);

            return Ok(deviceDto);
        }

        /// <summary>
        /// Proxies the status check to the bridge via the backend.
        /// Also supports legacy /check-mac path for bridge proxy compatibility.
        /// </summary>
        [HttpGet("status/{mac}")]
        [HttpGet("/check-mac/{mac}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDeviceStatus(string mac)
        {
            var result = await _deviceService.IsDeviceOnlineAsync(mac);
            if (result.IsSuccess)
            {
                return Ok(new { online = result.Value });
            }
            return Ok(new { online = false });
        }

        private Guid? GetCurrentUserId()
        {
            var sub = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
            return Guid.TryParse(sub, out var guid) ? guid : null;
        }
    }
}
