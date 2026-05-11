using System.ComponentModel.DataAnnotations;

namespace SmartBearServer.Model
{
    public class SubscriptionPlan
    {
        public int Id { get; set; }
        public SubscriptionPlanType PlanType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CanPlayMusic { get; set; }
        public bool CanTellStoriesOnUserSpeech { get; set; }
        public bool CanUseLearningAI { get; set; }
        public decimal PriceMonthly { get; set; }
        public decimal Price => PriceMonthly; // Compatible with Mobile App
        public bool IsActive { get; set; }

        /// <summary>How many AI candies are granted daily for this plan.</summary>
        public int DailyCandyLimit { get; set; } = 10;
    }

    public enum SubscriptionPlanType
    {
        Basic = 1,
        Premium = 2,
        VIP = 3
    }
}
