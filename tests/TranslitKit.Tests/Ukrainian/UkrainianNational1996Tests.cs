namespace TranslitKit.Tests.Ukrainian;

/// <summary>
/// Tests for the Ukrainian National 1996 transliteration.
/// Earlier version of the official Ukrainian romanization system.
/// </summary>
public class UkrainianNational1996Tests
{
    private readonly UkrainianNational1996 _table = new();

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
    [InlineData("и", "y")]
    [InlineData("і", "i")]
    [InlineData("ї", "yi")]
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
    [InlineData("у", "u")]
    [InlineData("ф", "f")]
    [InlineData("х", "kh")]
    [InlineData("ц", "ts")]
    [InlineData("ч", "ch")]
    [InlineData("ш", "sh")]
    [InlineData("щ", "sch")]
    [InlineData("ь", "'")]
    [InlineData("ю", "yu")]
    [InlineData("я", "ya")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("розгром", "rozghrom")]
    [InlineData("Розгром", "Rozghrom")]
    [InlineData("РОЗГРОМ", "ROZGHROM")]
    public void SpecialCases_AppliesZghRule(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Євген", "Yevhen")]
    [InlineData("Їжак", "Yizhak")]
    [InlineData("Йорж", "Yorzh")]
    [InlineData("Юрій", "Yurii")]
    [InlineData("Ярослав", "Yaroslav")]
    public void FirstCharacters_WordInitial_UsesAlternativeForm(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("поєднання", "poiednannia")]
    [InlineData("доїла", "doila")]
    [InlineData("май", "mai")]
    [InlineData("каюта", "kaiuta")]
    [InlineData("земля", "zemlia")]
    public void FirstCharacters_NonInitial_UsesStandardForm(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Київ", "Kyiv")]
    [InlineData("Україна", "Ukraina")]
    [InlineData("Харків", "Kharkiv")]
    [InlineData("Львів", "L'viv")]
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
    public void SoftSign_BecomesApostrophe()
    {
        Assert.Equal("sol'", Translit.Convert("соль", _table));
    }
}
