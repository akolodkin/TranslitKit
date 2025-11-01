using System.Text.RegularExpressions;

namespace cx.core.translit;

/// <summary>
/// Defines a contract for transliteration tables that convert Cyrillic text to Latin characters.
/// </summary>
public interface ITransliterationTable
{
    /// <summary>
    /// Gets the main transliteration mapping from Cyrillic characters to Latin equivalents.
    /// Each character maps to one or more Latin characters (e.g., 'ж' → "zh").
    /// </summary>
    Dictionary<char, string> MainTranslitTable { get; }

    /// <summary>
    /// Gets optional special case mappings for context-specific multi-character sequences.
    /// Used for combinations that require special handling (e.g., "зг" → "zgh" in Ukrainian).
    /// Returns null if no special cases are defined for this table.
    /// </summary>
    Dictionary<string, string>? SpecialCases { get; }

    /// <summary>
    /// Gets optional mappings for characters at word boundaries (word-initial position).
    /// Some characters transliterate differently when they appear at the start of a word
    /// (e.g., Ukrainian "є" → "ye" initially, "ie" otherwise).
    /// Returns null if no first-character rules are defined for this table.
    /// </summary>
    Dictionary<string, string>? FirstCharacters { get; }

    /// <summary>
    /// Gets optional regex pattern for characters that should be deleted from the text
    /// before transliteration (e.g., soft signs, apostrophes in some systems).
    /// Returns null if no deletion rules are defined for this table.
    /// </summary>
    Regex? DeletePattern { get; }
}
