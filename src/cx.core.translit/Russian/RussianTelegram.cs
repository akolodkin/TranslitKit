namespace cx.core.translit;

/// <summary>
/// Russian transliteration for telegraph communications.
/// Simplified system optimized for telegram transmission.
/// </summary>
public class RussianTelegram : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RussianTelegram"/> class.
    /// </summary>
    public RussianTelegram() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "д", "d" },
            { "е", "e" }, { "ё", "e" }, { "ж", "j" }, { "з", "z" }, { "и", "i" },
            { "й", "i" }, { "к", "k" }, { "л", "l" }, { "м", "m" }, { "н", "n" },
            { "о", "o" }, { "п", "p" }, { "р", "r" }, { "с", "s" }, { "т", "t" },
            { "у", "u" }, { "ф", "f" }, { "х", "h" }, { "ц", "c" }, { "ч", "ch" },
            { "ш", "sh" }, { "щ", "sc" }, { "ъ", "" }, { "ы", "y" }, { "ь", "" },
            { "э", "e" }, { "ю", "iu" }, { "я", "ia" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
