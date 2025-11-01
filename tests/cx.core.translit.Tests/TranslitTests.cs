using System.Text.RegularExpressions;
using Xunit;

namespace TranslitKit.Tests;

/// <summary>
/// Tests for Translit static class - main transliteration engine.
/// Following TDD: Writing tests before implementation.
/// </summary>
public class TranslitTests
{
    #region Test Helper - Mock Table

    /// <summary>
    /// Simple mock transliteration table for testing.
    /// </summary>
    private class SimpleTestTable : TransliterationTableBase
    {
        public SimpleTestTable()
            : base(
                mainTable: new Dictionary<string, string>
                {
                    { "а", "a" },
                    { "б", "b" },
                    { "в", "v" },
                    { "г", "h" },
                    { "д", "d" },
                    { "е", "e" },
                    { "є", "ie" },
                    { "ж", "zh" },
                    { "з", "z" },
                    { "и", "y" },
                    { "і", "i" },
                    { "ї", "i" },
                    { "й", "i" },
                    { "к", "k" },
                    { "л", "l" },
                    { "м", "m" },
                    { "н", "n" },
                    { "о", "o" },
                    { "п", "p" },
                    { "р", "r" },
                    { "с", "s" },
                    { "т", "t" },
                    { "у", "u" },
                    { "ф", "f" },
                    { "х", "kh" },
                    { "ц", "ts" },
                    { "ч", "ch" },
                    { "ш", "sh" },
                    { "щ", "shch" },
                    { "ю", "iu" },
                    { "я", "ia" }
                },
                specialCases: new Dictionary<string, string>
                {
                    { "зг", "zgh" },
                    { "ЗГ", "ZGh" },
                    { "Зг", "Zgh" }
                },
                firstCharacters: new Dictionary<string, string>
                {
                    { "є", "ye" },
                    { "Є", "Ye" },
                    { "ї", "yi" },
                    { "Ї", "Yi" },
                    { "й", "y" },
                    { "Й", "Y" },
                    { "ю", "yu" },
                    { "Ю", "Yu" },
                    { "я", "ya" },
                    { "Я", "Ya" }
                },
                deleteChars: new[] { "ь", "Ь", "'", "\u2019", "\u02BC" })
        {
        }
    }

    #endregion

    #region Basic Functionality Tests

    [Fact]
    public void Convert_SimpleText_ReturnsTransliterated()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "привіт";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("pryvit", result);
    }

    [Fact]
    public void Convert_MultiCharacterMappings_WorksCorrectly()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "жаба"; // zh-a-b-a

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("zhaba", result);
    }

    [Fact]
    public void Convert_MixedCase_PreservesCase()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "Привіт";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("Pryvit", result);
    }

    #endregion

    #region Stage 1: Delete Pattern Tests

    [Fact]
    public void Convert_Stage1_DeletesSoftSign()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "мать"; // mat' (with soft sign)

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("mat", result); // ь should be deleted
    }

    [Fact]
    public void Convert_Stage1_DeletesApostrophes()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "м'ясо"; // m'iaso

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("miaso", result); // ' should be deleted
    }

    [Fact]
    public void Convert_Stage1_DeletesUnicodeApostrophes()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "м\u2019ясо"; // m'iaso (Unicode apostrophe U+2019)

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("miaso", result);
    }

    #endregion

    #region Stage 2: Special Cases Tests

    [Fact]
    public void Convert_Stage2_AppliesSpecialCases()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "розгром"; // "зг" should become "zgh"

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("rozghrom", result);
    }

    [Fact]
    public void Convert_Stage2_SpecialCasesUppercase()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "РОЗГРОМ"; // "ЗГ" should become "ZGh", then Stage 5 uppercases all

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        // Stage 2 applies "ЗГ" → "ZGh" giving "ROZGhROM"
        // Stage 5 uppercases everything because source was all caps: "ROZGHROM"
        Assert.Equal("ROZGHROM", result);
    }

    [Fact]
    public void Convert_Stage2_SpecialCasesMixedCase()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "Розгром"; // "Зг" should become "Zgh"

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("Rozghrom", result);
    }

    #endregion

    #region Stage 3: First Characters Tests

    [Fact]
    public void Convert_Stage3_FirstCharacterAtWordStart()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "Євген"; // "Є" at start should become "Ye"

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("Yevhen", result);
    }

    [Fact]
    public void Convert_Stage3_FirstCharacterMidWord()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "поєднання"; // "є" in middle should become "ie" (from main table)

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("poiednannia", result);
    }

    [Fact]
    public void Convert_Stage3_MultipleWordsWithFirstCharacters()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "Єва і Юрій"; // Both start with first-character rules

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("Yeva i Yurii", result);
    }

    [Fact]
    public void Convert_Stage3_FirstCharacterAfterSpace()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "моє ім'я"; // "є" after space is NOT word-initial (after moie)

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        // "моє" = "moie" (є is not at word boundary after о)
        // "ім'я" = "imia" (' deleted, я not at start)
        Assert.Equal("moie imia", result);
    }

    #endregion

    #region Stage 4: Main Transliteration Tests

    [Fact]
    public void Convert_Stage4_AllBasicCharacters()
    {
        // Arrange
        var table = new SimpleTestTable();
        // а б в г д е ж з и к л м н о п р с т у ф х ц ч ш щ
        // a b v h d e zh z y k l m n o p r s t u f kh ts ch sh shch
        string input = "абвгдежзиклмнопрстуфхцчшщ";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        // Each character should be transliterated according to the table
        Assert.NotEmpty(result);
        Assert.Contains("zh", result); // ж → zh
        Assert.Contains("kh", result); // х → kh
        Assert.Contains("ts", result); // ц → ts
        Assert.Contains("ch", result); // ч → ch
        Assert.Contains("sh", result); // ш → sh
        Assert.Contains("shch", result); // щ → shch
        // Just verify no Cyrillic remains
        Assert.DoesNotMatch(@"[а-яА-ЯёЁ]", result);
    }

    [Fact]
    public void Convert_Stage4_MaintainsNonCyrillicCharacters()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "test123!@#";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("test123!@#", result); // Non-Cyrillic should pass through
    }

    #endregion

    #region Stage 5: Case Preservation Tests

    [Fact]
    public void Convert_Stage5_AllUppercase_PreservesWhenFlagTrue()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "ПРИВІТ";

        // Act
        string result = Translit.Convert(input, table, preserveCase: true);

        // Assert
        Assert.Equal("PRYVIT", result);
    }

    [Fact]
    public void Convert_Stage5_AllUppercase_DoesNotPreserveWhenFlagFalse()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "ПРИВІТ";

        // Act
        string result = Translit.Convert(input, table, preserveCase: false);

        // Assert
        Assert.Equal("Pryvit", result); // Only first char uppercase from natural capitalization
    }

    [Fact]
    public void Convert_Stage5_MixedCase_DoesNotApplyUppercase()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "ПрИвІт"; // Mixed case

        // Act
        string result = Translit.Convert(input, table, preserveCase: true);

        // Assert
        // Mixed case should not trigger all-uppercase conversion
        Assert.NotEqual("PRYVIT", result);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Convert_EmptyString_ReturnsEmpty()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("", result);
    }

    [Fact]
    public void Convert_NullString_ReturnsEmpty()
    {
        // Arrange
        var table = new SimpleTestTable();
        string? input = null;

        // Act
        string result = Translit.Convert(input!, table);

        // Assert
        Assert.Equal("", result);
    }

    [Fact]
    public void Convert_OnlySpaces_ReturnsSpaces()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "   ";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("   ", result);
    }

    [Fact]
    public void Convert_MixedCyrillicAndLatin_PreservesLatin()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "hello привіт world";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("hello pryvit world", result);
    }

    [Fact]
    public void Convert_SpecialCharactersAndPunctuation_Preserved()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "Привіт, світ! Як справи?";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("Pryvit, svit! Yak spravy?", result);
    }

    [Fact]
    public void Convert_VeryLongString_HandlesCorrectly()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = string.Concat(Enumerable.Repeat("привіт ", 1000));

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal(string.Concat(Enumerable.Repeat("pryvit ", 1000)), result);
    }

    #endregion

    #region Integration Tests - Full Pipeline

    [Fact]
    public void Convert_FullPipeline_AllStagesWork()
    {
        // Arrange
        var table = new SimpleTestTable();
        // This string exercises all stages:
        // - Delete: ь and '
        // - Special case: зг
        // - First char: Є at start
        // - Main: all other chars
        // - Case: not all uppercase
        string input = "Єдність розгром'ять";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        // Є (first) -> Ye
        // дність -> dnist
        // розгром'ять -> rozghromiat (зг->zgh, '->deleted, ь->deleted)
        Assert.Equal("Yednist rozghromiat", result);
    }

    [Fact]
    public void Convert_RealWorldExample_UkrainianSentence()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "Берег моря. Чути розбещені крики морських птахів";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        // Not testing exact output since this is a simplified table,
        // but verify it doesn't crash and produces reasonable output
        Assert.NotEmpty(result);
        Assert.DoesNotContain("ь", result);
        Assert.Contains("morskykh", result); // Check морських -> morskykh
    }

    #endregion
}
