namespace TranslitKit;

/// <summary>
/// Special characters transliteration map for Unicode characters like guillemets and numero sign.
/// This map can be stacked/composed with any language-specific transliteration table.
/// </summary>
public class SpecialCharactersMap : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SpecialCharactersMap"/> class.
    /// </summary>
    public SpecialCharactersMap() : base(
        mainTable: new Dictionary<string, string>
        {
            // Guillemets (French quotation marks)
            { "«", "\"" },  // Left-pointing double angle quotation mark
            { "»", "\"" },  // Right-pointing double angle quotation mark

            // Numero sign
            { "№", "No" }   // Numero sign
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
