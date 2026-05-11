using SmartBearServer.Model;

namespace SmartBearServer.Services.Interfaces
{
    public interface IMediaService
    {
        Task<StoryPlaybackResponse> ResolveMediaAsync(Device device, string query, MediaType type);
        Task<bool> UploadMediaAsync(Stream stream, string fileName, MediaType type);
        Task<List<Song>> GetAllSongsAsync();
        Task<string> SpeakAsync(string text, string voiceId, string provider);
        Task<string> GetUploadUrlAsync(string fileName, MediaType type);
        Task<bool> ConfirmUploadAsync(string fileName, MediaType type, string? name = null, string? displayInfo = null, string? id = null);
    }
}
