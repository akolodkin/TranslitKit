namespace TranslitKit;

/// <summary>
/// Ukrainian transliteration according to the ISO 9 standard.
/// International standard for Cyrillic transliteration.
/// </summary>
public class UkrainianISO9 : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UkrainianISO9"/> class.
    /// </summary>
    public UkrainianISO9() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "ґ", "g̀" },
            { "д", "d" }, { "е", "e" }, { "є", "ê" }, { "ж", "ž" }, { "з", "z" },
            { "и", "i" }, { "і", "ì" }, { "ї", "ï" }, { "й", "j" }, { "к", "k" },
            { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" },
            { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "u" }, { "ф", "f" },
            { "х", "h" }, { "ц", "c" }, { "ч", "č" }, { "ш", "š" }, { "щ", "ŝ" },
            { "ь", "′" }, { "ю", "û" }, { "я", "â" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
