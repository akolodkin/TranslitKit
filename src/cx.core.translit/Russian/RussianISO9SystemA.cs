namespace cx.core.translit;

/// <summary>
/// Russian transliteration according to ISO 9:1995 System A.
/// Diacritical system with special characters for precise phonetic representation.
/// </summary>
public class RussianISO9SystemA : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RussianISO9SystemA"/> class.
    /// </summary>
    public RussianISO9SystemA() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "д", "d" },
            { "е", "e" }, { "ё", "ë" }, { "ж", "ž" }, { "з", "z" }, { "и", "i" },
            { "й", "j" }, { "к", "k" }, { "л", "l" }, { "м", "m" }, { "н", "n" },
            { "о", "o" }, { "п", "p" }, { "р", "r" }, { "с", "s" }, { "т", "t" },
            { "у", "u" }, { "ф", "f" }, { "х", "h" }, { "ц", "c" }, { "ч", "č" },
            { "ш", "š" }, { "щ", "ŝ" }, { "ъ", "″" }, { "ы", "y" }, { "ь", "′" },
            { "э", "è" }, { "ю", "û" }, { "я", "â" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
