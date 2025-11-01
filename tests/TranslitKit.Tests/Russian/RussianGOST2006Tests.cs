namespace TranslitKit.Tests.Russian;

/// <summary>
/// Tests for the Russian GOST R 52535.1-2006 transliteration.
/// Official Russian government standard.
/// </summary>
public class RussianGOST2006Tests
{
    private readonly RussianGOST2006 _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("б", "b")]
    [InlineData("в", "v")]
    [InlineData("г", "g")]
    [InlineData("д", "d")]
    [InlineData("е", "e")]
    [InlineData("ё", "e")]
    [InlineData("ж", "zh")]
    [InlineData("з", "z")]
    [InlineData("и", "i")]
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
    [InlineData("ц", "tc")]
    [InlineData("ч", "ch")]
    [InlineData("ш", "sh")]
    [InlineData("щ", "shch")]
    [InlineData("ы", "y")]
    [InlineData("э", "e")]
    [InlineData("ю", "iu")]
    [InlineData("я", "ia")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Москва", "Moskva")]
    [InlineData("Санкт-Петербург", "Sankt-Peterburg")]
    [InlineData("Россия", "Rossiia")]
    [InlineData("Владивосток", "Vladivostok")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("МОСКВА", "MOSKVA")]
    [InlineData("РОССИЯ", "ROSSIIA")]
    public void AllUppercase_PreservesCase(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void HardSign_Deleted()
    {
        Assert.Equal("obekt", Translit.Convert("объект", _table));
    }

    [Fact]
    public void SoftSign_Deleted()
    {
        Assert.Equal("den", Translit.Convert("день", _table));
    }
}
