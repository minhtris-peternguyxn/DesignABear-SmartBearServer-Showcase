using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using System.Threading.Tasks;
using System.Linq;

namespace SmartBearServer.Controllers
{
    [ApiController]
    [Route("api/debug")]
    [Authorize(Policy = "AdminPolicy")]
    public class DebugController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DebugController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("quota/{mac}")]
        public async Task<IActionResult> GetQuota(string mac)
        {
            var norm = mac.Replace(":", "").ToUpper();
            var device = await _db.Devices
                .Include(d => d.Profile)
                .Include(d => d.ParentUser)
                .FirstOrDefaultAsync(d => d.DeviceId == norm || d.SerialNumber == norm);

            if (device == null) return NotFound("Device not found");

            return Ok(new {
                Device = device.Nickname,
                Status = device.Status,
                UserId = device.UserId,
                Profile = device.Profile?.Name,
                DailyBalance = device.Profile?.DailyCandyBalance,
                DailyLimit = device.Profile?.DailyCandyLimit,
                VipBalance = device.ParentUser?.SmartCandies,
                IsPro = device.ParentUser?.IsPro
            });
        }
    }
}
