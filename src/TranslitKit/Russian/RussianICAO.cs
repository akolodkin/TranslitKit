namespace TranslitKit;

/// <summary>
/// Russian transliteration according to ICAO standard (2013).
/// International Civil Aviation Organization standard for travel documents.
/// </summary>
public class RussianICAO : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RussianICAO"/> class.
    /// </summary>
    public RussianICAO() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "д", "d" },
            { "е", "e" }, { "ё", "e" }, { "ж", "zh" }, { "з", "z" }, { "и", "i" },
            { "й", "i" }, { "к", "k" }, { "л", "l" }, { "м", "m" }, { "н", "n" },
            { "о", "o" }, { "п", "p" }, { "р", "r" }, { "с", "s" }, { "т", "t" },
            { "у", "u" }, { "ф", "f" }, { "х", "kh" }, { "ц", "ts" }, { "ч", "ch" },
            { "ш", "sh" }, { "щ", "shch" }, { "ъ", "ie" }, { "ы", "y" }, { "ь", "" },
            { "э", "e" }, { "ю", "iu" }, { "я", "ia" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
