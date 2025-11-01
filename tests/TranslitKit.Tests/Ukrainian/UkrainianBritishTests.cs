namespace TranslitKit.Tests.Ukrainian;

/// <summary>
/// Tests for the Ukrainian British Standard transliteration.
/// Uses macrons and breves for specific characters.
/// </summary>
public class UkrainianBritishTests
{
    private readonly UkrainianBritish _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("б", "b")]
    [InlineData("в", "v")]
    [InlineData("г", "h")]
    [InlineData("ґ", "g")]
    [InlineData("д", "d")]
    [InlineData("е", "e")]
    [InlineData("є", "ye")]
    [InlineData("ж", "zh")]
    [InlineData("з", "z")]
    [InlineData("и", "ȳ")]
    [InlineData("і", "i")]
    [InlineData("ї", "yi")]
    [InlineData("й", "ĭ")]
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
    [InlineData("ю", "yu")]
    [InlineData("я", "ya")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Київ", "Kȳyiv")]
    [InlineData("Україна", "Ukrayina")]
    [InlineData("Харків", "Kharkiv")]
    [InlineData("Львів", "Lviv")]
    [InlineData("Дніпро", "Dnipro")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void SoftSignDeleted()
    {
        Assert.Equal("sol", Translit.Convert("сольь", _table));
    }

    [Fact]
    public void ApostrophesDeleted()
    {
        Assert.Equal("myaso", Translit.Convert("м'ясо", _table));
    }
}
