using Domain.Enums;
using System.Drawing;

namespace Domain.Extentions
{
    public static class ColorExtension
    {
        public static ColorCategory GetColorGroup(this string hexColor)
        {
            Color color = ColorTranslator.FromHtml(hexColor);
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
