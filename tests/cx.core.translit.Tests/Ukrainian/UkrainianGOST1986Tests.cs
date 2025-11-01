namespace TranslitKit.Tests.Ukrainian;

/// <summary>
/// Tests for the Ukrainian GOST 7.79-2000 (1986 variant) transliteration.
/// Improved Soviet-era romanization with diacritics.
/// </summary>
public class UkrainianGOST1986Tests
{
    private readonly UkrainianGOST1986 _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("б", "b")]
    [InlineData("в", "v")]
    [InlineData("г", "g")]
    [InlineData("ґ", "g")]
    [InlineData("д", "d")]
    [InlineData("е", "e")]
    [InlineData("є", "je")]
    [InlineData("ж", "ž")]
    [InlineData("з", "z")]
    [InlineData("и", "i")]
    [InlineData("і", "i")]
    [InlineData("ї", "i")]
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
    [InlineData("х", "h")]
    [InlineData("ц", "c")]
    [InlineData("ч", "č")]
    [InlineData("ш", "š")]
    [InlineData("щ", "šč")]
    [InlineData("ь", "'")]
    [InlineData("ю", "ju")]
    [InlineData("я", "ja")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Київ", "Kiiv")]
    [InlineData("Україна", "Ukraina")]
    [InlineData("Харків", "Harkiv")]
    [InlineData("Львів", "L'viv")]
    [InlineData("Дніпро", "Dnipro")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("КИЇВ", "KIIV")]
    [InlineData("УКРАЇНА", "UKRAINA")]
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
    public void SoftSign_BecomesApostrophe()
    {
        Assert.Equal("sol'", Translit.Convert("соль", _table));
    }
}
