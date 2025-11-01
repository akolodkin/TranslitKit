namespace cx.core.translit.Tests.Ukrainian;

/// <summary>
/// Tests for the Ukrainian GOST 16876-71 (1971) transliteration.
/// Soviet-era romanization system.
/// </summary>
public class UkrainianGOST1971Tests
{
    private readonly UkrainianGOST1971 _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("б", "b")]
    [InlineData("в", "v")]
    [InlineData("г", "g")]
    [InlineData("ґ", "g")]
    [InlineData("д", "d")]
    [InlineData("е", "e")]
    [InlineData("є", "je")]
    [InlineData("ж", "zh")]
    [InlineData("з", "z")]
    [InlineData("и", "i")]
    [InlineData("і", "i")]
    [InlineData("ї", "ji")]
    [InlineData("й", "j")]
    [InlineData("к", "k")]
    [InlineData("л", "l")]
    [InlineData("м", "m")]
    [InlineData("н", "n")]
    [InlineData("о", "o")]
    [InlineData("п", "p")]
    [InlineData("р", "r")]
    [InlineData("с", "s")]
    [InlineData("т", "t")]
    [InlineData("у", "u")]
    [InlineData("ф", "f")]
    [InlineData("х", "kh")]
    [InlineData("ц", "c")]
    [InlineData("ч", "ch")]
    [InlineData("ш", "sh")]
    [InlineData("щ", "shh")]
    [InlineData("ь", "'")]
    [InlineData("ю", "ju")]
    [InlineData("я", "ja")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Київ", "Kijiv")]
    [InlineData("Україна", "Ukrajina")]
    [InlineData("Харків", "Kharkiv")]
    [InlineData("Львів", "L'viv")]
    [InlineData("Дніпро", "Dnipro")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("КИЇВ", "KIJIV")]
    [InlineData("УКРАЇНА", "UKRAJINA")]
    public void AllUppercase_PreservesCase(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void SoftSign_BecomesApostrophe()
    {
        Assert.Equal("sol'", Translit.Convert("соль", _table));
    }
}
