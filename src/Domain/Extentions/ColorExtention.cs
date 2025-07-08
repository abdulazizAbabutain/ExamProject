using Domain.Enums;
using System.Drawing;

namespace Domain.Extentions;
/// <summary>
/// Provides color-related extension methods and utilities, such as categorizing colors by group or generating random hex colors.
/// </summary>
public static class ColorExtension
{
    /// <summary>
    /// Determines the <see cref="ColorCategory"/> of a given hexadecimal color string.
    /// </summary>
    /// <param name="hexColor">A string representing a color in hex format (e.g., <c>#FF5733</c>).</param>
    /// <returns>
    /// A <see cref="ColorCategory"/> that classifies the color (e.g., Red, Green, Blue, etc.).
    /// Includes logic for grayscale colors based on brightness and saturation.
    /// </returns>
    /// <summary>
    /// Determines the color category of a hexadecimal color string based on its brightness, saturation, and hue.
    /// </summary>
    /// <param name="hexColor">A hexadecimal color string (e.g., "#FF5733").</param>
    /// <returns>The <see cref="ColorCategory"/> that best represents the input color.</returns>
    /// <exception cref="ArgumentException">Thrown if the hex color string is invalid or cannot be parsed.</exception>
    public static ColorCategory GetColorGroup(this string hexColor)
    {
        Color color = ColorTranslator.FromHtml(hexColor);

        // Check for white, black, and gray using brightness and saturation
        float brightness = color.GetBrightness(); // 0 to 1
        float saturation = color.GetSaturation(); // 0 to 1

        if (saturation <= 0.05f)
        {
            if (brightness >= 0.9f)
                return ColorCategory.White;
            if (brightness <= 0.1f)
                return ColorCategory.Black;
            return ColorCategory.Gray;  
        }

        float hue = color.GetHue(); // 0-360 degrees
        if (hue < 20 || hue >= 340) return ColorCategory.Red;
        if (hue < 40) return ColorCategory.Orange;
        if (hue < 65) return ColorCategory.Yellow;
        if (hue < 170) return ColorCategory.Green;
        if (hue < 200) return ColorCategory.Cyan;
        if (hue < 260) return ColorCategory.Blue;
        if (hue < 300) return ColorCategory.Purple;
        
        return ColorCategory.Pink;
    }

    /// <summary>
    /// Generates a random hex color string in the format <c>#RRGGBB</c>, avoiding extremes (too dark or too bright).
    /// </summary>
    /// <summary>
    /// Generates a random hexadecimal color string with RGB components constrained to avoid colors that are too dark or too bright.
    /// </summary>
    /// <returns>A string representing a random color in hexadecimal format (e.g., "#A1B2C3").</returns>
    public static string GenerateRandomHexColor()
    {
        Random random = new Random();
        int r = random.Next(50, 200);
        int g = random.Next(50, 200);
        int b = random.Next(50, 200);
        return $"#{r:X2}{g:X2}{b:X2}";
    }
}
