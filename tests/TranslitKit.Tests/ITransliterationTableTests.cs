using System.Text.RegularExpressions;
using Xunit;

namespace TranslitKit.Tests;

/// <summary>
/// Tests for ITransliterationTable interface contract expectations.
/// Following TDD: Writing tests before implementation.
/// </summary>
public class ITransliterationTableTests
{
    /// <summary>
    /// Mock implementation of ITransliterationTable for testing.
    /// </summary>
    private class MockTransliterationTable : ITransliterationTable
    {
        public Dictionary<char, string> MainTranslitTable { get; set; } = new();
        public Dictionary<string, string>? SpecialCases { get; set; }
        public Dictionary<string, string>? FirstCharacters { get; set; }
        public Regex? DeletePattern { get; set; }
    }

    [Fact]
    public void ITransliterationTable_ShouldHaveMainTranslitTableProperty()
    {
        // Arrange & Act
        var table = new MockTransliterationTable
        {
            MainTranslitTable = new Dictionary<char, string>
            {
                { 'а', "a" },
                { 'б', "b" }
            }
        };

        // Assert
        Assert.NotNull(table.MainTranslitTable);
        Assert.Equal(2, table.MainTranslitTable.Count);
        Assert.Equal("a", table.MainTranslitTable['а']);
        Assert.Equal("b", table.MainTranslitTable['б']);
    }

    [Fact]
    public void ITransliterationTable_SpecialCases_CanBeNull()
    {
        // Arrange & Act
        var table = new MockTransliterationTable
        {
            MainTranslitTable = new Dictionary<char, string>(),
            SpecialCases = null
        };

        // Assert
        Assert.Null(table.SpecialCases);
    }

    [Fact]
    public void ITransliterationTable_SpecialCases_CanContainMappings()
    {
        // Arrange & Act
        var table = new MockTransliterationTable
        {
            MainTranslitTable = new Dictionary<char, string>(),
            SpecialCases = new Dictionary<string, string>
            {
                { "зг", "zgh" },
                { "ЗГ", "ZGh" }
            }
        };

        // Assert
        Assert.NotNull(table.SpecialCases);
        Assert.Equal(2, table.SpecialCases.Count);
        Assert.Equal("zgh", table.SpecialCases["зг"]);
    }

    [Fact]
    public void ITransliterationTable_FirstCharacters_CanBeNull()
    {
        // Arrange & Act
        var table = new MockTransliterationTable
        {
            MainTranslitTable = new Dictionary<char, string>(),
            FirstCharacters = null
        };

        // Assert
        Assert.Null(table.FirstCharacters);
    }

    [Fact]
    public void ITransliterationTable_FirstCharacters_CanContainMappings()
    {
        // Arrange & Act
        var table = new MockTransliterationTable
        {
            MainTranslitTable = new Dictionary<char, string>(),
            FirstCharacters = new Dictionary<string, string>
            {
                { "є", "ye" },
                { "ї", "yi" }
            }
        };

        // Assert
        Assert.NotNull(table.FirstCharacters);
        Assert.Equal(2, table.FirstCharacters.Count);
        Assert.Equal("ye", table.FirstCharacters["є"]);
    }

    [Fact]
    public void ITransliterationTable_DeletePattern_CanBeNull()
    {
        // Arrange & Act
        var table = new MockTransliterationTable
        {
            MainTranslitTable = new Dictionary<char, string>(),
            DeletePattern = null
        };

        // Assert
        Assert.Null(table.DeletePattern);
    }

    [Fact]
    public void ITransliterationTable_DeletePattern_CanBeRegex()
    {
        // Arrange & Act
        var pattern = new Regex(@"[ьЬ']");
        var table = new MockTransliterationTable
        {
            MainTranslitTable = new Dictionary<char, string>(),
            DeletePattern = pattern
        };

        // Assert
        Assert.NotNull(table.DeletePattern);
        Assert.Matches(table.DeletePattern, "ь");
        Assert.Matches(table.DeletePattern, "Ь");
        Assert.Matches(table.DeletePattern, "'");
    }

    [Fact]
    public void ITransliterationTable_MainTranslitTable_SupportsMultiCharacterOutput()
    {
        // Arrange & Act
        var table = new MockTransliterationTable
        {
            MainTranslitTable = new Dictionary<char, string>
            {
                { 'ж', "zh" },
                { 'ч', "ch" },
                { 'щ', "shch" }
            }
        };

        // Assert
        Assert.Equal("zh", table.MainTranslitTable['ж']);
        Assert.Equal("ch", table.MainTranslitTable['ч']);
        Assert.Equal("shch", table.MainTranslitTable['щ']);
    }
}
