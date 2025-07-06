using Domain.Enums;
using System.Drawing;

namespace Domain.Extentions
{
    public static class ColorExtension
    {
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
                return ColorCategory.Gray; // Optional: add if you support gray separately
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

        public static string GenerateRandomHexColor()
        {
            Random random = new Random();
            int r = random.Next(50, 200);
            int g = random.Next(50, 200);
            int b = random.Next(50, 200);
            return $"#{r:X2}{g:X2}{b:X2}";
        }
    }
}
