namespace cx.core.translit.Tests.Russian;

/// <summary>
/// Tests for the Russian Simple transliteration.
/// </summary>
public class RussianSimpleTests
{
    private readonly RussianSimple _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("ё", "e")]
    [InlineData("ж", "zh")]
    [InlineData("й", "j")]
    [InlineData("х", "h")]
    [InlineData("ц", "ts")]
    [InlineData("щ", "sch")]
    [InlineData("ъ", "'")]
    [InlineData("ь", "'")]
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
    public void HardSign_BecomesApostrophe()
    {
        Assert.Equal("ob'ekt", Translit.Convert("объект", _table));
    }

    [Fact]
    public void SoftSign_BecomesApostrophe()
    {
        Assert.Equal("den'", Translit.Convert("день", _table));
    }
}
