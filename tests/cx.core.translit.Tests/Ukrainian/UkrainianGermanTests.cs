namespace TranslitKit.Tests.Ukrainian;

/// <summary>
/// Tests for the Ukrainian German (Duden) transliteration.
/// Adapts Ukrainian sounds to German phonetics.
/// </summary>
public class UkrainianGermanTests
{
    private readonly UkrainianGerman _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("б", "b")]
    [InlineData("в", "w")]
    [InlineData("г", "h")]
    [InlineData("ґ", "g")]
    [InlineData("д", "d")]
    [InlineData("е", "e")]
    [InlineData("є", "je")]
    [InlineData("ж", "sh")]
    [InlineData("з", "s")]
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
    [InlineData("х", "ch")]
    [InlineData("ц", "z")]
    [InlineData("ч", "tsch")]
    [InlineData("ш", "sch")]
    [InlineData("щ", "schtsch")]
    [InlineData("ю", "ju")]
    [InlineData("я", "ja")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Київ", "Kyjiw")]
    [InlineData("Україна", "Ukrajina")]
    [InlineData("Харків", "Charkiw")]
    [InlineData("Львів", "Lwiw")]
    [InlineData("Дніпро", "Dnipro")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("КИЇВ", "KYJIW")]
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
}
