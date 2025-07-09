using Domain.Constants;
using System.Text.RegularExpressions;

namespace Domain.Extentions
{
    public static class StringExtension
    {
        public static bool IsNotNullOrEmpty(this string value)
            => !string.IsNullOrEmpty(value);

        public static bool IsHexColor(this string input) 
            => input.IsNotNullOrEmpty() && Regex.IsMatch(input, RegexPattern.MatchHexCode);

        public static string Slugify(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "icon";
            var invalid = Path.GetInvalidFileNameChars();
            var cleaned = new string(text.Where(c => !invalid.Contains(c)).ToArray());
            return cleaned
                .ToLowerInvariant()
                .Replace(" ", "-")
                .Trim();
        }
    }
}
