using SmartBearServer.Model;
using SmartBearServer.Model.DTOs;

namespace SmartBearServer.Services.Interfaces
{
    public interface IPromptBuilder
    {
        string BuildAskPrompt(string input, BearProfileConfig config);
        string BuildStreamPrompt(string input, BearProfileConfig config);
        string BuildToSpeechPrompt(string input, BearProfileConfig config);
        string BuildSystemPersona(BearProfileConfig config);
    }
}
