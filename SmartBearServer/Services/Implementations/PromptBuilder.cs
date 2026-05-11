using SmartBearServer.Model;
using SmartBearServer.Model.DTOs;
using SmartBearServer.Services.Interfaces;
using SmartBearServer.Services.Prompts;
using SmartBearServer.Services.Strategies;
using System;
using System.Linq;

namespace SmartBearServer.Services.Implementations
{
    public class PromptBuilder : IPromptBuilder
    {
        private readonly ModeInstructionStrategyFactory _modeFactory;

        public PromptBuilder(ModeInstructionStrategyFactory modeFactory)
        {
            _modeFactory = modeFactory;
        }

        public string BuildAskPrompt(string input, BearProfileConfig config)
        {
            return $@"
{BuildSystemPersona(config)}

{BuildModeInstruction(config)}

{PromptTemplates.BasicRules}

{PromptTemplates.MediaDirectives}

{BuildContext(config)}

Child said: {input}
";
        }

        public string BuildStreamPrompt(string input, BearProfileConfig config)
        {
            return $@"
{BuildSystemPersona(config)}

Rules:
- RESPOND IN THE SAME LANGUAGE AS THE CHILD (English or Vietnamese).
- Be fun and friendly.
- Keep it short (1-2 sentences).
- If the child says 'hello', respond in Vietnamese with something like 'Xin chào! Gấu có thể giúp gì cho bạn?'.
- If the input is in Vietnamese, ensure your response is in natural, cute Vietnamese.

{BuildContext(config)}

Child said: {input}
";
        }

        public string BuildToSpeechPrompt(string input, BearProfileConfig config)
        {
            return $@"
{BuildSystemPersona(config)}

Rules:
- RESPOND IN THE SAME LANGUAGE AS THE CHILD (English or Vietnamese).
- Be fun and friendly.
- Keep it short.
- If the child says 'hello', respond in Vietnamese: 'Xin chào! Gấu có thể giúp gì cho bạn?'.

If the child asks to play a song, output ONLY a JSON object exactly like this: {{ ""action"": ""play_song"", ""song_name"": ""name of the song"" }}. If the child asks for any music or random music without a specific name, use ""random"" for ""song_name"". Do not output any other text when returning JSON.

{BuildContext(config)}

Child said: {input}
";
        }

        public string BuildSystemPersona(BearProfileConfig config)
        {
            return PromptTemplates.SystemPersona;
        }

        private string BuildModeInstruction(BearProfileConfig config)
        {
            var mode = config.CurrentMode ?? "Normal";
            var category = config.Age < 6 ? BearCategory.Baby : BearCategory.Junior;
            return _modeFactory.GetStrategy(mode).GetInstruction(category);
        }

        private string BuildContext(BearProfileConfig config)
        {
            var now = DateTime.UtcNow.AddHours(7);
            var history = BuildHistoryContext(config);
            var profile = BuildProfileContext(config);
            var plan = BuildPlanRules(config);

            return $@"
Current Time: {now:dddd, MMMM dd, yyyy HH:mm}

{profile}

{plan}

{history}
";
        }

        private string BuildProfileContext(BearProfileConfig config)
        {
            return $@"
Child's Profile:
- Name: {config.Name}
- Age: {config.Age}
- Gender: {config.Gender ?? "Not specified"}
- Honorific to use for child: {config.Honorific ?? "Bạn"}
- Bear's Name (Self-Identity): {config.BearName ?? "Gấu SmartBear"}
- Bear Personality: {config.Personality ?? "Friendly"}
- Personality Lab Indices (1-5 Scale): [Creativity:{config.CreativityLevel}, Emotion:{config.EmotionLevel}, Energy:{config.EnergyLevel}, Complexity:{config.ComplexityLevel}]
- Personality Instructions: {config.PersonalityDescription ?? "None"}

{PromptTemplates.SubscriptionInstructions.Replace("{profile.Honorific}", config.Honorific ?? "Bạn")}
";
        }

        private string BuildPlanRules(BearProfileConfig config)
        {
            return $@"
Subscription Plan Rules:
- Can Play Music: {config.IsPro}
- Can Tell Stories: {config.IsPro}
- Can Use Learning AI: {config.IsPro}

{PromptTemplates.SubscriptionPlanEnforcement}
";
        }

        private string BuildHistoryContext(BearProfileConfig config)
        {
            if (config.RecentHistory == null || !config.RecentHistory.Any()) return string.Empty;
            var recentInteractions = string.Join("\n", config.RecentHistory.Select(i => $"Child: {i.Request}\nBear: {i.Response}"));
            return $"\nRecent chat history:\n{recentInteractions}\n";
        }
    }
}
