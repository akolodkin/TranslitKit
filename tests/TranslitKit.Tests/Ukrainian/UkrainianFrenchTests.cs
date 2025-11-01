namespace TranslitKit.Tests.Ukrainian;

/// <summary>
/// Tests for the Ukrainian French transliteration.
/// Adapts Ukrainian sounds to French phonetics.
/// </summary>
public class UkrainianFrenchTests
{
    private readonly UkrainianFrench _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("б", "b")]
    [InlineData("в", "v")]
    [InlineData("г", "h")]
    [InlineData("ґ", "g")]
    [InlineData("д", "d")]
    [InlineData("е", "e")]
    [InlineData("є", "ie")]
    [InlineData("ж", "j")]
    [InlineData("з", "z")]
    [InlineData("и", "y")]
    [InlineData("і", "i")]
    [InlineData("ї", "ï")]
    [InlineData("й", "y")]
    [InlineData("к", "k")]
    [InlineData("л", "l")]
    [InlineData("м", "m")]
    [InlineData("н", "n")]
    [InlineData("о", "o")]
    [InlineData("п", "p")]
    [InlineData("р", "r")]
    [InlineData("с", "s")]
    [InlineData("т", "t")]
    [InlineData("у", "ou")]
    [InlineData("ф", "f")]
    [InlineData("х", "kh")]
    [InlineData("ц", "ts")]
    [InlineData("ч", "tch")]
    [InlineData("ш", "ch")]
    [InlineData("щ", "chtch")]
    [InlineData("ю", "iou")]
    [InlineData("я", "ia")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Київ", "Kyïv")]
    [InlineData("Україна", "Oukraïna")]
    [InlineData("Харків", "Kharkiv")]
    [InlineData("Львів", "Lviv")]
    [InlineData("Дніпро", "Dnipro")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("КИЇВ", "KYÏV")]
    [InlineData("УКРАЇНА", "OUKRAÏNA")]
    public void AllUppercase_PreservesCase(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ApostrophesDeleted()
    {
        Assert.Equal("miaso", Translit.Convert("м'ясо", _table));
    }
}
