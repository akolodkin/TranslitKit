namespace TranslitKit.Tests.Ukrainian;

/// <summary>
/// Tests for the Ukrainian WWS (Working Group on Romanization Systems) transliteration.
/// Scholarly system using diacritics.
/// </summary>
public class UkrainianWWSTests
{
    private readonly UkrainianWWS _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("б", "b")]
    [InlineData("в", "v")]
    [InlineData("г", "h")]
    [InlineData("ґ", "g")]
    [InlineData("д", "d")]
    [InlineData("е", "e")]
    [InlineData("є", "je")]
    [InlineData("ж", "ž")]
    [InlineData("з", "z")]
    [InlineData("и", "y")]
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
    [InlineData("х", "x")]
    [InlineData("ц", "c")]
    [InlineData("ч", "č")]
    [InlineData("ш", "š")]
    [InlineData("щ", "šč")]
    [InlineData("ь", "ʹ")]
    [InlineData("ю", "ju")]
    [InlineData("я", "ja")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Київ", "Kyjiv")]
    [InlineData("Україна", "Ukrajina")]
    [InlineData("Харків", "Xarkiv")]
    [InlineData("Львів", "Lʹviv")]
    [InlineData("Дніпро", "Dnipro")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("КИЇВ", "KYJIV")]
    [InlineData("УКРАЇНА", "UKRAJINA")]
    public void AllUppercase_PreservesCase(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ApostrophesDeleted()
    {
        Assert.Equal("mjaso", Translit.Convert("м'ясо", _table));
    }

    [Fact]
    public void UnicodeApostropheDeleted()
    {
        Assert.Equal("mjaso", Translit.Convert("м\u2019ясо", _table));
    }
}
