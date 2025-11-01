namespace cx.core.translit;

/// <summary>
/// Ukrainian transliteration according to the BGN/PCGN romanization system.
/// Board on Geographic Names / Permanent Committee on Geographical Names.
/// </summary>
public class UkrainianBGN : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UkrainianBGN"/> class.
    /// </summary>
    public UkrainianBGN() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "h" }, { "ґ", "g" },
            { "д", "d" }, { "е", "e" }, { "є", "ye" }, { "ж", "zh" }, { "з", "z" },
            { "и", "y" }, { "і", "i" }, { "ї", "yi" }, { "й", "y" }, { "к", "k" },
            { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" },
            { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "u" }, { "ф", "f" },
            { "х", "kh" }, { "ц", "ts" }, { "ч", "ch" }, { "ш", "sh" }, { "щ", "shch" },
            { "ь", "'" }, { "ю", "yu" }, { "я", "ya" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
