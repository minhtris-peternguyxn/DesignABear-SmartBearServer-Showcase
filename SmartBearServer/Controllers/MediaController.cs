using Microsoft.AspNetCore.Mvc;
using SmartBearServer.Infrastructure.Common;
using SmartBearServer.Model;
using SmartBearServer.Services.Interfaces;

namespace SmartBearServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : BaseController
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromQuery] MediaType type)
        {
            if (file == null || file.Length == 0)
                return HandleResult(Result.Failure(new Infrastructure.Common.Error("BadRequest", "No file uploaded.")));

            using var stream = file.OpenReadStream();
            var success = await _mediaService.UploadMediaAsync(stream, file.FileName, type);

            if (success)
                return HandleResult(Result<object>.Success(new { Message = "Upload successful", FileName = file.FileName }));

            return HandleResult(Result.Failure(new Infrastructure.Common.Error("ServerError", "Upload failed.")));
        }

        [HttpGet("songs")]
        public async Task<IActionResult> GetSongs()
        {
            var songs = await _mediaService.GetAllSongsAsync();
            return HandleResult(Result<object>.Success(songs));
        }
    }
}
