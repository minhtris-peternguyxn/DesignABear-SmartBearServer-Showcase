using SmartBearServer.Model;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    public interface ISpeechService
    {
        Task<RecognitionResult> RecognizeSpeechAsync(byte[] audioBytes, string languageCode = "vi-VN");
    }
}
