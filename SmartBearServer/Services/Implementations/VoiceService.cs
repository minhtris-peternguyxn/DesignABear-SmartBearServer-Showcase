using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SmartBearServer.Data;
using SmartBearServer.Hubs;
using SmartBearServer.Model;
using SmartBearServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Implementations
{
    public class VoiceService : IVoiceService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<LLMHub> _hubContext;

        public VoiceService(AppDbContext context, IHubContext<LLMHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<List<object>> GetVoiceListAsync()
        {
            var voices = await _context.DemoVoices
                .OrderBy(v => v.IsPremium)
                .ThenBy(v => v.Provider)
                .ThenBy(v => v.Name)
                .Select(v => (object)new
                {
                    provider = v.Provider,
                    id = v.VoiceId,
                    name = v.Name,
                    description = v.Description ?? "",
                    isPremium = v.IsPremium
                })
                .ToListAsync();

            return voices;
        }

        public async Task<List<DemoVoice>> GetAllVoicesAsync()
        {
            return await _context.DemoVoices
                .OrderBy(v => v.IsPremium)
                .ThenBy(v => v.Provider)
                .ThenBy(v => v.Name)
                .ToListAsync();
        }

        public async Task<DemoVoice> AddVoiceAsync(DemoVoice voice)
        {
            voice.Id = Guid.NewGuid().ToString();
            voice.CreatedAt = DateTime.UtcNow;
            _context.DemoVoices.Add(voice);
            await _context.SaveChangesAsync();
            return voice;
        }

        public async Task<bool> UpdateVoiceAsync(string id, DemoVoice updated)
        {
            var voice = await _context.DemoVoices.FindAsync(id);
            if (voice == null) return false;

            voice.VoiceId = updated.VoiceId;
            voice.Name = updated.Name;
            voice.Provider = updated.Provider;
            voice.IsPremium = updated.IsPremium;
            voice.Description = updated.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVoiceAsync(string id)
        {
            var voice = await _context.DemoVoices.FindAsync(id);
            if (voice == null) return false;

            _context.DemoVoices.Remove(voice);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateVoicePreferenceAsync(Guid userId, string provider, string voiceId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return false;

            if (provider == "ElevenLabs" && !user.IsPro)
            {
                return false;
            }

            user.PreferredTtsProvider = provider;
            user.PreferredVoiceId = voiceId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateDeviceVoiceConfigAsync(string deviceId, float speed, float volume)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.DeviceId == deviceId);
            if (device == null) return false;

            device.PreferredSpeed = speed;
            device.PreferredVolume = volume;

            await _context.SaveChangesAsync();

            // Broadcast real-time update to the Python Bridge if connected
            var connectionId = LLMHub.GetConnectionIdBySerial(device.SerialNumber);
            if (!string.IsNullOrEmpty(connectionId))
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("UpdateBearConfig", new
                {
                    speed = speed,
                    volume = volume,
                    target_mac = device.SerialNumber
                });
            }

            return true;
        }
    }
}
