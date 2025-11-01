using Xunit;

namespace TranslitKit.Tests.Ukrainian;

/// <summary>
/// Tests for UkrainianKMU transliteration table (National 2010 standard).
/// This is the default Ukrainian transliteration system.
/// Following TDD: Writing tests before implementation.
/// </summary>
public class UkrainianKMUTests
{
    private readonly UkrainianKMU _table = new();

    #region Basic Character Mappings

    [Theory]
    [InlineData("а", "a")]
    [InlineData("б", "b")]
    [InlineData("в", "v")]
    [InlineData("г", "h")]
    [InlineData("ґ", "g")]
    [InlineData("д", "d")]
    [InlineData("е", "e")]
    [InlineData("є", "ye")] // Word-initial form (standalone character is at boundary)
    [InlineData("ж", "zh")]
    [InlineData("з", "z")]
    [InlineData("и", "y")]
    [InlineData("і", "i")]
    [InlineData("ї", "yi")] // Word-initial form (standalone character is at boundary)
    [InlineData("й", "y")]  // Word-initial form (standalone character is at boundary)
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
    [InlineData("ю", "yu")] // Word-initial form (standalone character is at boundary)
    [InlineData("я", "ya")] // Word-initial form (standalone character is at boundary)
    public void Convert_BasicCharacters_TransliteratesCorrectly(string input, string expected)
    {
        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("А", "A")]
    [InlineData("Б", "B")]
    [InlineData("В", "V")]
    [InlineData("Г", "H")]
    [InlineData("Ґ", "G")]
    [InlineData("Д", "D")]
    [InlineData("Е", "E")]
    [InlineData("Є", "YE")] // Word-initial, then uppercase preserv ation applies
    [InlineData("Ж", "ZH")] // All caps → uppercase
    [InlineData("З", "Z")]
    [InlineData("И", "Y")]
    [InlineData("І", "I")]
    [InlineData("Ї", "YI")] // Word-initial, then uppercase preservation
    [InlineData("Й", "Y")]  // Word-initial
    [InlineData("К", "K")]
    [InlineData("Л", "L")]
    [InlineData("М", "M")]
    [InlineData("Н", "N")]
    [InlineData("О", "O")]
    [InlineData("П", "P")]
    [InlineData("Р", "R")]
    [InlineData("С", "S")]
    [InlineData("Т", "T")]
    [InlineData("У", "U")]
    [InlineData("Ф", "F")]
    [InlineData("Х", "KH")] // All caps → uppercase
    [InlineData("Ц", "TS")] // All caps → uppercase
    [InlineData("Ч", "CH")] // All caps → uppercase
    [InlineData("Ш", "SH")] // All caps → uppercase
    [InlineData("Щ", "SHCH")] // All caps → uppercase
    [InlineData("Ю", "YU")] // Word-initial, then uppercase
    [InlineData("Я", "YA")] // Word-initial, then uppercase
    public void Convert_UppercaseCharacters_TransliteratesCorrectly(string input, string expected)
    {
        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region Delete Cases

    [Fact]
    public void Convert_DeleteCases_RemovesSoftSign()
    {
        // Act & Assert
        Assert.Equal("mat", Translit.Convert("мать", _table));  // ь soft sign
        Assert.Equal("Mat", Translit.Convert("Мать", _table));  // Ь uppercase soft sign
    }

    [Fact]
    public void Convert_DeleteCases_RemovesAsciiApostrophe()
    {
        // Act
        string result = Translit.Convert("м'ясо", _table);  // ASCII apostrophe U+0027

        // Assert
        Assert.Equal("miaso", result);
    }

    [Fact]
    public void Convert_DeleteCases_RemovesUnicodeApostrophe2019()
    {
        // Act
        string result = Translit.Convert("м'ясо", _table);  // Right single quotation mark U+2019

        // Assert
        Assert.Equal("miaso", result);
    }

    [Fact]
    public void Convert_DeleteCases_RemovesModifierApostrophe()
    {
        // Act
        string result = Translit.Convert("м\u02BCясо", _table);  // Modifier letter apostrophe U+02BC

        // Assert
        Assert.Equal("miaso", result);
    }

    #endregion

    #region Special Cases

    [Theory]
    [InlineData("розгром", "rozghrom")] // зг → zgh (lowercase)
    [InlineData("РОЗГРОМ", "ROZGHROM")] // ЗГ → ZGh → ROZGHROM (uppercase all)
    [InlineData("Розгром", "Rozghrom")] // Зг → Zgh (title case)
    public void Convert_SpecialCases_AppliesZghRule(string input, string expected)
    {
        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region First Characters (Word-Initial)

    [Theory]
    [InlineData("Євген", "Yevhen")] // Є at start → Ye
    [InlineData("Їжак", "Yizhak")] // Ї at start → Yi
    [InlineData("Йорж", "Yorzh")] // Й at start → Y
    [InlineData("Юрій", "Yurii")] // Ю at start → Yu
    [InlineData("Ярослав", "Yaroslav")] // Я at start → Ya
    public void Convert_FirstCharacters_WordInitial_UsesAlternativeForm(string input, string expected)
    {
        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("поєднання", "poiednannia")] // є in middle → ie
    [InlineData("їжа", "yizha")] // ї at start → yi
    [InlineData("доїла", "doila")] // ї in middle → i
    [InlineData("май", "mai")] // й at end → i
    [InlineData("каюта", "kaiuta")] // ю in middle → iu
    [InlineData("земля", "zemlia")] // я at end → ia
    public void Convert_FirstCharacters_NonInitial_UsesStandardForm(string input, string expected)
    {
        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region Real-World Examples

    [Fact]
    public void Convert_UkrainianCities_TransliteratesCorrectly()
    {
        // Arrange & Act & Assert
        Assert.Equal("Kyiv", Translit.Convert("Київ", _table));
        Assert.Equal("Kharkiv", Translit.Convert("Харків", _table));
        Assert.Equal("Lviv", Translit.Convert("Львів", _table));
        Assert.Equal("Odesa", Translit.Convert("Одеса", _table));
        Assert.Equal("Dnipro", Translit.Convert("Дніпро", _table));
    }

    [Fact]
    public void Convert_UkrainianNames_TransliteratesCorrectly()
    {
        // Arrange & Act & Assert
        Assert.Equal("Yurii", Translit.Convert("Юрій", _table));
        Assert.Equal("Yaroslav", Translit.Convert("Ярослав", _table));
        Assert.Equal("Yevhen", Translit.Convert("Євген", _table));
        Assert.Equal("Yizhak", Translit.Convert("Їжак", _table));
        Assert.Equal("Halyna", Translit.Convert("Галина", _table));
        Assert.Equal("Volodymyr", Translit.Convert("Володимир", _table));
    }

    [Fact]
    public void Convert_ExampleFromPythonLibrary_MatchesExpectedOutput()
    {
        // This is the example from the Python library README
        // Arrange
        string input = "Берег моря. Чути розбещені крики морських птахів";

        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        // Expected: "Bereh moria. Chuty rozbeshcheni kryky morskykh ptakhiv"
        Assert.Equal("Bereh moria. Chuty rozbeshcheni kryky morskykh ptakhiv", result);
    }

    [Fact]
    public void Convert_ComplexSentence_WithAllFeatures()
    {
        // Arrange - sentence with special cases, first characters, soft signs
        string input = "Є єдність у розгромі ворогів";

        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        // Є (first word) → Ye
        // єдність (є at word boundary after space) → yednist
        // розгромі → rozghromi (зг → zgh)
        Assert.Equal("Ye yednist u rozghromi vorohiv", result);
    }

    #endregion

    #region Case Preservation

    [Fact]
    public void Convert_AllUppercase_PreservesCase()
    {
        // Arrange
        string input = "УКРАЇНА";

        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        Assert.Equal("UKRAINA", result);
    }

    [Fact]
    public void Convert_MixedCase_MaintainsPattern()
    {
        // Arrange
        string input = "Україна";

        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        Assert.Equal("Ukraina", result);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Convert_EmptyString_ReturnsEmpty()
    {
        // Act
        string result = Translit.Convert("", _table);

        // Assert
        Assert.Equal("", result);
    }

    [Fact]
    public void Convert_OnlyPunctuation_PreservesPunctuation()
    {
        // Act
        string result = Translit.Convert(".,!?;:", _table);

        // Assert
        Assert.Equal(".,!?;:", result);
    }

    [Fact]
    public void Convert_MixedCyrillicAndLatin_PreservesLatin()
    {
        // Act
        string result = Translit.Convert("Kyiv Київ", _table);

        // Assert
        Assert.Equal("Kyiv Kyiv", result);
    }

    #endregion
}
