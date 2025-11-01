namespace cx.core.translit;

/// <summary>
/// Ukrainian transliteration according to the French system.
/// Adapts Ukrainian sounds to French phonetics.
/// </summary>
public class UkrainianFrench : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UkrainianFrench"/> class.
    /// </summary>
    public UkrainianFrench() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "h" }, { "ґ", "g" },
            { "д", "d" }, { "е", "e" }, { "є", "ie" }, { "ж", "j" }, { "з", "z" },
            { "и", "y" }, { "і", "i" }, { "ї", "ï" }, { "й", "y" }, { "к", "k" },
            { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" },
            { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "ou" }, { "ф", "f" },
            { "х", "kh" }, { "ц", "ts" }, { "ч", "tch" }, { "ш", "ch" }, { "щ", "chtch" },
            { "ь", "" }, { "ю", "iou" }, { "я", "ia" },
            // Apostrophes deleted
            { "'", "" }, { "\u2019", "" }, { "\u02BC", "" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
