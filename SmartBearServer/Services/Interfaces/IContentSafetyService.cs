using SmartBearServer.Model;
using System;
using System.Threading.Tasks;

namespace SmartBearServer.Services.Interfaces
{
    public interface IContentSafetyService
    {
        Task<(bool IsSafe, string Message, string? Category)> EvaluateAsync(string input, ChildProfile profile, Guid? parentUserId = null);
        Task<(bool IsSafe, string Message, string? Category)> EvaluateResponseAsync(string llmOutput, ChildProfile profile, Guid? parentUserId = null);
        (bool IsSafe, string Message) Evaluate(string input, ChildProfile profile);
        (bool IsSafe, string Message) EvaluateResponse(string llmOutput, ChildProfile profile);
    }
}
