using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Model.DTOs;
using SmartBearServer.Services.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartBearServer.Controllers
{
    /// <summary>
    /// Handles zero-input device pairing and ownership claims.
    /// This controller delegates all business logic to <see cref="IPairingService"/>.
    /// </summary>
    [Route("api/pairing")]
    public class PairingController : BaseController
    {
        private readonly IPairingService _pairingService;

        public PairingController(IPairingService pairingService)
        {
            _pairingService = pairingService;
        }

        /// <summary>
        /// Called by the ESP32 bear device after WiFi connects to request a pairing code.
        /// </summary>
        [HttpPost("request")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestPairingCode()
        {
            var mac = Request.Headers["Device-Id"].ToString();
            Console.WriteLine($"[PairingController] Received request with MAC: '{mac}'");
            var result = await _pairingService.RequestPairingCodeAsync(mac);
            
            if (result.IsSuccess)
            {
                return HandleResult(Result<object>.Success(new { code = result.Value }));
            }
            return HandleResult(result);
        }

        /// <summary>
        /// Triggered by the Mobile App to request an OTP code read aloud by the bear.
        /// </summary>
        [HttpPost("request_otp")]
        [Authorize]
        public async Task<IActionResult> RequestOtpByMobileApp([FromBody] RequestOtpParams req)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result.Failure(new Error("Unauthorized", "User identity not found in token.")));

            var result = await _pairingService.RequestOtpAsync(req.SerialNumber, userId.Value);
            if (result.IsSuccess)
            {
                return HandleResult(Result<object>.Success(new { code = result.Value }));
            }
            return HandleResult(result);
        }

        /// <summary>
        /// Claims ownership of a device using the pairing code heard from the bear.
        /// </summary>
        [HttpPost("claim")]
        [Authorize]
        public async Task<IActionResult> ClaimDevice([FromBody] ClaimDeviceRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return HandleResult(Result.Failure(new Error("Unauthorized", "User identity not found in token.")));

            var result = await _pairingService.ClaimDeviceAsync(userId.Value, request.Code, request.Nickname, request.ChildName);
            
            if (result.IsSuccess)
            {
                var device = result.Value;
                return Ok(new
                {
                    Message = "Ghép nối thành công!",
                    DeviceId = device.DeviceId,
                    SerialNumber = device.SerialNumber,
                    Nickname = device.Nickname,
                    Status = device.Status
                });
            }
            return HandleResult(result);
        }

        private System.Guid? GetCurrentUserId()
        {
            var sub = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
            return System.Guid.TryParse(sub, out var guid) ? guid : null;
        }

        public class RequestOtpParams
        {
            public string SerialNumber { get; set; }
        }
    }
}
