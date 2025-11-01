namespace TranslitKit.Tests.Ukrainian;

/// <summary>
/// Tests for the Ukrainian ISO 9 transliteration.
/// International standard for Cyrillic transliteration.
/// </summary>
public class UkrainianISO9Tests
{
    private readonly UkrainianISO9 _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("б", "b")]
    [InlineData("в", "v")]
    [InlineData("г", "g")]
    [InlineData("ґ", "g̀")]
    [InlineData("д", "d")]
    [InlineData("е", "e")]
    [InlineData("є", "ê")]
    [InlineData("ж", "ž")]
    [InlineData("з", "z")]
    [InlineData("и", "i")]
    [InlineData("і", "ì")]
    [InlineData("ї", "ï")]
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
    [InlineData("щ", "ŝ")]
    [InlineData("ь", "′")]
    [InlineData("ю", "û")]
    [InlineData("я", "â")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Київ", "Kiïv")]
    [InlineData("Україна", "Ukraïna")]
    [InlineData("Харків", "Harkìv")]
    [InlineData("Львів", "L′vìv")]
    [InlineData("Дніпро", "Dnìpro")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("КИЇВ", "KIÏV")]
    [InlineData("УКРАЇНА", "UKRAÏNA")]
    public void AllUppercase_PreservesCase(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }
}
