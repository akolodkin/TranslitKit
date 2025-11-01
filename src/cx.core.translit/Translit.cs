using System.Text;
using System.Text.RegularExpressions;

namespace TranslitKit;

/// <summary>
/// Main transliteration class providing static methods to convert Cyrillic text to Latin characters.
/// </summary>
public static class Translit
{
    /// <summary>
    /// Converts Cyrillic text to Latin characters using the specified transliteration table.
    /// Implements a 5-stage transliteration algorithm:
    /// 1. Delete unwanted characters (soft signs, apostrophes)
    /// 2. Apply special case patterns (context-specific sequences)
    /// 3. Apply first-character patterns (word-initial variants)
    /// 4. Main character-by-character transliteration
    /// 5. Preserve case if original was all uppercase
    /// </summary>
    /// <param name="source">The source text to transliterate. Can be null or empty.</param>
    /// <param name="table">The transliteration table to use.</param>
    /// <param name="preserveCase">
    /// If true and the source is all uppercase, the result will be converted to uppercase.
    /// Default is true.
    /// </param>
    /// <returns>Transliterated text in Latin characters.</returns>
    /// <exception cref="ArgumentNullException">Thrown when table is null.</exception>
    public static string Convert(string? source, ITransliterationTable table, bool preserveCase = true)
    {
        if (table == null)
            throw new ArgumentNullException(nameof(table));

        // Handle null or empty input
        if (string.IsNullOrEmpty(source))
            return string.Empty;

        // Check if source is all uppercase (for case preservation in Stage 5)
        bool sourceIsAllUpper = IsAllUpper(source);

        // When preserveCase is false, convert to lowercase first to get natural casing
        string result = preserveCase ? source : source.ToLower();

        // Stage 1: Delete unwanted characters (soft signs, apostrophes)
        if (table.DeletePattern != null)
        {
            result = table.DeletePattern.Replace(result, string.Empty);
        }

        // Stage 2: Apply special case patterns (context-specific multi-char sequences)
        if (table.SpecialCases != null && table.SpecialCases.Count > 0)
        {
            result = ApplySpecialCases(result, table.SpecialCases);
        }

        // Stage 3: Apply first-character patterns (word-initial variants)
        if (table.FirstCharacters != null && table.FirstCharacters.Count > 0)
        {
            result = ApplyFirstCharacters(result, table.FirstCharacters);
        }

        // Stage 4: Main transliteration (character-by-character)
        result = ApplyMainTransliteration(result, table.MainTranslitTable);

        // Stage 5: Preserve case if original was all uppercase
        if (sourceIsAllUpper && preserveCase)
        {
            result = result.ToUpper();
        }
        else if (!preserveCase && result.Length > 0)
        {
            // When preserveCase is false, ensure proper title casing (first letter uppercase)
            if (char.IsLower(result[0]))
            {
                result = char.ToUpper(result[0]) + result.Substring(1);
            }
        }

        return result;
    }

    /// <summary>
    /// Checks if a string is all uppercase (ignoring non-letter characters).
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <returns>True if all letter characters are uppercase, false otherwise.</returns>
    private static bool IsAllUpper(string text)
    {
        bool hasLetters = false;

        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                hasLetters = true;
                if (!char.IsUpper(c))
                    return false;
            }
        }

        return hasLetters; // Only return true if there were letters and all were uppercase
    }

    /// <summary>
    /// Applies special case replacements using string matching.
    /// Special cases are context-specific multi-character sequences that need
    /// special handling (e.g., "зг" → "zgh" in Ukrainian).
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="specialCases">Dictionary of special case mappings.</param>
    /// <returns>Text with special cases applied.</returns>
    private static string ApplySpecialCases(string text, Dictionary<string, string> specialCases)
    {
        // Build regex pattern from special case keys
        // Escape each key and join with |
        var escapedKeys = specialCases.Keys.Select(Regex.Escape);
        var pattern = string.Join("|", escapedKeys);

        if (string.IsNullOrEmpty(pattern))
            return text;

        var regex = new Regex(pattern, RegexOptions.Multiline);

        // Replace using the dictionary
        return regex.Replace(text, match => specialCases[match.Value]);
    }

    /// <summary>
    /// Applies first-character replacements at word boundaries.
    /// Some characters transliterate differently when they appear at the start
    /// of a word (e.g., Ukrainian "є" → "ye" initially, "ie" otherwise).
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="firstCharacters">Dictionary of first-character mappings.</param>
    /// <returns>Text with first-character rules applied.</returns>
    private static string ApplyFirstCharacters(string text, Dictionary<string, string> firstCharacters)
    {
        // Build regex pattern with word boundary anchor
        // Pattern: \b(key1|key2|key3)
        var escapedKeys = firstCharacters.Keys.Select(Regex.Escape);
        var pattern = @"\b(" + string.Join("|", escapedKeys) + ")";

        if (escapedKeys.Count() == 0)
            return text;

        var regex = new Regex(pattern, RegexOptions.Multiline);

        // Replace using the dictionary
        return regex.Replace(text, match => firstCharacters[match.Value]);
    }

    /// <summary>
    /// Applies the main transliteration table character-by-character.
    /// Each Cyrillic character is replaced with its Latin equivalent.
    /// Characters not in the table are left unchanged.
    /// </summary>
    /// <param name="text">The text to transliterate.</param>
    /// <param name="table">Dictionary mapping Cyrillic characters to Latin equivalents.</param>
    /// <returns>Transliterated text.</returns>
    private static string ApplyMainTransliteration(string text, Dictionary<char, string> table)
    {
        var result = new StringBuilder(text.Length * 2); // Estimate for multi-char replacements

        foreach (char c in text)
        {
            if (table.TryGetValue(c, out string? replacement))
            {
                result.Append(replacement);
            }
            else
            {
                // Character not in table - keep as-is
                result.Append(c);
            }
        }

        return result.ToString();
    }
}
