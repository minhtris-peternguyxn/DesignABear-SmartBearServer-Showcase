using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Model;
using SmartBearServer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Implementations
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly AppDbContext _db;
        public DeviceRepository(AppDbContext db) => _db = db;

        public async Task<List<Device>> GetAllAsync() => await _db.Devices.Include(d => d.Profile).Include(d => d.ParentUser).ThenInclude(u => u!.SubscriptionPlan).ToListAsync();
        
        public async Task<Device?> GetByIdAsync(string id) => await _db.Devices.Include(d => d.Profile).Include(d => d.ParentUser).ThenInclude(u => u!.SubscriptionPlan).FirstOrDefaultAsync(d => d.DeviceId == id);

        public async Task<Device?> GetByDeviceIdAsync(string deviceId)
        {
            var normalizedMac = NormalizeMac(deviceId);
            return await _db.Devices
                .Include(d => d.Profile)
                .Include(d => d.ParentUser)
                    .ThenInclude(u => u!.SubscriptionPlan)
                .FirstOrDefaultAsync(d => 
                    d.DeviceId == deviceId || 
                    d.DeviceId == normalizedMac || 
                    d.DeviceId.Replace(":", "").Replace("-", "").ToUpper() == normalizedMac);
        }

        public async Task<Device?> GetByIdSimpleAsync(string id)
        {
            var normalizedMac = NormalizeMac(id);
            return await _db.Devices.FirstOrDefaultAsync(d => 
                d.DeviceId == id || 
                d.DeviceId == normalizedMac || 
                d.DeviceId.Replace(":", "").Replace("-", "").ToUpper() == normalizedMac);
        }

        /// <summary>Resolves a device from its physical SerialNumber. Used by Python bridge session resolution.</summary>
        public async Task<Device?> GetBySerialNumberAsync(string serialNumber)
        {
            var normalizedMac = NormalizeMac(serialNumber);
            return await _db.Devices
                .Include(d => d.Profile)
                .Include(d => d.ParentUser)
                    .ThenInclude(u => u!.SubscriptionPlan)
                .FirstOrDefaultAsync(d => 
                    d.SerialNumber == serialNumber || 
                    d.DeviceId == serialNumber || 
                    d.DeviceId == normalizedMac ||
                    d.DeviceId.Replace(":", "").Replace("-", "").ToUpper() == normalizedMac);
        }

        public async Task<Device?> GetByPairingCodeAsync(string code)
            => await _db.Devices
                .Include(d => d.Profile)
                .FirstOrDefaultAsync(d => d.PairingCode == code && d.PairingCodeExpiresAt > System.DateTime.UtcNow);

        public async Task<Device> AddAsync(Device device)
        {
            _db.Devices.Add(device);
            await _db.SaveChangesAsync();
            return device;
        }

        public async Task UpdateAsync(Device device)
        {
            _db.Devices.Update(device);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var device = await GetByIdAsync(id);
            if (device != null)
            {
                _db.Devices.Remove(device);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Device>> GetDevicesWithDetailsByUserAsync(Guid userId)
        {
            return await _db.Devices
                .Include(d => d.ParentUser)
                    .ThenInclude(u => u!.SubscriptionPlan)
                .Include(d => d.Profile)
                .Where(d => d.UserId == userId)
                .ToListAsync();
        }

        public async Task<Device?> GetByProfileIdWithProfileAsync(string profileId, Guid userId)
        {
            return await _db.Devices
                .Include(d => d.Profile)
                .FirstOrDefaultAsync(d => d.ProfileId == profileId && d.UserId == userId);
        }

        public async Task<Device?> GetByProfileIdAsync(string profileId)
        {
            return await _db.Devices.FirstOrDefaultAsync(d => d.ProfileId == profileId);
        }

        public async Task<bool> ExistsForUserAsync(Guid userId, string profileId)
        {
            return await _db.Devices.AnyAsync(d => d.UserId == userId && d.ProfileId == profileId);
        }

        private static string NormalizeMac(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            return input.Replace(":", "").Replace("-", "").Replace(" ", "").ToUpper();
        }
    }
}
