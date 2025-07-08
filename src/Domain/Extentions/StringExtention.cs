using Domain.Constants;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Domain.Extentions
{
    public static class StringExtension
    {
        public static bool IsNotNullOrEmpty(this string value)
            => !string.IsNullOrEmpty(value);

        public static bool IsHexColor(this string input) 
            => input.IsNotNullOrEmpty() && Regex.IsMatch(input, RegexPattern.MatchHexCode);
    }
}
