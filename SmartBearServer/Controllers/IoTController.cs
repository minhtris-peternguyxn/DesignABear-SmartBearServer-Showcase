using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBearServer.Infrastructure.Common;
using Microsoft.AspNetCore.SignalR;
using SmartBearServer.Hubs;
using SmartBearServer.Model;
using SmartBearServer.Services;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using SmartBearServer.Services.Interfaces;

namespace SmartBearServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IoTController : BaseController
    {
        private readonly BearCommandProcessor _processor;
        private readonly IHubContext<LLMHub> _hubContext;
        private readonly IDeviceIdentityService _deviceIdentity;
        private readonly IMusicPlaybackService _musicPlaybackService;
        private readonly IMediaService _mediaService;
        private readonly IDeviceService _deviceService;

        public IoTController(BearCommandProcessor processor, IHubContext<LLMHub> hubContext, IDeviceIdentityService deviceIdentity, IMusicPlaybackService musicPlaybackService, IMediaService mediaService, IDeviceService deviceService)
        {
            _processor = processor;
            _hubContext = hubContext;
            _deviceIdentity = deviceIdentity;
            _musicPlaybackService = musicPlaybackService;
            _mediaService = mediaService;
            _deviceService = deviceService;
        }

        /// <summary>
        /// Process text-based interaction from the device.
        /// </summary>
        [HttpPost("speak")]
        public async Task<IActionResult> Speak([FromBody] IoTRequest req)
        {
            var authResult = await AuthorizeDeviceRequest(req.DeviceId);
            if (authResult != null)
            {
                return authResult;
            }

            var lang = Request.Headers["X-Language"].FirstOrDefault() ?? "en-US";
            await _hubContext.Clients.All.SendAsync("StatusUpdate", req.DeviceId, $"Processing text: {req.Text} (Lang: {lang})");
            
            var audio = await _processor.ProcessTextCommandAsync(req.DeviceId, req.Text, lang);
            
            await _hubContext.Clients.All.SendAsync("StatusUpdate", req.DeviceId, "Sent audio response");
            return File(audio, "audio/mpeg");
        }

        /// <summary>
        /// Process audio-based interaction from the device (Voice Command).
        /// </summary>
        [HttpPost("voice")]
        public async Task<IActionResult> Voice(IFormFile audioFile, [FromQuery] string deviceId, [FromQuery] string lang = "en-US")
        {
            var authResult = await AuthorizeDeviceRequest(deviceId);
            if (authResult != null)
            {
                return authResult;
            }

            var headerLang = Request.Headers["X-Language"].FirstOrDefault();
            if (!string.IsNullOrEmpty(headerLang)) lang = headerLang;

            if (audioFile == null || audioFile.Length == 0)
                return HandleResult(Result.Failure(new Infrastructure.Common.Error("BadRequest", "No audio file uploaded.")));

            await _hubContext.Clients.All.SendAsync("StatusUpdate", deviceId, $"Processing voice command... (Initial Lang: {lang})");

            using var stream = audioFile.OpenReadStream();
            var responseAudio = await _processor.ProcessVoiceCommandAsync(deviceId, stream, lang);

            await _hubContext.Clients.All.SendAsync("StatusUpdate", deviceId, "Voice processed successfully");

            return File(responseAudio, "audio/mpeg");
        }

        [HttpPost("music")]
        public async Task<IActionResult> PlayMusic()
        {
            var serial = Request.Headers["X-Serial-Number"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(serial))
            {
                return HandleResult(Result<object>.Failure(new Error("Unauthorized", "Missing X-Serial-Number header.")));
            }

            var deviceResult = await _deviceService.GetBySerialNumberAsync(serial);
            if (deviceResult.IsFailure || deviceResult.Value == null)
            {
                return HandleResult(Result<object>.Failure(new Error("Unauthorized", "Unknown device serial number.")));
            }

            var device = deviceResult.Value;

            var authResult = await AuthorizeDeviceRequest(device.DeviceId);
            if (authResult != null)
            {
                return authResult;
            }

            var request = await ParseMusicRequestAsync();
            var result = await _musicPlaybackService.ResolveMusicAsync(device, request);

            await _hubContext.Clients.All.SendAsync(
                "StatusUpdate",
                device.DeviceId,
                $"Music request processed. Action={result.Action}, Song={result.SongName}");

            return HandleResult(Result<object>.Success(result));
        }

        [HttpPost("story")]
        public async Task<IActionResult> PlayStory([FromBody] StoryRequest request)
        {
            var serial = Request.Headers["X-Serial-Number"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(serial))
            {
                return HandleResult(Result<object>.Failure(new Error("Unauthorized", "Missing X-Serial-Number header.")));
            }

            var deviceResult = await _deviceService.GetBySerialNumberAsync(serial);
            if (deviceResult.IsFailure || deviceResult.Value == null)
            {
                return HandleResult(Result<object>.Failure(new Error("Unauthorized", "Unknown device serial number.")));
            }

            var device = deviceResult.Value;

            var authResult = await AuthorizeDeviceRequest(device.DeviceId);
            if (authResult != null)
            {
                return authResult;
            }

            var text = request?.Text ?? string.Empty;
            var result = await _mediaService.ResolveMediaAsync(device, text, MediaType.Story);

            return HandleResult(Result<object>.Success(result));
        }

        private async Task<IActionResult> AuthorizeDeviceRequest(string deviceId)
        {
            var token = Request.Headers["X-Device-Token"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(token))
            {
                return HandleResult(Result.Failure(new Infrastructure.Common.Error("Unauthorized", "Missing X-Device-Token header.")));
            }

            var isValid = await _deviceIdentity.ValidateTokenAsync(deviceId, token);
            if (!isValid)
            {
                return HandleResult(Result.Failure(new Infrastructure.Common.Error("Unauthorized", "Invalid or expired device token.")));
            }

            return null;
        }
        private async Task<MusicRequest> ParseMusicRequestAsync()
        {
            using var reader = new StreamReader(Request.Body);
            var rawBody = await reader.ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(rawBody))
            {
                return new MusicRequest();
            }

            var contentType = Request.ContentType?.ToLowerInvariant() ?? string.Empty;

            if (contentType.Contains("application/json"))
            {
                try
                {
                    var parsed = JsonSerializer.Deserialize<MusicRequest>(rawBody);
                    return parsed ?? new MusicRequest();
                }
                catch
                {
                    return new MusicRequest { Text = rawBody };
                }
            }

            // Handle text/plain or other content types
            return new MusicRequest { Text = rawBody };
        }
    }
}
