using SmartBearServer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    public interface IVoiceService
    {
        /// <summary>
        /// Returns voice list for mobile app (formatted as anonymous objects)
        /// </summary>
        Task<List<object>> GetVoiceListAsync();

        /// <summary>
        /// Returns full DemoVoice entities for admin CRUD
        /// </summary>
        Task<List<DemoVoice>> GetAllVoicesAsync();

        Task<DemoVoice> AddVoiceAsync(DemoVoice voice);
        Task<bool> UpdateVoiceAsync(string id, DemoVoice updated);
        Task<bool> DeleteVoiceAsync(string id);

        Task<bool> UpdateVoicePreferenceAsync(Guid userId, string provider, string voiceId);
        Task<bool> UpdateDeviceVoiceConfigAsync(string deviceId, float speed, float volume);
    }
}
