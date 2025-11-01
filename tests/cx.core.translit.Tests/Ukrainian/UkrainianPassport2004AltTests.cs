namespace TranslitKit.Tests.Ukrainian;

/// <summary>
/// Tests for the Ukrainian Passport 2004 Alternative transliteration.
/// Alternative transliteration system for international passports.
/// </summary>
public class UkrainianPassport2004AltTests
{
    private readonly UkrainianPassport2004Alt _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("б", "b")]
    [InlineData("в", "v")]
    [InlineData("г", "g")]
    [InlineData("ґ", "h")]
    [InlineData("д", "d")]
    [InlineData("е", "e")]
    [InlineData("є", "ye")]
    [InlineData("ж", "j")]
    [InlineData("з", "z")]
    [InlineData("и", "y")]
    [InlineData("і", "i")]
    [InlineData("ї", "yi")]
    [InlineData("й", "y")]
    [InlineData("к", "c")]
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
    [InlineData("Євген", "Yevgen")]
    [InlineData("Їжак", "Yijac")]
    [InlineData("Йорж", "Yorj")]
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
    [InlineData("каюта", "caiuta")]
    [InlineData("земля", "zemlia")]
    public void FirstCharacters_NonInitial_UsesStandardForm(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Київ", "Cyiv")]
    [InlineData("Україна", "Ucraina")]
    [InlineData("Харків", "Kharciv")]
    [InlineData("Львів", "L'viv")]
    [InlineData("Дніпро", "Dnipro")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("КИЇВ", "CYIV")]
    [InlineData("УКРАЇНА", "UCRAINA")]
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
