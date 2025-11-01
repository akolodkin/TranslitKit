using Xunit;

namespace cx.core.translit.Tests.Ukrainian;

/// <summary>
/// Tests for UkrainianSimple transliteration table.
/// A simplified Ukrainian transliteration system.
/// Following TDD: Writing tests before implementation.
/// </summary>
public class UkrainianSimpleTests
{
    private readonly UkrainianSimple _table = new();

    #region Basic Character Mappings

    [Theory]
    [InlineData("а", "a")]
    [InlineData("б", "b")]
    [InlineData("в", "v")]
    [InlineData("г", "h")]
    [InlineData("ґ", "g")]
    [InlineData("д", "d")]
    [InlineData("е", "e")]
    [InlineData("є", "ye")]  // Always ye (no word-boundary rules)
    [InlineData("ж", "zh")]
    [InlineData("з", "z")]
    [InlineData("и", "y")]
    [InlineData("і", "i")]
    [InlineData("ї", "yi")]  // Always yi (no word-boundary rules)
    [InlineData("й", "j")]   // Different from KMU (j instead of i/y)
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
    [InlineData("ь", "'")]   // Soft sign becomes apostrophe (not deleted)
    [InlineData("ю", "ju")]  // Different from KMU (ju instead of iu/yu)
    [InlineData("я", "ja")]  // Different from KMU (ja instead of ia/ya)
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
    [InlineData("Є", "YE")]
    [InlineData("Ж", "ZH")]
    [InlineData("З", "Z")]
    [InlineData("И", "Y")]
    [InlineData("І", "I")]
    [InlineData("Ї", "YI")]
    [InlineData("Й", "J")]
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
    [InlineData("Х", "KH")]
    [InlineData("Ц", "TS")]
    [InlineData("Ч", "CH")]
    [InlineData("Ш", "SH")]
    [InlineData("Щ", "SHCH")]
    [InlineData("Ь", "'")]
    [InlineData("Ю", "JU")]
    [InlineData("Я", "JA")]
    public void Convert_UppercaseCharacters_TransliteratesCorrectly(string input, string expected)
    {
        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region No Special Features

    [Fact]
    public void Convert_SoftSign_BecomesApostrophe()
    {
        // Arrange
        string input = "мать";

        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        Assert.Equal("mat'", result);  // ь → ' (not deleted like in KMU)
    }

    [Fact]
    public void Convert_NoSpecialCases_TransliteratesNormally()
    {
        // Arrange - "зг" has no special handling in Simple
        string input = "розгром";

        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        Assert.Equal("rozhrom", result);  // з→z, г→h = "zh" (no special zgh rule like KMU)
    }

    [Fact]
    public void Convert_NoWordBoundaryRules_ConsistentMapping()
    {
        // Arrange - є always becomes ye, not ie
        string input = "Євген поєднання";

        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        Assert.Equal("Yevhen poyednannja", result);  // є → ye everywhere
    }

    #endregion

    #region Real-World Examples

    [Fact]
    public void Convert_UkrainianCities_TransliteratesCorrectly()
    {
        // Act & Assert
        Assert.Equal("Kyyiv", Translit.Convert("Київ", _table));      // и→y, ї→yi = "yyi"
        Assert.Equal("Kharkiv", Translit.Convert("Харків", _table));
        Assert.Equal("L'viv", Translit.Convert("Львів", _table));     // ь → '
        Assert.Equal("Odesa", Translit.Convert("Одеса", _table));
        Assert.Equal("Dnipro", Translit.Convert("Дніпро", _table));
    }

    [Fact]
    public void Convert_UkrainianNames_TransliteratesCorrectly()
    {
        // Act & Assert
        Assert.Equal("Jurij", Translit.Convert("Юрій", _table));      // ю→ju, й→j
        Assert.Equal("Jaroslav", Translit.Convert("Ярослав", _table)); // я→ja
        Assert.Equal("Yevhen", Translit.Convert("Євген", _table));    // є→ye
        Assert.Equal("Yizhak", Translit.Convert("Їжак", _table));     // ї→yi
        Assert.Equal("Halyna", Translit.Convert("Галина", _table));
    }

    [Fact]
    public void Convert_ComplexSentence_WorksCorrectly()
    {
        // Arrange
        string input = "Україна є beautiful";

        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        Assert.Equal("Ukrayina ye beautiful", result);  // ї→yi makes "ayina"
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
        Assert.Equal("UKRAYINA", result);  // Ї→YI
    }

    [Fact]
    public void Convert_MixedCase_MaintainsPattern()
    {
        // Arrange
        string input = "Україна";

        // Act
        string result = Translit.Convert(input, _table);

        // Assert
        Assert.Equal("Ukrayina", result);  // ї→yi
    }

    #endregion
}
