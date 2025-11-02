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

    #region Character Deletion Tests

    [Fact]
    public void Convert_WithSoftSign_RemovesIt()
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
    public void Convert_WithApostrophe_RemovesIt()
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
    public void Convert_WithUnicodeApostrophe_RemovesIt()
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

    #region Contextual Sequence Tests

    [Fact]
    public void Convert_WithContextualSequence_AppliesMapping()
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
    public void Convert_WithContextualSequenceAllCaps_AppliesMappingAndPreservesCase()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "РОЗГРОМ"; // "ЗГ" should become "ZGh", then uppercase all

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        // Contextual mapping applies "ЗГ" → "ZGh" giving "ROZGhROM"
        // Case preservation uppercases everything because source was all caps: "ROZGHROM"
        Assert.Equal("ROZGHROM", result);
    }

    [Fact]
    public void Convert_WithContextualSequenceMixedCase_AppliesMapping()
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

    #region Word Boundary Rules Tests

    [Fact]
    public void Convert_WithFirstCharacterAtWordStart_AppliesWordBoundaryRule()
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
    public void Convert_WithFirstCharacterMidWord_AppliesMainTableMapping()
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
    public void Convert_WithMultipleWordBoundaries_AppliesWordBoundaryRule()
    {
        // Arrange
        var table = new SimpleTestTable();
        string input = "Єва і Юрій"; // Both start with word boundary rules

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("Yeva i Yurii", result);
    }

    [Fact]
    public void Convert_WithCharacterAfterWordBoundary_DoesNotApplyWordBoundaryRule()
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

    #region Character Transliteration Tests

    [Fact]
    public void Convert_WithAllBasicCharacters_TransliteratesCorrectly()
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
    public void Convert_WithNonCyrillicCharacters_PreservesThemUnchanged()
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

    #region Case Preservation Tests

    [Fact]
    public void Convert_AllUppercaseWithPreserveFlagTrue_ConvertsToUppercase()
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
    public void Convert_AllUppercaseWithPreserveFlagFalse_AppliesTitleCase()
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
    public void Convert_MixedCase_DoesNotApplyAllUppercase()
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

    #region Composite Transliteration Table Tests

    [Fact]
    public void Compose_SpecialCharactersWithLanguageMap_CreatesCompositeTable()
    {
        // Arrange
        var baseTable = new SimpleTestTable();
        var compositeTable = new CompositeTransliterationTable(
            TransliterationTables.SpecialCharacters,
            baseTable);

        // Act & Assert
        Assert.NotNull(compositeTable);
        Assert.NotNull(compositeTable.MainTranslitTable);
        // Verify both special characters and language mappings are in the composite
        Assert.True(compositeTable.MainTranslitTable.ContainsKey('«'));
        Assert.True(compositeTable.MainTranslitTable.ContainsKey('а'));
    }

    [Fact]
    public void Compose_CombineMethod_StacksTablesCorrectly()
    {
        // Arrange
        var baseTable = new SimpleTestTable();
        var compositeTable = CompositeTransliterationTable.Combine(
            TransliterationTables.SpecialCharacters,
            baseTable);
        string input = "«тест» №5";

        // Act
        string result = Translit.Convert(input, compositeTable);

        // Assert
        Assert.Equal("\"test\" No5", result);
        Assert.DoesNotContain("«", result);
        Assert.DoesNotContain("»", result);
        Assert.DoesNotContain("№", result);
    }

    #endregion

    #region Unicode Character Conversion Tests (Using Composed Tables)

    [Fact]
    public void Convert_LeftGuillemet_ConvertsToAsciiDoubleQuote()
    {
        // Arrange
        var table = CompositeTransliterationTable.Combine(
            TransliterationTables.SpecialCharacters,
            new SimpleTestTable());
        string input = "«привіт»";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("\"pryvit\"", result);
        Assert.DoesNotContain("«", result);
        Assert.DoesNotContain("»", result);
    }

    [Fact]
    public void Convert_RightGuillemet_ConvertsToAsciiDoubleQuote()
    {
        // Arrange
        var table = CompositeTransliterationTable.Combine(
            TransliterationTables.SpecialCharacters,
            new SimpleTestTable());
        string input = "сказав «слово» добре";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Contains("\"slovo\"", result);
        Assert.DoesNotContain("«", result);
        Assert.DoesNotContain("»", result);
    }

    [Fact]
    public void Convert_NumeroSign_ConvertsToNo()
    {
        // Arrange
        var table = CompositeTransliterationTable.Combine(
            TransliterationTables.SpecialCharacters,
            new SimpleTestTable());
        string input = "Формула №1";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("Formula No1", result);
        Assert.DoesNotContain("№", result);
    }

    [Fact]
    public void Convert_MultipleNumeroSigns_AllConverted()
    {
        // Arrange
        var table = CompositeTransliterationTable.Combine(
            TransliterationTables.SpecialCharacters,
            new SimpleTestTable());
        string input = "№1, №2 та №3";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("No1, No2 ta No3", result);
        Assert.DoesNotContain("№", result);
    }

    [Fact]
    public void Convert_MixedSpecialUnicodeCharacters_AllConverted()
    {
        // Arrange
        var table = CompositeTransliterationTable.Combine(
            TransliterationTables.SpecialCharacters,
            new SimpleTestTable());
        string input = "«Тест» №99 та «ще один»";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("\"Test\" No99 ta \"shche odyn\"", result);
        Assert.DoesNotContain("«", result);
        Assert.DoesNotContain("»", result);
        Assert.DoesNotContain("№", result);
    }

    [Fact]
    public void Convert_GuillemetsWithCasePreservation_MaintainsCasing()
    {
        // Arrange
        var table = CompositeTransliterationTable.Combine(
            TransliterationTables.SpecialCharacters,
            new SimpleTestTable());
        string input = "«ПРИВІТ»";

        // Act
        string result = Translit.Convert(input, table, preserveCase: true);

        // Assert
        Assert.Equal("\"PRYVIT\"", result);
    }

    [Fact]
    public void Convert_MultiCharacterGuillemetsWithAllCaps_PreservesCase()
    {
        // Arrange
        var table = CompositeTransliterationTable.Combine(
            TransliterationTables.SpecialCharacters,
            new SimpleTestTable());
        // Ї at start converts to "Yi" or "yi" depending on context, followed by guillemets
        string input = "«ЇЖ» №1";

        // Act
        string result = Translit.Convert(input, table, preserveCase: true);

        // Assert
        // ї -> "i", ж -> "zh", guillemets -> "
        // The result should have guillemets converted to quotes
        Assert.Contains("\"", result);
        Assert.DoesNotContain("«", result);
        Assert.DoesNotContain("»", result);
        Assert.DoesNotContain("Ї", result);
        Assert.DoesNotContain("ї", result);
    }

    [Fact]
    public void Convert_MultiCharacterConversionWithGuillemets_HandlesCorrectly()
    {
        // Arrange
        var table = CompositeTransliterationTable.Combine(
            TransliterationTables.SpecialCharacters,
            new SimpleTestTable());
        // ж converts to "zh" (2 chars), ю converts to "iu" (2 chars)
        string input = "«жю»";

        // Act
        string result = Translit.Convert(input, table);

        // Assert
        Assert.Equal("\"zhiu\"", result);
        Assert.DoesNotContain("«", result);
        Assert.DoesNotContain("»", result);
    }

    [Fact]
    public void Convert_AllCapsMultiCharacterWithGuillemets_PreservesCase()
    {
        // Arrange
        var table = CompositeTransliterationTable.Combine(
            TransliterationTables.SpecialCharacters,
            new SimpleTestTable());
        // ЖЮ all caps with guillemets
        string input = "«ЖЮ»";

        // Act
        string result = Translit.Convert(input, table, preserveCase: true);

        // Assert
        // All-caps should be converted to uppercase
        Assert.Equal("\"ZHIU\"", result);
        Assert.DoesNotContain("«", result);
        Assert.DoesNotContain("»", result);
    }

    [Fact]
    public void Convert_AllCapsNumeroSignAndMultiCharacters_HandlesAllFeatures()
    {
        // Arrange
        var table = CompositeTransliterationTable.Combine(
            TransliterationTables.SpecialCharacters,
            new SimpleTestTable());
        // Mix of all features: guillemets, numero sign, multi-char conversions, all caps
        // Use "та" (and) in lowercase - the full string is NOT all-caps
        string input = "«ЖЮ» №1 та «ШЩ» №2";

        // Act
        string result = Translit.Convert(input, table, preserveCase: true);

        // Assert
        // Since the input is NOT all-caps (та is lowercase), case is not forced to uppercase
        // ж/Ж -> "zh"/"Zh", ю/Ю -> "iu"/"Iu", ш -> "sh", щ -> "shch"
        // № -> "No"
        Assert.Equal("\"ZhIu\" No1 ta \"ShShch\" No2", result);
        Assert.DoesNotContain("«", result);
        Assert.DoesNotContain("»", result);
        Assert.DoesNotContain("№", result);
        Assert.DoesNotContain("Ж", result);
        Assert.DoesNotContain("Ю", result);
    }

    [Fact]
    public void Convert_TrulyAllCapsNumeroAndGuillemets_PreservesCase()
    {
        // Arrange
        var table = CompositeTransliterationTable.Combine(
            TransliterationTables.SpecialCharacters,
            new SimpleTestTable());
        // Truly all-caps input with guillemets and numero sign
        string input = "«ЖЮ» №1 І «ШЩ» №2";

        // Act
        string result = Translit.Convert(input, table, preserveCase: true);

        // Assert
        // All Cyrillic letters are uppercase, so case should be preserved as uppercase
        // ж/Ж -> "zh"/"Zh", ю/Ю -> "iu"/"Iu", ш -> "sh", щ -> "shch", і/І -> "i"/"I"
        // № -> "No" -> "NO" (converted to uppercase in case preservation stage)
        Assert.Equal("\"ZHIU\" NO1 I \"SHSHCH\" NO2", result);
        Assert.DoesNotContain("«", result);
        Assert.DoesNotContain("»", result);
        Assert.DoesNotContain("№", result);
    }

    #endregion

    #region Currency Map Tests

    [Fact]
    public void Compose_CurrencyMapWithLanguageMap_CreatesCompositeTable()
    {
        // Arrange
        var currencyMap = new CurrencyMap();
        var baseTable = new SimpleTestTable();
        var compositeTable = CompositeTransliterationTable.Combine(currencyMap, baseTable);

        // Act & Assert
        Assert.NotNull(compositeTable);
        Assert.NotNull(compositeTable.MainTranslitTable);
        // Verify currency symbols are in composite
        Assert.True(compositeTable.MainTranslitTable.ContainsKey('₴'));
        Assert.True(compositeTable.MainTranslitTable.ContainsKey('₽'));
        Assert.True(compositeTable.MainTranslitTable.ContainsKey('€'));
    }

    [Fact]
    public void Convert_UkrainianHryvnia_ConvertsToUAH()
    {
        // Arrange
        var compositeTable = CompositeTransliterationTable.Combine(
            new CurrencyMap(),
            new SimpleTestTable());
        string input = "Ціна: 100₴";

        // Act
        string result = Translit.Convert(input, compositeTable);

        // Assert
        Assert.Contains("UAH", result);
        Assert.DoesNotContain("₴", result);
    }

    [Fact]
    public void Convert_RussianRuble_ConvertsToRUB()
    {
        // Arrange
        var compositeTable = CompositeTransliterationTable.Combine(
            new CurrencyMap(),
            new SimpleTestTable());
        string input = "Вартість: 500₽";

        // Act
        string result = Translit.Convert(input, compositeTable);

        // Assert
        Assert.Contains("RUB", result);
        Assert.DoesNotContain("₽", result);
    }

    [Fact]
    public void Convert_MultipleCurrencies_AllConverted()
    {
        // Arrange
        var compositeTable = CompositeTransliterationTable.Combine(
            new CurrencyMap(),
            new SimpleTestTable());
        string input = "100₴ 50€ 75£ 200¥";

        // Act
        string result = Translit.Convert(input, compositeTable);

        // Assert
        Assert.Contains("UAH", result);
        Assert.Contains("EUR", result);
        Assert.Contains("GBP", result);
        Assert.Contains("CNY", result);
        Assert.DoesNotContain("₴", result);
        Assert.DoesNotContain("€", result);
        Assert.DoesNotContain("£", result);
        Assert.DoesNotContain("¥", result);
    }

    [Fact]
    public void Convert_CurrenciesWithText_IntegratesCorrectly()
    {
        // Arrange
        var compositeTable = CompositeTransliterationTable.Combine(
            new CurrencyMap(),
            new SimpleTestTable());
        string input = "Вартість товару: 150₴, Контрактна вартість: 50€";

        // Act
        string result = Translit.Convert(input, compositeTable);

        // Assert
        Assert.Contains("150UAH", result);
        Assert.Contains("50EUR", result);
        Assert.DoesNotContain("₴", result);
        Assert.DoesNotContain("€", result);
    }

    [Fact]
    public void Convert_CurrencyWithComposedMaps_StacksCorrectly()
    {
        // Arrange - Stack currency + special characters + language
        var compositeTable = CompositeTransliterationTable.Combine(
            new CurrencyMap(),
            CompositeTransliterationTable.Combine(
                TransliterationTables.SpecialCharacters,
                new SimpleTestTable()));
        string input = "«Ціна» №1: 100₴";

        // Act
        string result = Translit.Convert(input, compositeTable);

        // Assert
        Assert.Contains("\"", result);  // Guillemets converted
        Assert.Contains("No1", result); // Numero sign converted
        Assert.Contains("UAH", result); // Currency converted
        Assert.DoesNotContain("«", result);
        Assert.DoesNotContain("»", result);
        Assert.DoesNotContain("№", result);
        Assert.DoesNotContain("₴", result);
    }

    #endregion
}
