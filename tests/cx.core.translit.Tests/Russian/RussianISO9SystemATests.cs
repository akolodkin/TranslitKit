namespace TranslitKit.Tests.Russian;

/// <summary>
/// Tests for the Russian ISO 9 System A transliteration.
/// </summary>
public class RussianISO9SystemATests
{
    private readonly RussianISO9SystemA _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("ё", "ë")]
    [InlineData("ж", "ž")]
    [InlineData("й", "j")]
    [InlineData("х", "h")]
    [InlineData("ц", "c")]
    [InlineData("ч", "č")]
    [InlineData("ш", "š")]
    [InlineData("щ", "ŝ")]
    [InlineData("ъ", "″")]
    [InlineData("ь", "′")]
    [InlineData("э", "è")]
    [InlineData("ю", "û")]
    [InlineData("я", "â")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Москва", "Moskva")]
    [InlineData("Россия", "Rossiâ")]
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
