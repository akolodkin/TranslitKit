namespace TranslitKit;

/// <summary>
/// Composable transliteration table that stacks/layers two transliteration tables together.
/// Applies the first table, then applies the second table to the result.
/// This allows combining special character maps with language-specific transliteration tables.
/// </summary>
public class CompositeTransliterationTable : ITransliterationTable
{
    private readonly ITransliterationTable _firstTable;
    private readonly ITransliterationTable _secondTable;
    private readonly Dictionary<char, string> _composedMainTable;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeTransliterationTable"/> class.
    /// </summary>
    /// <param name="firstTable">The first transliteration table to apply.</param>
    /// <param name="secondTable">The second transliteration table to apply (composed on top of first).</param>
    /// <exception cref="ArgumentNullException">Thrown when either table is null.</exception>
    public CompositeTransliterationTable(ITransliterationTable firstTable, ITransliterationTable secondTable)
    {
        _firstTable = firstTable ?? throw new ArgumentNullException(nameof(firstTable));
        _secondTable = secondTable ?? throw new ArgumentNullException(nameof(secondTable));

        // Compose the main transliteration tables
        _composedMainTable = ComposeMainTables(_firstTable.MainTranslitTable, _secondTable.MainTranslitTable);
    }

    /// <summary>
    /// Combines two transliteration tables by composing them together.
    /// The first table is applied, then the second table is applied on top.
    /// </summary>
    /// <param name="firstTable">The first transliteration table to apply.</param>
    /// <param name="secondTable">The second transliteration table to apply (composed on top of first).</param>
    /// <returns>A composite transliteration table combining both tables.</returns>
    /// <exception cref="ArgumentNullException">Thrown when either table is null.</exception>
    public static ITransliterationTable Combine(ITransliterationTable firstTable, ITransliterationTable secondTable)
    {
        return new CompositeTransliterationTable(firstTable, secondTable);
    }

    /// <inheritdoc/>
    public Dictionary<char, string> MainTranslitTable => _composedMainTable;

    /// <inheritdoc/>
    public Dictionary<string, string>? SpecialCases
    {
        get
        {
            // Combine special cases from both tables
            // Second table's special cases take precedence
            var combined = new Dictionary<string, string>();

            if (_firstTable.SpecialCases != null)
            {
                foreach (var kvp in _firstTable.SpecialCases)
                {
                    combined[kvp.Key] = kvp.Value;
                }
            }

            if (_secondTable.SpecialCases != null)
            {
                foreach (var kvp in _secondTable.SpecialCases)
                {
                    combined[kvp.Key] = kvp.Value;
                }
            }

            return combined.Count > 0 ? combined : null;
        }
    }

    /// <inheritdoc/>
    public Dictionary<string, string>? FirstCharacters
    {
        get
        {
            // Combine first characters from both tables
            // Second table's first characters take precedence
            var combined = new Dictionary<string, string>();

            if (_firstTable.FirstCharacters != null)
            {
                foreach (var kvp in _firstTable.FirstCharacters)
                {
                    combined[kvp.Key] = kvp.Value;
                }
            }

            if (_secondTable.FirstCharacters != null)
            {
                foreach (var kvp in _secondTable.FirstCharacters)
                {
                    combined[kvp.Key] = kvp.Value;
                }
            }

            return combined.Count > 0 ? combined : null;
        }
    }

    /// <inheritdoc/>
    public System.Text.RegularExpressions.Regex? DeletePattern
    {
        get
        {
            // Use second table's delete pattern if available, otherwise first
            // We can't easily merge regex patterns, so we prioritize
            return _secondTable.DeletePattern ?? _firstTable.DeletePattern;
        }
    }

    /// <summary>
    /// Composes two main transliteration tables by merging their character mappings.
    /// The second table's mappings take precedence over the first table's mappings.
    /// </summary>
    private static Dictionary<char, string> ComposeMainTables(
        Dictionary<char, string> firstTable,
        Dictionary<char, string> secondTable)
    {
        var composed = new Dictionary<char, string>(firstTable);

        // Second table mappings override first table mappings
        foreach (var kvp in secondTable)
        {
            composed[kvp.Key] = kvp.Value;
        }

        return composed;
    }
}
