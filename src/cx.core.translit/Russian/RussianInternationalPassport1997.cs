namespace cx.core.translit;

/// <summary>
/// Russian transliteration according to the 1997 International Passport standard.
/// Standard system for Russian international travel documents.
/// </summary>
public class RussianInternationalPassport1997 : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RussianInternationalPassport1997"/> class.
    /// </summary>
    public RussianInternationalPassport1997() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "д", "d" },
            { "е", "e" }, { "ё", "e" }, { "ж", "zh" }, { "з", "z" }, { "и", "i" },
            { "й", "y" }, { "к", "k" }, { "л", "l" }, { "м", "m" }, { "н", "n" },
            { "о", "o" }, { "п", "p" }, { "р", "r" }, { "с", "s" }, { "т", "t" },
            { "у", "u" }, { "ф", "f" }, { "х", "kh" }, { "ц", "ts" }, { "ч", "ch" },
            { "ш", "sh" }, { "щ", "shch" }, { "ъ", "'" }, { "ы", "y" }, { "ь", "" },
            { "э", "e" }, { "ю", "yu" }, { "я", "ya" }
        },
        specialCases: new Dictionary<string, string>
        {
            { "ье", "'ye" }, { "Ье", "'Ye" },
            { "ьё", "'ye" }, { "Ьё", "'Ye" }
        },
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
