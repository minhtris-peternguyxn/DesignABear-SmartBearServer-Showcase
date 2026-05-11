using SmartBearServer.Model;
using SmartBearServer.Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    public interface IAIService
    {
        Task<string> Process(string input, Device? device = null);
        Task<string> ProcessWithConfig(string input, BearProfileConfig config);
        
        IAsyncEnumerable<string> StreamProcess(string input, Device? device = null);
        IAsyncEnumerable<string> StreamProcessWithConfig(string input, BearProfileConfig config);
        
        Task<AIService.AISpeechResult> ProcessToSpeech(string input, Device? device = null);
        Task<AIService.AISpeechResult> ProcessToSpeechWithConfig(string input, BearProfileConfig config);
        Task<byte[]> ProcessToSpeechFallback(string text, string languageCode);
        Task<(string Title, string Summary, string Category)> SummarizeSessionAsync(IEnumerable<InteractionHistory> interactions);
    }
}
