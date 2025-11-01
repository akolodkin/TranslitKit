namespace cx.core.translit.Tests.Russian;

/// <summary>
/// Tests for the Russian International Passport 1997 transliteration.
/// </summary>
public class RussianInternationalPassport1997Tests
{
    private readonly RussianInternationalPassport1997 _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("ё", "e")]
    [InlineData("й", "y")]
    [InlineData("х", "kh")]
    [InlineData("ц", "ts")]
    [InlineData("щ", "shch")]
    [InlineData("ъ", "'")]
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
    [InlineData("Семье", "Sem'ye")]
    [InlineData("ружьё", "ruzh'ye")]
    public void SpecialCases_SoftSignE_TransliteratesCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void SoftSign_Deleted()
    {
        Assert.Equal("den", Translit.Convert("день", _table));
    }
}
