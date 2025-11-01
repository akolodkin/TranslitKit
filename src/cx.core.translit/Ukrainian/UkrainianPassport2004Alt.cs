namespace cx.core.translit;

/// <summary>
/// Ukrainian transliteration according to the 2004 Passport alternative standard.
/// Alternative transliteration system for international passports.
/// </summary>
public class UkrainianPassport2004Alt : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UkrainianPassport2004Alt"/> class.
    /// </summary>
    public UkrainianPassport2004Alt() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "ґ", "h" },
            { "д", "d" }, { "е", "e" }, { "є", "ie" }, { "ж", "j" }, { "з", "z" },
            { "и", "y" }, { "і", "i" }, { "ї", "i" }, { "й", "i" }, { "к", "c" },
            { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" },
            { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "u" }, { "ф", "f" },
            { "х", "kh" }, { "ц", "ts" }, { "ч", "ch" }, { "ш", "sh" }, { "щ", "shch" },
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
