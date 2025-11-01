namespace cx.core.translit;

/// <summary>
/// Russian transliteration according to the 1997 International Passport standard (reduced variant).
/// Variant with additional compression rules for common endings.
/// </summary>
public class RussianInternationalPassport1997Reduced : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RussianInternationalPassport1997Reduced"/> class.
    /// </summary>
    public RussianInternationalPassport1997Reduced() : base(
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
            { "ьё", "'ye" }, { "Ьё", "'Ye" },
            { "ый", "y" }, { "Ый", "Y" },
            { "ий", "y" }, { "Ий", "Y" }
        },
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
