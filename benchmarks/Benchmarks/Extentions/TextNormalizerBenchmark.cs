using BenchmarkDotNet.Attributes;
using Domain.Enums;
using Domain.Extentions;

namespace Benchmarks.Extentions;

[MemoryDiagnoser]
[ShortRunJob]
public class TextNormalizerBenchmark
{
    private string _arabicInput;
    private string _englishInput;
    private string _mixedInput;

    [GlobalSetup]
    public void Setup()
    {
        _arabicInput = "اللغة العربية جميلة جدًا وتتضمن تشكيلًا.";
        _englishInput = "Café déjà vu! It's a beautiful day.";
        _mixedInput = "Hello مرحبا Bonjour";
    }

    [Benchmark]
    public string NormalizeArabic() => TextNormalizer.Normalize(_arabicInput);

    [Benchmark]
    public string NormalizeEnglish() => TextNormalizer.Normalize(_englishInput);

    [Benchmark]
    public string NormalizeMixed() => TextNormalizer.Normalize(_mixedInput);

    [Benchmark]
    public EntityLanguage DetectLanguageArabic() => TextNormalizer.DetectLanguageByUnicode(_arabicInput);

    [Benchmark]
    public EntityLanguage DetectLanguageEnglish() => TextNormalizer.DetectLanguageByUnicode(_englishInput);

    [Benchmark]
    public EntityLanguage DetectLanguageMixed() => TextNormalizer.DetectLanguageByUnicode(_mixedInput);
}
