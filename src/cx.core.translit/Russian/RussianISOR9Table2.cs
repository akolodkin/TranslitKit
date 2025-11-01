namespace cx.core.translit;

/// <summary>
/// Russian transliteration according to ISO/R 9:1968 Table 2.
/// Older ISO standard for Cyrillic romanization.
/// </summary>
public class RussianISOR9Table2 : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RussianISOR9Table2"/> class.
    /// </summary>
    public RussianISOR9Table2() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "д", "d" },
            { "е", "e" }, { "ё", "jo" }, { "ж", "zh" }, { "з", "z" }, { "и", "i" },
            { "й", "jj" }, { "к", "k" }, { "л", "l" }, { "м", "m" }, { "н", "n" },
            { "о", "o" }, { "п", "p" }, { "р", "r" }, { "с", "s" }, { "т", "t" },
            { "у", "u" }, { "ф", "f" }, { "х", "kh" }, { "ц", "c" }, { "ч", "ch" },
            { "ш", "sh" }, { "щ", "shh" }, { "ъ", "″" }, { "ы", "y" }, { "ь", "′" },
            { "э", "eh" }, { "ю", "ju" }, { "я", "ja" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
