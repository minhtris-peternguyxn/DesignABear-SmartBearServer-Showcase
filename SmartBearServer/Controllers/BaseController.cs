using Microsoft.AspNetCore.Mvc;
using SmartBearServer.Infrastructure.Common;

namespace SmartBearServer.Controllers
{
    /// <summary>
    /// Base controller providing shared functionality for mapping Result objects to HTTP responses.
    /// </summary>
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Maps a <see cref="Result{T}"/> to an <see cref="IActionResult"/>.
        /// </summary>
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                // Flutter expects the raw data (List or Object) directly
                return Ok(result.Value);
            }

            if (result.Error == null) return BadRequest(result);

            if (result.Error.Code.Contains("NotFound") || result.Error.Code.Contains("Not Found"))
            {
                return NotFound(result);
            }

            if (result.Error.Code.Contains("Unauthorized"))
            {
                return Unauthorized(result);
            }

            if (result.Error.Code.Contains("Forbidden") || result.Error.Code.Contains("AccessDenied"))
            {
                return StatusCode(403, result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Maps a <see cref="Result"/> to an <see cref="IActionResult"/>.
        /// </summary>
        protected IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
            {
                return Ok(new { success = true });
            }

            if (result.Error == null) return BadRequest(result);

            if (result.Error.Code.Contains("NotFound") || result.Error.Code.Contains("Not Found"))
            {
                return NotFound(result);
            }

            return BadRequest(result);
        }
    }
}
