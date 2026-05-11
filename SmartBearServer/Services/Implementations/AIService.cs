using SmartBearServer.Infrastructure;
using SmartBearServer.Model;
using SmartBearServer.Model.DTOs;
using SmartBearServer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.Json;

namespace SmartBearServer.Services
{
    public class AIService : IAIService
    {
        private readonly GeminiClient _gemini;
        private readonly IPromptBuilder _promptBuilder;
        private readonly ICacheService _cache;
        private readonly IEnumerable<IAIActionHandler> _actionHandlers;
        private readonly IEnumerable<ITTSService> _ttsServices;
        private readonly ILogger<AIService> _logger;

        public AIService(
            GeminiClient gemini,
            IPromptBuilder promptBuilder,
            ICacheService cache,
            IEnumerable<IAIActionHandler> actionHandlers,
            IEnumerable<ITTSService> ttsServices,
            ILogger<AIService> logger)
        {
            _gemini = gemini;
            _promptBuilder = promptBuilder;
            _cache = cache;
            _actionHandlers = actionHandlers;
            _ttsServices = ttsServices;
            _logger = logger;
        }

        public async Task<string> Process(string input, Device? device = null)
        {
            var config = device != null ? BearProfileConfig.FromDevice(device) : new BearProfileConfig();
            return await ProcessWithConfig(input, config);
        }

        public async Task<string> ProcessWithConfig(string input, BearProfileConfig config)
        {
            try
            {
                await InjectRecentHistory(config);
                var prompt = _promptBuilder.BuildAskPrompt(input, config);
                var response = await _gemini.Generate(prompt);

                var actionResult = await TryHandleAction(response, config);
                var finalResponse = actionResult?.FinalResponse ?? response;

                // Save to Redis for instant memory in the next turn
                await SaveToRecentHistory(config, input, finalResponse);

                return finalResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProcessWithConfig");
                return "Gấu đang gặp chút trục trặc, bé chờ gấu một lát nhé!";
            }
        }

        public async IAsyncEnumerable<string> StreamProcess(string input, Device? device = null)
        {
            var config = device != null ? BearProfileConfig.FromDevice(device) : new BearProfileConfig();
            await foreach (var chunk in StreamProcessWithConfig(input, config)) yield return chunk;
        }

        public async IAsyncEnumerable<string> StreamProcessWithConfig(string input, BearProfileConfig config)
        {
            var prompt = _promptBuilder.BuildStreamPrompt(input, config);
            await foreach (var chunk in _gemini.StreamGenerate(prompt))
            {
                yield return chunk;
            }
        }

        public async Task<AISpeechResult> ProcessToSpeech(string input, Device? device = null)
        {
            var config = device != null ? BearProfileConfig.FromDevice(device) : new BearProfileConfig();
            return await ProcessToSpeechWithConfig(input, config);
        }

        public async Task<AISpeechResult> ProcessToSpeechWithConfig(string input, BearProfileConfig config)
        {
            try
            {
                await InjectRecentHistory(config);
                var prompt = _promptBuilder.BuildToSpeechPrompt(input, config);
                var text = await _gemini.Generate(prompt);

                // 1. Try to handle as a structured action
                var actionResult = await TryHandleAction(text, config);
                string responseText = actionResult?.ResponseText ?? text;

                // Save to Redis for instant memory
                await SaveToRecentHistory(config, input, responseText);

                if (actionResult != null && actionResult.IsHandled)
                {
                    return new AISpeechResult
                    {
                        Text = responseText,
                        Audio = actionResult.AudioData ?? Array.Empty<byte>()
                    };
                }

                // 2. Normal text response -> TTS
                var audio = await GenerateTts(text, config);
                return new AISpeechResult { Text = text, Audio = audio };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProcessToSpeechWithConfig");
                var errorText = "Gấu đang bận một chút, bé thử lại sau nhé!";
                return new AISpeechResult { Text = errorText, Audio = await ProcessToSpeechFallback(errorText, "vi-VN") };
            }
        }

        public async Task<byte[]> ProcessToSpeechFallback(string text, string languageCode)
        {
            var tts = ResolveTts(Constants.TtsProviders.Gcp);
            var voiceId = languageCode.StartsWith("vi") ? Constants.Voices.DefaultVietnamese : Constants.Voices.DefaultEnglish;
            return await tts.GenerateAudio(text, voiceId, languageCode);
        }

        public async Task<(string Title, string Summary, string Category)> SummarizeSessionAsync(IEnumerable<InteractionHistory> interactions)
        {
            // Keeping this simple for now but using BuildHistoryContext style if needed
            if (interactions == null || !interactions.Any())
                return ("New Session", "No content yet.", "General");

            var chatLog = string.Join("\n", interactions.Select(i => $"Child: {i.Request}\nBear: {i.Response}"));
            var prompt = $@"
Analyze the following conversation between a child and their AI SmartBear. 
Generate a JSON response with 'title', 'summary', and 'category'.

CONVERSATION:
{chatLog}

JSON Response Format:
{{
  ""title"": ""..."",
  ""summary"": ""..."",
  ""category"": ""...""
}}
";
            try
            {
                var response = await _gemini.Generate(prompt);
                var json = ExtractJson(response);
                if (!string.IsNullOrEmpty(json))
                {
                    using var doc = JsonDocument.Parse(json);
                    return (
                        doc.RootElement.GetProperty("title").GetString() ?? "Chat Session",
                        doc.RootElement.GetProperty("summary").GetString() ?? "Ongoing conversation.",
                        doc.RootElement.GetProperty("category").GetString() ?? "General"
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to summarize session.");
            }

            return ("Chat Session", "Ongoing conversation.", "General");
        }

        private async Task<AIActionResult?> TryHandleAction(string text, BearProfileConfig config)
        {
            var json = ExtractJson(text);
            if (string.IsNullOrEmpty(json)) return null;

            try
            {
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("action", out var actionProp))
                {
                    var actionName = actionProp.GetString();
                    var handler = _actionHandlers.FirstOrDefault(h => h.ActionName == actionName);
                    if (handler != null)
                    {
                        return await handler.HandleAsync(json, config);
                    }
                }
            }
            catch (JsonException ex)
            {
                _logger.LogWarning(ex, "Failed to parse JSON from AI response: {Json}", json);
            }
            return null;
        }

        private async Task<byte[]> GenerateTts(string text, BearProfileConfig config)
        {
            var provider = config.PreferredTtsProvider ?? Constants.TtsProviders.Gcp;
            var voiceId = config.PreferredVoiceId ?? (provider == Constants.TtsProviders.Gcp ? Constants.Voices.DefaultVietnamese : "");
            var tts = ResolveTts(provider);
            return await tts.GenerateAudio(text, voiceId);
        }

        private ITTSService ResolveTts(string provider)
        {
            return _ttsServices.FirstOrDefault(s => s.Provider == provider) 
                ?? _ttsServices.First(s => s.Provider == Constants.TtsProviders.Gcp);
        }

        private string ExtractJson(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            // Improved regex to handle markdown code blocks better
            var match = Regex.Match(text, @"\{.*\}", RegexOptions.Singleline);
            return match.Success ? match.Value : string.Empty;
        }

        private async Task InjectRecentHistory(BearProfileConfig config)
        {
            if (string.IsNullOrEmpty(config.ProfileId)) return;
            try
            {
                var redisHistory = await _cache.GetRecentHistoryAsync<InteractionHistoryDto>(config.ProfileId);
                if (redisHistory != null && redisHistory.Any())
                {
                    // Merge Redis history with existing config history
                    var combined = redisHistory.Concat(config.RecentHistory)
                        .GroupBy(h => h.Request + h.Response)
                        .Select(g => g.First())
                        .OrderByDescending(h => h.Timestamp)
                        .Take(5)
                        .OrderBy(h => h.Timestamp)
                        .ToList();

                    config.RecentHistory = combined;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to inject recent history from Redis.");
            }
        }

        private async Task SaveToRecentHistory(BearProfileConfig config, string input, string response)
        {
            if (string.IsNullOrEmpty(config.ProfileId)) return;
            try
            {
                await _cache.PushRecentHistoryAsync(config.ProfileId, new InteractionHistoryDto
                {
                    Request = input,
                    Response = response,
                    Timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to save interaction to Redis history.");
            }
        }

        public class AISpeechResult
        {
            public string Text { get; set; } = string.Empty;
            public byte[] Audio { get; set; } = Array.Empty<byte>();
        }
    }
}
