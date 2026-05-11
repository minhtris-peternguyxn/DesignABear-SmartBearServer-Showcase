using System;

namespace SmartBearServer.Model
{
    public class LearningRecommendationResponse
    {
        public string ProfileId { get; set; }
        public string ChildName { get; set; }
        public string Recommendation { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}
