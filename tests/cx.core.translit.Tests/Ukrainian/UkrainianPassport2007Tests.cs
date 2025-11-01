namespace TranslitKit.Tests.Ukrainian;

/// <summary>
/// Tests for the Ukrainian Passport 2007 transliteration.
/// Used for international passports issued before 2010.
/// </summary>
public class UkrainianPassport2007Tests
{
    private readonly UkrainianPassport2007 _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("б", "b")]
    [InlineData("в", "v")]
    [InlineData("г", "g")]
    [InlineData("ґ", "g")]
    [InlineData("д", "d")]
    [InlineData("е", "e")]
    [InlineData("є", "ie")]
    [InlineData("ж", "zh")]
    [InlineData("з", "z")]
    [InlineData("и", "y")]
    [InlineData("і", "i")]
    [InlineData("ї", "i")]
    [InlineData("й", "i")]
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
    [InlineData("ц", "ts")]
    [InlineData("ч", "ch")]
    [InlineData("ш", "sh")]
    [InlineData("щ", "shch")]
    [InlineData("ю", "iu")]
    [InlineData("я", "ia")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Київ", "Kyiv")]
    [InlineData("Україна", "Ukraina")]
    [InlineData("Харків", "Kharkiv")]
    [InlineData("Львів", "Lviv")]
    [InlineData("Дніпро", "Dnipro")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("КИЇВ", "KYIV")]
    [InlineData("УКРАЇНА", "UKRAINA")]
    public void AllUppercase_PreservesCase(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void SoftSignDeleted()
    {
        Assert.Equal("sol", Translit.Convert("соль", _table));
    }

    [Fact]
    public void ApostrophesDeleted()
    {
        Assert.Equal("miaso", Translit.Convert("м'ясо", _table));
    }
}
