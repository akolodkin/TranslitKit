namespace cx.core.translit;

/// <summary>
/// Ukrainian transliteration according to the 1996 National standard.
/// Earlier version of the official Ukrainian romanization system.
/// </summary>
public class UkrainianNational1996 : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UkrainianNational1996"/> class.
    /// </summary>
    public UkrainianNational1996() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "h" }, { "ґ", "g" },
            { "д", "d" }, { "е", "e" }, { "є", "ie" }, { "ж", "zh" }, { "з", "z" },
            { "и", "y" }, { "і", "i" }, { "ї", "i" }, { "й", "i" }, { "к", "k" },
            { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" },
            { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "u" }, { "ф", "f" },
            { "х", "kh" }, { "ц", "ts" }, { "ч", "ch" }, { "ш", "sh" }, { "щ", "sch" },
            { "ь", "'" }, { "ю", "iu" }, { "я", "ia" }
        },
        specialCases: new Dictionary<string, string>
        {
            { "зг", "zgh" },
            { "Зг", "Zgh" },
            { "ЗГ", "ZGh" }
        },
        firstCharacters: new Dictionary<string, string>
        {
            { "є", "ye" }, { "Є", "Ye" },
            { "ї", "yi" }, { "Ї", "Yi" },
            { "й", "y" }, { "Й", "Y" },
            { "ю", "yu" }, { "Ю", "Yu" },
            { "я", "ya" }, { "Я", "Ya" }
        },
        deleteChars: null)
    {
    }
}
