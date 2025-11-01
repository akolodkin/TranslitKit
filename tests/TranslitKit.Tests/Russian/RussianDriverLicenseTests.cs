namespace TranslitKit.Tests.Russian;

/// <summary>
/// Tests for the Russian Driver License transliteration.
/// </summary>
public class RussianDriverLicenseTests
{
    private readonly RussianDriverLicense _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("ё", "yo")]
    [InlineData("й", "y")]
    [InlineData("ъ", "'")]
    [InlineData("ь", "'")]
    [InlineData("ю", "yu")]
    [InlineData("я", "ya")]
    public void BasicCharacters_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Москва", "Moskva")]
    [InlineData("Россия", "Rossiya")]
    [InlineData("Алёна", "Alyena")]
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("семье", "sem'ye")]
    [InlineData("объект", "ob'yekt")]
    [InlineData("ружьё", "ruzh'yo")]
    [InlineData("чёрный", "chernyy")]
    [InlineData("шёлк", "shelk")]
    [InlineData("житьё", "zhit'yo")]
    [InlineData("воробьи", "vorob'yi")]
    public void SpecialCases_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Евгений", "Yevgeniy")]
    [InlineData("Ёлка", "Yolka")]
    public void FirstCharacters_WordInitial_UsesAlternativeForm(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }
}
