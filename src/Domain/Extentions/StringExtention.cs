using Domain.Constants;
using Domain.Enums;
using System.Text.RegularExpressions;

namespace Domain.Extentions;

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

    public static string GetOriginalNameFromFile(this string fileName)
    {
        var nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
        var match = Regex.Match(nameWithoutExt, @"^(.*)-[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value : nameWithoutExt;
    }

    public static Guid GetGuidFromFileName(this string fileName)
    {
        var nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);

        var match = Regex.Match(nameWithoutExt,
            @"(?<name>.*)-(?<guid>[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12})$",
            RegexOptions.IgnoreCase);

        Guid.TryParse(match.Groups["guid"].Value, out var guid);
        return guid;
    }


    public static IconSource? ParseIconSourceFromPath(this string path)
    {
        var sourceStr = GetIconSourceFromPath(path);
        return Enum.TryParse<IconSource>(sourceStr, true, out var source) ? source : null;
    }

    public static string? GetIconSourceFromPath(this string path)
    {
        if (string.IsNullOrWhiteSpace(path)) return null;

        var segments = path.Trim('/').Split('/');
        // Expected format: ["icons", "predefined", "filename"]
        if (segments.Length >= 2 && segments[0].Equals("icons", StringComparison.OrdinalIgnoreCase))
        {
            return segments[1]; // returns "predefined" or "uploaded"
        }

        return null;
    }
}
