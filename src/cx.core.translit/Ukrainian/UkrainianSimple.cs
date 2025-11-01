namespace cx.core.translit;

/// <summary>
/// Simplified Ukrainian transliteration table.
/// Borrowed from transliterate library by Artur Barseghyan.
///
/// This system provides a straightforward character-to-character mapping
/// without special cases or word-boundary rules.
/// </summary>
public class UkrainianSimple : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UkrainianSimple"/> class.
    /// </summary>
    public UkrainianSimple() : base(
        mainTable: new Dictionary<string, string>
        {
            // Vowels
            { "а", "a" },
            { "е", "e" },
            { "и", "y" },
            { "і", "i" },
            { "о", "o" },
            { "у", "u" },

            // Consonants
            { "б", "b" },
            { "в", "v" },
            { "г", "h" },
            { "ґ", "g" },
            { "д", "d" },
            { "ж", "zh" },
            { "з", "z" },
            { "й", "j" },    // Different from KMU: j instead of i
            { "к", "k" },
            { "л", "l" },
            { "м", "m" },
            { "н", "n" },
            { "п", "p" },
            { "р", "r" },
            { "с", "s" },
            { "т", "t" },
            { "ф", "f" },
            { "х", "kh" },
            { "ц", "ts" },
            { "ч", "ch" },
            { "ш", "sh" },
            { "щ", "shch" },

            // Complex vowels - always same (no word-boundary variants)
            { "є", "ye" },   // Always "ye" (not "ie" like KMU)
            { "ї", "yi" },   // Always "yi"
            { "ю", "ju" },   // Different from KMU: "ju" instead of "iu"
            { "я", "ja" },   // Different from KMU: "ja" instead of "ia"

            // Soft sign - becomes apostrophe (not deleted)
            { "ь", "'" }
        },
        specialCases: null,        // No special cases
        firstCharacters: null,     // No word-boundary rules
        deleteChars: null)         // Nothing to delete
    {
    }
}
