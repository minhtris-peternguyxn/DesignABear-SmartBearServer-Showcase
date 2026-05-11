namespace SmartBearServer.Infrastructure
{
    public static class Constants
    {
        public static class TtsProviders
        {
            public const string Gcp = "GCP";
            public const string ElevenLabs = "ElevenLabs";
        }

        public static class Languages
        {
            public const string Vietnamese = "vi-VN";
            public const string English = "en-US";
        }

        public static class Storage
        {
            public const string WwwRoot = "wwwroot";
            public const string AudioResponsesPath = "audio/responses";
        }

        public static class Voices
        {
            public const string DefaultVietnamese = "vi-VN-Neural2-A";
            public const string DefaultEnglish = "en-US-Neural2-C";
        }

        public static class Payment
        {
            public const int MinPayOsAmount = 2000;
            public const int OrderExpiryThresholdHours = 48;
        }

        public static class ConfigurationKeys
        {
            public const string PayOsSuccessUrl = "PayOS:Urls:SuccessUrl";
            public const string PayOsCancelUrl = "PayOS:Urls:CancelUrl";
            public const string InitialSmartCandies = "AppConfiguration:Subscription:InitialSmartCandies";
            public const string ProTrialDays = "AppConfiguration:Subscription:ProTrialDays";
        }
    }
}
