using System.Text.RegularExpressions;

namespace cx.core.translit;

/// <summary>
/// Base class for all transliteration table implementations.
/// Handles common initialization logic including uppercase variant generation
/// and pattern compilation.
/// </summary>
public abstract class TransliterationTableBase : ITransliterationTable
{
    /// <inheritdoc/>
    public Dictionary<char, string> MainTranslitTable { get; }

    /// <inheritdoc/>
    public Dictionary<string, string>? SpecialCases { get; }

    /// <inheritdoc/>
    public Dictionary<string, string>? FirstCharacters { get; }

    /// <inheritdoc/>
    public Regex? DeletePattern { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TransliterationTableBase"/> class.
    /// </summary>
    /// <param name="mainTable">
    /// Dictionary mapping Cyrillic characters (as single-char strings) to Latin equivalents.
    /// Uppercase variants will be generated automatically.
    /// </param>
    /// <param name="specialCases">
    /// Optional dictionary for context-specific multi-character sequences
    /// (e.g., "зг" → "zgh"). Pass null if not needed.
    /// </param>
    /// <param name="firstCharacters">
    /// Optional dictionary for word-initial character variants
    /// (e.g., "є" → "ye" at word start). Pass null if not needed.
    /// </param>
    /// <param name="deleteChars">
    /// Optional array of characters to delete during transliteration
    /// (e.g., soft signs, apostrophes). Pass null if not needed.
    /// </param>
    protected TransliterationTableBase(
        Dictionary<string, string> mainTable,
        Dictionary<string, string>? specialCases = null,
        Dictionary<string, string>? firstCharacters = null,
        string[]? deleteChars = null)
    {
        // Build main transliteration table with uppercase variants
        MainTranslitTable = BuildMainTranslitTable(mainTable);

        // Store special cases and first characters as-is
        SpecialCases = specialCases;
        FirstCharacters = firstCharacters;

        // Build delete pattern if delete chars are provided
        DeletePattern = deleteChars != null && deleteChars.Length > 0
            ? BuildDeletePattern(deleteChars)
            : null;
    }

    /// <summary>
    /// Builds the main transliteration table from the input dictionary,
    /// adding uppercase variants automatically.
    /// </summary>
    /// <param name="mainTable">Input dictionary with string keys (single characters).</param>
    /// <returns>Dictionary with char keys ready for transliteration.</returns>
    private static Dictionary<char, string> BuildMainTranslitTable(Dictionary<string, string> mainTable)
    {
        var result = new Dictionary<char, string>();

        foreach (var kvp in mainTable)
        {
            if (string.IsNullOrEmpty(kvp.Key) || kvp.Key.Length != 1)
            {
                throw new ArgumentException(
                    $"Main table keys must be single characters. Invalid key: '{kvp.Key}'",
                    nameof(mainTable));
            }

            char key = kvp.Key[0];
            string value = kvp.Value;

            // Add the original mapping
            if (!result.ContainsKey(key))
            {
                result[key] = value;
            }

            // Generate uppercase variant if the key is lowercase
            if (char.IsLower(key))
            {
                char upperKey = char.ToUpper(key);
                if (!result.ContainsKey(upperKey))
                {
                    // Capitalize the value (first char uppercase, rest as-is)
                    string upperValue = CapitalizeFirst(value);
                    result[upperKey] = upperValue;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Capitalizes the first character of a string while leaving the rest unchanged.
    /// </summary>
    /// <param name="value">The string to capitalize.</param>
    /// <returns>String with first character uppercase.</returns>
    private static string CapitalizeFirst(string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        if (value.Length == 1)
            return value.ToUpper();

        return char.ToUpper(value[0]) + value.Substring(1);
    }

    /// <summary>
    /// Builds a regex pattern that matches any of the characters to be deleted.
    /// </summary>
    /// <param name="deleteChars">Array of character strings to delete.</param>
    /// <returns>Compiled regex pattern.</returns>
    private static Regex BuildDeletePattern(string[] deleteChars)
    {
        // Escape each character for regex and join with |
        var escapedChars = deleteChars.Select(Regex.Escape);
        var pattern = string.Join("|", escapedChars);

        // Use RegexOptions for better performance and Unicode support
        return new Regex(pattern, RegexOptions.Compiled | RegexOptions.Multiline);
    }
}
