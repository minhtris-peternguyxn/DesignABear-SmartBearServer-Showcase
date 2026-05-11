using SmartBearServer.Model;

namespace SmartBearServer.Services.Interfaces
{
    public interface ILearningRecommendationPromptBuilder
    {
        string Build(ChildProfile profile);
    }
}
