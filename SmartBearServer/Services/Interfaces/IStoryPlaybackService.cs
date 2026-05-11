using SmartBearServer.Model;

namespace SmartBearServer.Services.Interfaces
{
    public interface IStoryPlaybackService
    {
        Task<StoryPlaybackResponse> ResolveStoryAsync(Device device, string inputText);
    }
}