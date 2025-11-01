using System.Text.RegularExpressions;
using Xunit;

namespace TranslitKit.Tests;

/// <summary>
/// Tests for TransliterationTableBase abstract class.
/// Following TDD: Writing tests before implementation.
/// </summary>
public class TransliterationTableBaseTests
{
    /// <summary>
    /// Concrete test implementation of TransliterationTableBase.
    /// </summary>
    private class TestTransliterationTable : TransliterationTableBase
    {
        public TestTransliterationTable(
            Dictionary<string, string> mainTable,
            Dictionary<string, string>? specialCases = null,
            Dictionary<string, string>? firstCharacters = null,
            string[]? deleteChars = null)
            : base(mainTable, specialCases, firstCharacters, deleteChars)
        {
        }
    }

    [Fact]
    public void TransliterationTableBase_Constructor_BuildsMainTranslitTable()
    {
        // Arrange
        var mainTable = new Dictionary<string, string>
        {
            { "а", "a" },
            { "б", "b" },
            { "А", "A" },
            { "Б", "B" }
        };

        // Act
        var table = new TestTransliterationTable(mainTable);

        // Assert
        Assert.NotNull(table.MainTranslitTable);
        Assert.Equal(4, table.MainTranslitTable.Count);
        Assert.Equal("a", table.MainTranslitTable['а']);
        Assert.Equal("b", table.MainTranslitTable['б']);
        Assert.Equal("A", table.MainTranslitTable['А']);
        Assert.Equal("B", table.MainTranslitTable['Б']);
    }

    [Fact]
    public void TransliterationTableBase_Constructor_AddsUppercaseVariants()
    {
        // Arrange
        var mainTable = new Dictionary<string, string>
        {
            { "а", "a" },
            { "ж", "zh" }
        };

        // Act
        var table = new TestTransliterationTable(mainTable);

        // Assert - should have both lowercase and uppercase
        Assert.Equal(4, table.MainTranslitTable.Count);
        Assert.Equal("a", table.MainTranslitTable['а']);
        Assert.Equal("A", table.MainTranslitTable['А']);
        Assert.Equal("zh", table.MainTranslitTable['ж']);
        Assert.Equal("Zh", table.MainTranslitTable['Ж']); // Capitalized multi-char
    }

    [Fact]
    public void TransliterationTableBase_SpecialCases_CanBeNull()
    {
        // Arrange & Act
        var mainTable = new Dictionary<string, string> { { "а", "a" } };
        var table = new TestTransliterationTable(mainTable, specialCases: null);

        // Assert
        Assert.Null(table.SpecialCases);
    }

    [Fact]
    public void TransliterationTableBase_SpecialCases_StoresCorrectly()
    {
        // Arrange
        var mainTable = new Dictionary<string, string> { { "а", "a" } };
        var specialCases = new Dictionary<string, string>
        {
            { "зг", "zgh" },
            { "ЗГ", "ZGh" }
        };

        // Act
        var table = new TestTransliterationTable(mainTable, specialCases: specialCases);

        // Assert
        Assert.NotNull(table.SpecialCases);
        Assert.Equal(2, table.SpecialCases.Count);
        Assert.Equal("zgh", table.SpecialCases["зг"]);
        Assert.Equal("ZGh", table.SpecialCases["ЗГ"]);
    }

    [Fact]
    public void TransliterationTableBase_FirstCharacters_CanBeNull()
    {
        // Arrange & Act
        var mainTable = new Dictionary<string, string> { { "а", "a" } };
        var table = new TestTransliterationTable(mainTable, firstCharacters: null);

        // Assert
        Assert.Null(table.FirstCharacters);
    }

    [Fact]
    public void TransliterationTableBase_FirstCharacters_StoresCorrectly()
    {
        // Arrange
        var mainTable = new Dictionary<string, string> { { "є", "ie" } };
        var firstCharacters = new Dictionary<string, string>
        {
            { "є", "ye" },
            { "ї", "yi" }
        };

        // Act
        var table = new TestTransliterationTable(mainTable, firstCharacters: firstCharacters);

        // Assert
        Assert.NotNull(table.FirstCharacters);
        Assert.Equal(2, table.FirstCharacters.Count);
        Assert.Equal("ye", table.FirstCharacters["є"]);
        Assert.Equal("yi", table.FirstCharacters["ї"]);
    }

    [Fact]
    public void TransliterationTableBase_DeletePattern_NullWhenNoDeleteChars()
    {
        // Arrange & Act
        var mainTable = new Dictionary<string, string> { { "а", "a" } };
        var table = new TestTransliterationTable(mainTable, deleteChars: null);

        // Assert
        Assert.Null(table.DeletePattern);
    }

    [Fact]
    public void TransliterationTableBase_DeletePattern_CreatesRegexFromDeleteChars()
    {
        // Arrange
        var mainTable = new Dictionary<string, string> { { "а", "a" } };
        var deleteChars = new[] { "ь", "Ь", "'" };

        // Act
        var table = new TestTransliterationTable(mainTable, deleteChars: deleteChars);

        // Assert
        Assert.NotNull(table.DeletePattern);
        Assert.Matches(table.DeletePattern, "ь");
        Assert.Matches(table.DeletePattern, "Ь");
        Assert.Matches(table.DeletePattern, "'");
        Assert.DoesNotMatch(table.DeletePattern, "a");
    }

    [Fact]
    public void TransliterationTableBase_DeletePattern_HandlesUnicodeApostrophes()
    {
        // Arrange
        var mainTable = new Dictionary<string, string> { { "а", "a" } };
        var deleteChars = new[]
        {
            "'",      // ASCII apostrophe U+0027
            "\u2019", // Right single quotation mark
            "\u02BC"  // Modifier letter apostrophe
        };

        // Act
        var table = new TestTransliterationTable(mainTable, deleteChars: deleteChars);

        // Assert
        Assert.NotNull(table.DeletePattern);
        Assert.Matches(table.DeletePattern, "'");
        Assert.Matches(table.DeletePattern, "\u2019");
        Assert.Matches(table.DeletePattern, "\u02BC");
    }

    [Fact]
    public void TransliterationTableBase_MainTranslitTable_HandlesMultiCharacterOutput()
    {
        // Arrange
        var mainTable = new Dictionary<string, string>
        {
            { "ж", "zh" },
            { "ч", "ch" },
            { "щ", "shch" }
        };

        // Act
        var table = new TestTransliterationTable(mainTable);

        // Assert
        Assert.Equal("zh", table.MainTranslitTable['ж']);
        Assert.Equal("ch", table.MainTranslitTable['ч']);
        Assert.Equal("shch", table.MainTranslitTable['щ']);
        // Check uppercase variants
        Assert.Equal("Zh", table.MainTranslitTable['Ж']);
        Assert.Equal("Ch", table.MainTranslitTable['Ч']);
        Assert.Equal("Shch", table.MainTranslitTable['Щ']);
    }

    [Fact]
    public void TransliterationTableBase_MainTranslitTable_PreservesExistingUppercase()
    {
        // Arrange - when both cases are explicitly provided
        var mainTable = new Dictionary<string, string>
        {
            { "а", "a" },
            { "А", "A" }
        };

        // Act
        var table = new TestTransliterationTable(mainTable);

        // Assert - should not duplicate
        Assert.Equal(2, table.MainTranslitTable.Count);
        Assert.Equal("a", table.MainTranslitTable['а']);
        Assert.Equal("A", table.MainTranslitTable['А']);
    }
}
