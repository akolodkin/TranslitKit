namespace TranslitKit.Tests.Russian;

/// <summary>
/// Tests for the Russian ISO 9 System B transliteration.
/// </summary>
public class RussianISO9SystemBTests
{
    private readonly RussianISO9SystemB _table = new();

    [Theory]
    [InlineData("а", "a")]
    [InlineData("ё", "yo")]
    [InlineData("й", "j")]
    [InlineData("х", "x")]
    [InlineData("ц", "cz")]
    [InlineData("щ", "shh")]
    [InlineData("ъ", "''")]
    [InlineData("ы", "y'")]
    [InlineData("ь", "'")]
    [InlineData("э", "e'")]
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
    public void RealWorldExamples_TransliterateCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("центр", "centr")]
    [InlineData("Центр", "Centr")]
    [InlineData("цирк", "cirk")]
    [InlineData("цыплёнок", "cy'plyonok")]
    [InlineData("цюрих", "cyurix")]
    [InlineData("царь", "czar'")]
    public void SpecialCases_CContextDependent_TransliteratesCorrectly(string source, string expected)
    {
        var result = Translit.Convert(source, _table);
        Assert.Equal(expected, result);
    }
}
