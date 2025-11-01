namespace cx.core.translit;

/// <summary>
/// Simple Russian transliteration system.
/// Basic romanization without complex rules.
/// </summary>
public class RussianSimple : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RussianSimple"/> class.
    /// </summary>
    public RussianSimple() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "д", "d" },
            { "е", "e" }, { "ё", "e" }, { "ж", "zh" }, { "з", "z" }, { "и", "i" },
            { "й", "j" }, { "к", "k" }, { "л", "l" }, { "м", "m" }, { "н", "n" },
            { "о", "o" }, { "п", "p" }, { "р", "r" }, { "с", "s" }, { "т", "t" },
            { "у", "u" }, { "ф", "f" }, { "х", "h" }, { "ц", "ts" }, { "ч", "ch" },
            { "ш", "sh" }, { "щ", "sch" }, { "ъ", "'" }, { "ы", "y" }, { "ь", "'" },
            { "э", "e" }, { "ю", "ju" }, { "я", "ja" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
