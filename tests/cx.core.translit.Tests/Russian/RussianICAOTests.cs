namespace cx.core.translit.Tests.Russian;

/// <summary>
/// Tests for the Russian ICAO transliteration.
/// </summary>
public class RussianICAOTests
{
    private readonly RussianICAO _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("ё", "e")]
    [InlineData("й", "i")]
    [InlineData("х", "kh")]
    [InlineData("ц", "ts")]
    [InlineData("щ", "shch")]
    [InlineData("ъ", "ie")]
    [InlineData("ю", "iu")]
    [InlineData("я", "ia")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Москва", "Moskva")]
    [InlineData("Россия", "Rossiia")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void HardSign_BecomesIE()
    {
        Assert.Equal("obieekt", Translit.Convert("объект", _table));
    }

    [Fact]
    public void SoftSign_Deleted()
    {
        Assert.Equal("den", Translit.Convert("день", _table));
    }
}
