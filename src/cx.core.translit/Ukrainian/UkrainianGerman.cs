namespace cx.core.translit;

/// <summary>
/// Ukrainian transliteration according to the German (Duden) system.
/// Adapts Ukrainian sounds to German phonetics.
/// </summary>
public class UkrainianGerman : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UkrainianGerman"/> class.
    /// </summary>
    public UkrainianGerman() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "w" }, { "г", "h" }, { "ґ", "g" },
            { "д", "d" }, { "е", "e" }, { "є", "je" }, { "ж", "sh" }, { "з", "s" },
            { "и", "y" }, { "і", "i" }, { "ї", "ji" }, { "й", "j" }, { "к", "k" },
            { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" },
            { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "u" }, { "ф", "f" },
            { "х", "ch" }, { "ц", "z" }, { "ч", "tsch" }, { "ш", "sch" }, { "щ", "schtsch" },
            { "ь", "" }, { "ю", "ju" }, { "я", "ja" },
            // Apostrophes deleted
            { "'", "" }, { "\u2019", "" }, { "\u02BC", "" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
