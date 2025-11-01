namespace TranslitKit;

/// <summary>
/// Ukrainian transliteration according to the Scholarly (WWS) system.
/// Uses diacritics and special characters for precise phonetic representation.
/// </summary>
public class UkrainianWWS : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UkrainianWWS"/> class.
    /// </summary>
    public UkrainianWWS() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "h" }, { "ґ", "g" },
            { "д", "d" }, { "е", "e" }, { "є", "je" }, { "ж", "ž" }, { "з", "z" },
            { "и", "y" }, { "і", "i" }, { "ї", "ji" }, { "й", "j" }, { "к", "k" },
            { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" },
            { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "u" }, { "ф", "f" },
            { "х", "x" }, { "ц", "c" }, { "ч", "č" }, { "ш", "š" }, { "щ", "šč" },
            { "ь", "ʹ" }, { "ю", "ju" }, { "я", "ja" },
            // Apostrophes deleted
            { "'", "" }, { "\u2019", "" }, { "\u02BC", "" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
