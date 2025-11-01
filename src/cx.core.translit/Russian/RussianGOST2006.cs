namespace cx.core.translit;

/// <summary>
/// Russian transliteration according to GOST R 52535.1-2006 standard.
/// Official Russian government standard for romanization.
/// </summary>
public class RussianGOST2006 : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RussianGOST2006"/> class.
    /// </summary>
    public RussianGOST2006() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "д", "d" },
            { "е", "e" }, { "ё", "e" }, { "ж", "zh" }, { "з", "z" }, { "и", "i" },
            { "й", "i" }, { "к", "k" }, { "л", "l" }, { "м", "m" }, { "н", "n" },
            { "о", "o" }, { "п", "p" }, { "р", "r" }, { "с", "s" }, { "т", "t" },
            { "у", "u" }, { "ф", "f" }, { "х", "kh" }, { "ц", "tc" }, { "ч", "ch" },
            { "ш", "sh" }, { "щ", "shch" }, { "ъ", "" }, { "ы", "y" }, { "ь", "" },
            { "э", "e" }, { "ю", "iu" }, { "я", "ia" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
