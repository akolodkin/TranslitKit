namespace TranslitKit;

/// <summary>
/// Ukrainian transliteration according to the 2007 Passport standard.
/// Used for international passports issued before 2010.
/// </summary>
public class UkrainianPassport2007 : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UkrainianPassport2007"/> class.
    /// </summary>
    public UkrainianPassport2007() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "ґ", "g" },
            { "д", "d" }, { "е", "e" }, { "є", "ie" }, { "ж", "zh" }, { "з", "z" },
            { "и", "y" }, { "і", "i" }, { "ї", "i" }, { "й", "i" }, { "к", "k" },
            { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" },
            { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "u" }, { "ф", "f" },
            { "х", "kh" }, { "ц", "ts" }, { "ч", "ch" }, { "ш", "sh" }, { "щ", "shch" },
            { "ь", "" }, { "ю", "iu" }, { "я", "ia" },
            // Apostrophes deleted
            { "'", "" }, { "\u2019", "" }, { "\u02BC", "" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
