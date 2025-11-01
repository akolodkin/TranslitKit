namespace TranslitKit.Tests.Russian;

/// <summary>
/// Tests for the Russian International Passport 1997 Reduced transliteration.
/// </summary>
public class RussianInternationalPassport1997ReducedTests
{
    private readonly RussianInternationalPassport1997Reduced _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("й", "y")]
    [InlineData("ю", "yu")]
    [InlineData("я", "ya")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Москва", "Moskva")]
    [InlineData("Россия", "Rossiya")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("семье", "sem'ye")]
    [InlineData("белый", "bely")]
    [InlineData("Белый", "Bely")]
    [InlineData("синий", "siny")]
    [InlineData("Синий", "Siny")]
    public void SpecialCases_CompressionRules_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }
}
