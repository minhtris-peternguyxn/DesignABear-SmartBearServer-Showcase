using SmartBearServer.Model.DTOs;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    public interface IAIActionHandler
    {
        string ActionName { get; }
        Task<AIActionResult> HandleAsync(string jsonPayload, BearProfileConfig config);
    }

    public class AIActionResult
    {
        public bool IsHandled { get; set; }
        public string? ResponseText { get; set; }
        public byte[]? AudioData { get; set; }
        public string? FinalResponse { get; set; }
    }
}
