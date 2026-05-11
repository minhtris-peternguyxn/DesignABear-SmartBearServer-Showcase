using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartBearServer.Infrastructure.Common
{
    public static class TextHelper
    {
        /// <summary>
        /// Normalizes Vietnamese text to base ASCII and replaces spaces with dashes/removes special characters.
        /// Useful for both URL generation and robust keyword matching.
        /// </summary>
        public static string Normalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            var lower = text.Trim().ToLowerInvariant();
            var formD = lower.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in formD)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            var noDiacritics = sb.ToString().Normalize(NormalizationForm.FormC).Replace('đ', 'd');
            
            // Remove all special characters, keep only a-z, 0-9 and spaces
            noDiacritics = Regex.Replace(noDiacritics, @"[^a-z0-9\s]", "");
            
            // Collapse multiple spaces into one
            noDiacritics = Regex.Replace(noDiacritics, @"\s+", " ");
            
            return noDiacritics.Trim();
        }
    }
}
