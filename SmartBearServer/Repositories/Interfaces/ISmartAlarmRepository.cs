using SmartBearServer.Model;

namespace SmartBearServer.Repositories.Interfaces
{
    public interface ISmartAlarmRepository
    {
        Task<List<SmartAlarm>> GetByUserAsync(Guid userId);
        Task<SmartAlarm?> GetByIdForUserAsync(Guid userId, string alarmId);
        Task<bool> IsOwnedDeviceAsync(Guid userId, string deviceId);
        Task<SmartAlarm> AddAsync(SmartAlarm alarm);
        Task UpdateAsync(SmartAlarm alarm);
        Task DeleteAsync(SmartAlarm alarm);
        Task<List<SmartAlarm>> GetDueAlarmsAsync(int hour, int minute);
    }
}
