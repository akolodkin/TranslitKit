namespace TranslitKit;

/// <summary>
/// Ukrainian transliteration according to the British Standard system.
/// Uses macrons and breves for specific characters.
/// </summary>
public class UkrainianBritish : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UkrainianBritish"/> class.
    /// </summary>
    public UkrainianBritish() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "h" }, { "ґ", "g" },
            { "д", "d" }, { "е", "e" }, { "є", "ye" }, { "ж", "zh" }, { "з", "z" },
            { "и", "ȳ" }, { "і", "i" }, { "ї", "yi" }, { "й", "ĭ" }, { "к", "k" },
            { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" },
            { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "u" }, { "ф", "f" },
            { "х", "kh" }, { "ц", "ts" }, { "ч", "ch" }, { "ш", "sh" }, { "щ", "shch" },
            { "ь", "" }, { "ю", "yu" }, { "я", "ya" },
            // Apostrophes deleted
            { "'", "" }, { "\u2019", "" }, { "\u02BC", "" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
