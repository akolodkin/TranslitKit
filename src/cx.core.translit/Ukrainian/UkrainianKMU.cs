namespace TranslitKit;

/// <summary>
/// Ukrainian transliteration table based on the KMU (Cabinet of Ministers of Ukraine)
/// National system from 2010. This is the most recent officially approved standard
/// and serves as the default Ukrainian transliteration system.
///
/// Resolution of the Cabinet of Ministers of Ukraine №55 from January 27, 2010.
/// </summary>
public class UkrainianKMU : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UkrainianKMU"/> class.
    /// </summary>
    public UkrainianKMU() : base(
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
            { "й", "i" },
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

            // Complex vowels
            { "є", "ie" },
            { "ї", "i" },
            { "ю", "iu" },
            { "я", "ia" }
        },
        specialCases: new Dictionary<string, string>
        {
            // Special combination: зг → zgh
            { "зг", "zgh" },
            { "Зг", "Zgh" },
            { "ЗГ", "ZGh" }
        },
        firstCharacters: new Dictionary<string, string>
        {
            // Characters that transliterate differently at word boundaries
            { "є", "ye" },
            { "Є", "Ye" },
            { "ї", "yi" },
            { "Ї", "Yi" },
            { "й", "y" },
            { "Й", "Y" },
            { "ю", "yu" },
            { "Ю", "Yu" },
            { "я", "ya" },
            { "Я", "Ya" }
        },
        deleteChars: new[]
        {
            // Soft sign
            "ь",
            "Ь",
            // Apostrophe variants
            "'",      // U+0027 ASCII apostrophe
            "\u2019", // U+2019 Right single quotation mark
            "\u02BC"  // U+02BC Modifier letter apostrophe
        })
    {
    }
}
