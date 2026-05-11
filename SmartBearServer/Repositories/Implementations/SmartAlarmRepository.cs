using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;

namespace SmartBearServer.Repositories.Implementations
{
    public class SmartAlarmRepository : ISmartAlarmRepository
    {
        private readonly AppDbContext _db;

        public SmartAlarmRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<SmartAlarm>> GetByUserAsync(Guid userId)
        {
            return await _db.SmartAlarms
                .Include(a => a.Device)
                .Where(a => a.Device.UserId == userId)
                .OrderBy(a => a.Hour)
                .ThenBy(a => a.Minute)
                .ToListAsync();
        }

        public async Task<SmartAlarm?> GetByIdForUserAsync(Guid userId, string alarmId)
        {
            return await _db.SmartAlarms
                .Include(a => a.Device)
                .FirstOrDefaultAsync(a => a.AlarmId == alarmId && a.Device.UserId == userId);
        }

        public async Task<bool> IsOwnedDeviceAsync(Guid userId, string deviceId)
        {
            return await _db.Devices.AnyAsync(d => d.DeviceId == deviceId && d.UserId == userId);
        }

        public async Task<SmartAlarm> AddAsync(SmartAlarm alarm)
        {
            _db.SmartAlarms.Add(alarm);
            await _db.SaveChangesAsync();
            return alarm;
        }

        public async Task UpdateAsync(SmartAlarm alarm)
        {
            _db.SmartAlarms.Update(alarm);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(SmartAlarm alarm)
        {
            _db.SmartAlarms.Remove(alarm);
            await _db.SaveChangesAsync();
        }

        public async Task<List<SmartAlarm>> GetDueAlarmsAsync(int hour, int minute)
        {
            return await _db.SmartAlarms
                .Include(a => a.Device)
                    .ThenInclude(d => d.Profile)
                .Include(a => a.Device)
                    .ThenInclude(d => d.ParentUser)
                .Where(a => a.IsEnabled && a.Hour == hour && a.Minute == minute)
                .ToListAsync();
        }
    }
}
