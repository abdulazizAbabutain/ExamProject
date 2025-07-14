using Domain.Enums;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Extentions
{
    public static class TextNormalizer
    {

        private static readonly Dictionary<EntityLanguage, (int Start, int End)[]> LanguageRanges = new()
        {
            // Arabic
            [EntityLanguage.Arabic] = new[] {
                (0x0600, 0x06FF), // Arabic
                (0x0750, 0x077F), // Arabic Supplement
                (0x08A0, 0x08FF)  // Arabic Extended-A
            },
            [EntityLanguage.English] = new[] {
                (0x0041, 0x007A),// Basic Latin
                (0x00C0, 0x00FF),// Latin-1 Supplement
                (0x0100, 0x017F) // Latin Extended-A
            },
            [EntityLanguage.Russian] = new[] {
                (0x0400, 0x04FF), // Cyrillic
                (0x0500, 0x052F), // Cyrillic Supplement
                (0x2DE0, 0x2DFF), // Cyrillic Extended-A
                (0xA640, 0xA69F)  // Cyrillic Extended-B
            },
            // Chinese (Han)
            [EntityLanguage.Chinese] = new[] {
                (0x4E00, 0x9FFF), // CJK Unified Ideographs
                (0x3400, 0x4DBF), // CJK Unified Ideographs Extension A
                (0x20000, 0x2A6DF) // Extension B (requires surrogate handling)
            },
            // Hindi (Devanagari)
            [EntityLanguage.Hindi] = new[] {
                (0x0900, 0x097F) // Devanagari
            },
            // Japanese (Hiragana, Katakana, Kanji)
            [EntityLanguage.Japanese] = new[] {
                (0x3040, 0x309F), // Hiragana
                (0x30A0, 0x30FF), // Katakana
                (0x4E00, 0x9FFF)  // Kanji (shared with Chinese)
            }
        };



        public static string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var lang = DetectLanguageByUnicode(input);
            return lang switch
            {
                EntityLanguage.Arabic => NormalizeArabic(input),
                EntityLanguage.English => NormalizeEnglish(input),
                _ => BasicNormalize(input)
            };
        }

        private static string NormalizeEnglish(string input)
        {
            input = input.ToLowerInvariant();
            input = RemoveLatinDiacritics(input);
            input = Regex.Replace(input, @"[^\w\s]", "");
            input = Regex.Replace(input, @"\s+", " ").Trim();
            return input;
        }

        private static string NormalizeArabic(string input)
        {
            input = RemoveArabicDiacritics(input);
            input = input.Replace("أ", "ا")
                         .Replace("إ", "ا")
                         .Replace("آ", "ا")
                         .Replace("ى", "ي")
                         .Replace("ة", "ه")
                         .Replace("ؤ", "و")
                         .Replace("ئ", "ي")
                         .Replace("ء", "");
            input = Regex.Replace(input, @"[^\u0600-\u06FF\u0660-\u0669\u06F0-\u06F9\u0030-\u0039\s]", "");
            input = Regex.Replace(input, @"\s+", " ").Trim();
            return input;
        }


        private static string BasicNormalize(string input)
        {
            input = input.ToLowerInvariant();
            input = RemoveLatinDiacritics(input);
            input = Regex.Replace(input, @"[^\w\s]", "");
            input = Regex.Replace(input, @"\s+", " ").Trim();
            return input;
        }

        private static string RemoveLatinDiacritics(string input)
        {
            var normalized = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        private static string RemoveArabicDiacritics(string input)
        {
            return Regex.Replace(input, @"[\u064B-\u065F\u0610-\u061A\u06D6-\u06ED]", "");
        }

        public static EntityLanguage DetectLanguageByUnicode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return EntityLanguage.Unknown;

            var counts = new Dictionary<EntityLanguage, int>();
            ReadOnlySpan<char> span = input.AsSpan();

            for (int i = 0; i < span.Length; i++)
            {
                int code = span[i];
                foreach (var kvp in LanguageRanges)
                {
                    foreach (var (start, end) in kvp.Value)
                    {
                        if (code >= start && code <= end)
                        {
                            if (!counts.ContainsKey(kvp.Key))
                                counts[kvp.Key] = 0;
                            counts[kvp.Key]++;
                            break;
                        }
                    }
                }
            }

            if (counts.Count == 0)
                return EntityLanguage.Unknown;

            return counts.MaxBy(kv => kv.Value).Key;
        }
    }
}
