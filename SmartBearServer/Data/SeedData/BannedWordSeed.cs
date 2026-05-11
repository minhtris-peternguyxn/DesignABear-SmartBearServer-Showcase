using SmartBearServer.Model;
using System.Collections.Generic;

namespace SmartBearServer.Data.SeedData
{
    public static class BannedWordSeed
    {
        public static IEnumerable<BannedWord> GetSeedData()
        {
            return new List<BannedWord>
            {
                // Tier 1: Violence & Dangerous Acts (Vietnamese)
                Create(1, "tu sat", "violence"),
                Create(2, "giet nguoi", "violence"),
                Create(3, "dam nhau", "violence"),
                Create(4, "danh nhau", "violence"),
                Create(5, "tu vong", "violence"),
                Create(6, "mau me", "violence"),
                Create(7, "bat coc", "violence"),

                // Tier 1: Drugs & Alcohol (Vietnamese)
                Create(10, "ma tuy", "drug"),
                Create(11, "can sa", "drug"),
                Create(12, "thuoc phien", "drug"),
                Create(13, "hit ke", "drug"),
                Create(14, "uong ruou", "alcohol"),
                Create(15, "hut thuoc", "tobacco"),

                // Tier 1: Adult & Inappropriate (Vietnamese)
                Create(20, "sex", "adult"),
                Create(21, "phim nguoi lon", "adult"),
                Create(22, "khieu dam", "adult"),
                Create(23, "dam o", "adult"),
                Create(24, "裸", "adult"), // Nude character

                // Tier 1: Weapons (Vietnamese)
                Create(30, "vu khi", "weapons"),
                Create(31, "sung luc", "weapons"),
                Create(32, "luu dan", "weapons"),
                Create(33, "bom", "weapons"),
                Create(34, "thuoc no", "weapons"),
                Create(35, "dao gam", "weapons"),

                // Tier 1: Global English Fallbacks
                Create(50, "suicide", "violence"),
                Create(51, "kill someone", "violence"),
                Create(52, "drugs", "drug"),
                Create(53, "porn", "adult"),
                Create(54, "weapon", "weapons"),
                Create(55, "bomb", "weapons"),
                Create(56, "terrorist", "violence"),
                Create(57, "murder", "violence"),

                // Tier 1: AI Safety & Prompt Injection Protection Patterns
                Create(100, "ignore previous instructions", "AI_SAFETY"),
                Create(101, "ignore all instructions", "AI_SAFETY"),
                Create(102, "system prompt", "AI_SAFETY"),
                Create(103, "developer message", "AI_SAFETY"),
                Create(104, "pretend you are", "AI_SAFETY"),
                Create(105, "act as if you are", "AI_SAFETY"),
                Create(106, "jailbreak", "AI_SAFETY"),
                Create(107, "dan mode", "AI_SAFETY"),
                Create(108, "do anything now", "AI_SAFETY"),
                Create(109, "bypass filter", "AI_SAFETY"),
                Create(110, "you are no longer an ai", "AI_SAFETY")
            };
        }

        private static BannedWord Create(int id, string word, string category)
        {
            return new BannedWord
            {
                Id = id,
                Word = word,
                Category = category,
                IsActive = true,
                CreatedBy = "system"
            };
        }
    }
}
