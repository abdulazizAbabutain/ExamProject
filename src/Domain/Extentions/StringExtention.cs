using System.Runtime.CompilerServices;

namespace Domain.Extentions
{
    public static class StringExtension
    {
        public static bool IsNotNullOrEmpty(this string value)
            => !string.IsNullOrEmpty(value);
    }
}
