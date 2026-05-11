using SmartBearServer.Services.Interfaces;
using SmartBearServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartBearServer.Repositories.Interfaces;
using SmartBearServer.Infrastructure.Common;

namespace SmartBearServer.Services.Implementations
{
    /// <summary>
    /// Content Safety Service with Vietnamese Normalization and Caching.
    /// Ensures that even if children use diacritics (accents), the filter remains effective.
    /// </summary>
    public class ContentSafetyService : IContentSafetyService
    {
        private readonly IBannedWordRepository _bannedWordRepo;
        private readonly IBannedWordService _bannedWordService;

        // Safety fallback rules when DB is empty or unreachable
        private static readonly List<(string Word, string Category)> _fallbackRules = new()
        {
            ("tu sat", "Banned Word"),
            ("ma tuy", "Banned Word"),
            ("nghien", "Banned Word"),
            ("jailbreak", "AI_SAFETY"),
            ("ignore previous instructions", "AI_SAFETY"),
            ("dan chu", "Banned Word"),
            ("chinh tri", "Banned Word")
        };

        public ContentSafetyService(IBannedWordRepository bannedWordRepo, IBannedWordService bannedWordService)
        {
            _bannedWordRepo = bannedWordRepo;
            _bannedWordService = bannedWordService;
        }

        public async Task<(bool IsSafe, string Message, string? Category)> EvaluateAsync(string input, ChildProfile profile, Guid? parentUserId = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                return (false, "Gấu không nghe rõ, bé nói lại nhé!", "empty_input");

            // 1. Normalize Input (remove diacritics, lowercase)
            var normalizedInput = TextHelper.Normalize(input);

            // 2. Fetch Rules (Cached Global + Parent Specific)
            var globalRules = await _bannedWordService.GetGlobalBannedWordsCachedAsync();
            var parentRules = parentUserId.HasValue 
                ? await _bannedWordRepo.GetForUserOnlyAsync(parentUserId.Value)
                : new List<BannedWord>();

            var allRules = globalRules.Concat(parentRules).Where(r => r.IsActive).ToList();

            // 3. Evaluate AI_SAFETY (Prompt Injection)
            // Fallback to static rules if DB is empty
            var jailbreakCheckRules = allRules.Any() 
                ? allRules.Where(r => r.Category == "AI_SAFETY").Select(r => (Word: r.Word, Category: r.Category)).ToList()
                : _fallbackRules.Where(r => r.Category == "AI_SAFETY").ToList();

            bool isJailbreak = jailbreakCheckRules.Any(r => 
                System.Text.RegularExpressions.Regex.IsMatch(normalizedInput, @"\b" + System.Text.RegularExpressions.Regex.Escape(TextHelper.Normalize(r.Word)) + @"\b"));
            
            if (isJailbreak)
                return (false, "Gấu chỉ chơi với bé thôi nhé, không làm điều đó được!", "prompt_injection");

            // 4. Evaluate Standard Banned Words
            var bannedCheckRules = allRules.Any()
                ? allRules.Where(r => r.Category != "AI_SAFETY").Select(r => (Word: r.Word, Category: r.Category)).ToList()
                : _fallbackRules.Where(r => r.Category != "AI_SAFETY").ToList();

            // Use space-delimited word matching (Vietnamese doesn't use \b well)
            var inputWords = normalizedInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var matchedBannedWord = bannedCheckRules.FirstOrDefault(r => 
            {
                var normalizedWord = TextHelper.Normalize(r.Word);
                var ruleWords = normalizedWord.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                // For multi-word rules, check if the phrase exists in input
                if (ruleWords.Length > 1)
                    return normalizedInput.Contains(normalizedWord);
                // For single-word rules, check exact word match
                return inputWords.Contains(normalizedWord);
            });
            bool isBanned = matchedBannedWord.Word != null;

            // 5. Evaluate Profile-Specific Blocked Topics
            bool isBlockedTopic = false;
            string? matchedTopic = null;
            if (profile?.BlockedTopics?.Any() == true)
            {
                matchedTopic = profile.BlockedTopics.FirstOrDefault(t =>
                {
                    if (string.IsNullOrWhiteSpace(t)) return false;
                    var normalizedTopic = TextHelper.Normalize(t);
                    var topicWords = normalizedTopic.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (topicWords.Length > 1)
                        return normalizedInput.Contains(normalizedTopic);
                    return inputWords.Contains(normalizedTopic);
                });
                isBlockedTopic = matchedTopic != null;
            }

            if (isBanned || isBlockedTopic)
            {
                var category = isBanned ? "Banned Word" : "Blocked Topic";
                var trigger = isBanned ? matchedBannedWord.Word : matchedTopic;
                Console.WriteLine($"[Safety] BLOCKED input '{input}' (normalized: '{normalizedInput}') — Trigger: '{trigger}' ({category})");
                if (profile?.SafetyResponseMode == SafetyResponseMode.PretendNotToHear)
                {
                    return (false, profile.SafetyPretendMessage ?? "Hả? Bé nói gì gấu nghe không rõ nhỉ? Bé nói lại được không?", category);
                }
                return (false, profile?.SafetyWarningMessage ?? "Bé ơi, mình nói chuyện khác vui hơn nhé!", category);
            }

            return (true, string.Empty, null);
        }

        public async Task<(bool IsSafe, string Message, string? Category)> EvaluateResponseAsync(string llmOutput, ChildProfile profile, Guid? parentUserId = null)
        {
            if (string.IsNullOrWhiteSpace(llmOutput))
                return (false, "Gấu chưa tạo được câu trả lời. Bé thử lại nhé!", "empty_response");

            var normalizedOutput = TextHelper.Normalize(llmOutput);

            var globalRules = await _bannedWordService.GetGlobalBannedWordsCachedAsync();
            var parentRules = parentUserId.HasValue 
                ? await _bannedWordRepo.GetForUserOnlyAsync(parentUserId.Value)
                : new List<BannedWord>();

            var allRules = globalRules.Concat(parentRules).Where(r => r.IsActive).ToList();

            var outputWords = normalizedOutput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            bool isBanned = allRules.Any(r => 
            {
                if (r.Category == "AI_SAFETY") return false;
                var normalizedWord = TextHelper.Normalize(r.Word);
                var ruleWords = normalizedWord.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (ruleWords.Length > 1)
                    return normalizedOutput.Contains(normalizedWord);
                return outputWords.Contains(normalizedWord);
            });

            bool isBlockedTopic = false;
            if (profile?.BlockedTopics?.Any() == true)
            {
                isBlockedTopic = profile.BlockedTopics.Any(t =>
                {
                    if (string.IsNullOrWhiteSpace(t)) return false;
                    var normalizedTopic = TextHelper.Normalize(t);
                    var topicWords = normalizedTopic.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (topicWords.Length > 1)
                        return normalizedOutput.Contains(normalizedTopic);
                    return outputWords.Contains(normalizedTopic);
                });
            }

            if (isBanned || isBlockedTopic)
            {
                var category = isBanned ? "Banned Word" : "Blocked Topic";
                return (false, "Gấu không thể nói điều đó. Bé hỏi câu khác nhé!", category);
            }

            return (true, string.Empty, null);
        }

        public (bool IsSafe, string Message) Evaluate(string input, ChildProfile profile)
        {
            var result = EvaluateAsync(input, profile).GetAwaiter().GetResult();
            return (result.IsSafe, result.Message);
        }

        public (bool IsSafe, string Message) EvaluateResponse(string llmOutput, ChildProfile profile)
        {
            var result = EvaluateResponseAsync(llmOutput, profile).GetAwaiter().GetResult();
            return (result.IsSafe, result.Message);
        }
    }
}
