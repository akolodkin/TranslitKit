namespace cx.core.translit;

/// <summary>
/// Ukrainian transliteration according to GOST 7.79-2000 (1986 variant).
/// Improved Soviet-era romanization system with diacritics.
/// </summary>
public class UkrainianGOST1986 : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UkrainianGOST1986"/> class.
    /// </summary>
    public UkrainianGOST1986() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "ґ", "g" },
            { "д", "d" }, { "е", "e" }, { "є", "je" }, { "ж", "ž" }, { "з", "z" },
            { "и", "i" }, { "і", "i" }, { "ї", "i" }, { "й", "j" }, { "к", "k" },
            { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" },
            { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "u" }, { "ф", "f" },
            { "х", "h" }, { "ц", "c" }, { "ч", "č" }, { "ш", "š" }, { "щ", "šč" },
            { "ь", "'" }, { "ю", "ju" }, { "я", "ja" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: new[] { "'", "\u2019", "\u02BC" })
    {
    }
}
