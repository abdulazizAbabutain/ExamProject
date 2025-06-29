namespace Application.Commons.Extensions
{
    public static class StringExtension
    {
        public static string FirstCharToLower(this string input)
        {
            if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
                return input;

            return char.ToLower(input[0]) + input.Substring(1);
        }


        public static string ToReadableSize(double bytes)
        {
            if (bytes < 1024)
                return $"{bytes:F0} B";

            double kb = bytes / 1024;
            if (kb < 1024)
                return $"{kb:F1} KB";

            double mb = kb / 1024;
            if (mb < 1024)
                return $"{mb:F1} MB";

            double gb = mb / 1024;
            return $"{gb:F2} GB";
        }
    }
}
