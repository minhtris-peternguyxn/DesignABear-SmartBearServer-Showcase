using SmartBearServer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Repositories.Interfaces
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetAllAsync();
        Task<Device?> GetByIdAsync(string id);
        Task<Device?> GetByDeviceIdAsync(string deviceId);
        Task<Device?> GetByIdSimpleAsync(string id);
        Task<Device?> GetBySerialNumberAsync(string serialNumber);
        Task<Device?> GetByPairingCodeAsync(string code);
        Task<Device> AddAsync(Device device);
        Task UpdateAsync(Device device);
        Task DeleteAsync(string id);
        

        Task<List<Device>> GetDevicesWithDetailsByUserAsync(System.Guid userId);
        Task<Device?> GetByProfileIdWithProfileAsync(string profileId, System.Guid userId);
        Task<Device?> GetByProfileIdAsync(string profileId);
        Task<bool> ExistsForUserAsync(System.Guid userId, string profileId);
    }
}
