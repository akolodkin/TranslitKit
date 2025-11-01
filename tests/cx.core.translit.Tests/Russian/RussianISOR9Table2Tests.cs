namespace TranslitKit.Tests.Russian;

/// <summary>
/// Tests for the Russian ISO/R 9:1968 Table 2 transliteration.
/// </summary>
public class RussianISOR9Table2Tests
{
    private readonly RussianISOR9Table2 _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("ё", "jo")]
    [InlineData("й", "jj")]
    [InlineData("х", "kh")]
    [InlineData("ц", "c")]
    [InlineData("щ", "shh")]
    [InlineData("ъ", "″")]
    [InlineData("ь", "′")]
    [InlineData("э", "eh")]
    [InlineData("ю", "ju")]
    [InlineData("я", "ja")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Москва", "Moskva")]
    [InlineData("Россия", "Rossija")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void HardSign_BecomesDoubleQuote()
    {
        Assert.Equal("ob″ekt", Translit.Convert("объект", _table));
    }

    [Fact]
    public void SoftSign_BecomesPrime()
    {
        Assert.Equal("den′", Translit.Convert("день", _table));
    }
}
