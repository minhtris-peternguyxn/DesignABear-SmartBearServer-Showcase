using System.Text.RegularExpressions;

namespace SmartBearServer.Infrastructure
{
    /// <summary>
    /// Helper class for standardized MAC address manipulation.
    /// </summary>
    public static class MACAddressHelper
    {
        /// <summary>
        /// Normalizes a MAC address or Serial Number by removing separators, 
        /// whitespace, and converting to uppercase.
        /// e.g., "10:20:BA:4A:E9:60" -> "1020BA4AE960"
        /// </summary>
        /// <param name="mac">The raw MAC address string.</param>
        /// <returns>A normalized uppercase alphanumeric string.</returns>
        public static string Normalize(string mac)
        {
            if (string.IsNullOrWhiteSpace(mac)) return string.Empty;
            
            return mac.Replace(":", "")
                      .Replace("-", "")
                      .Replace(" ", "")
                      .ToUpperInvariant()
                      .Trim();
        }
    }
}
