namespace cx.core.translit;

/// <summary>
/// Russian transliteration according to ISO 9:1995 System B.
/// Digraph system with context-dependent ц handling.
/// </summary>
public class RussianISO9SystemB : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RussianISO9SystemB"/> class.
    /// </summary>
    public RussianISO9SystemB() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "д", "d" },
            { "е", "e" }, { "ё", "yo" }, { "ж", "zh" }, { "з", "z" }, { "и", "i" },
            { "й", "j" }, { "к", "k" }, { "л", "l" }, { "м", "m" }, { "н", "n" },
            { "о", "o" }, { "п", "p" }, { "р", "r" }, { "с", "s" }, { "т", "t" },
            { "у", "u" }, { "ф", "f" }, { "х", "x" }, { "ц", "cz" }, { "ч", "ch" },
            { "ш", "sh" }, { "щ", "shh" }, { "ъ", "''" }, { "ы", "y'" }, { "ь", "'" },
            { "э", "e'" }, { "ю", "yu" }, { "я", "ya" }
        },
        specialCases: new Dictionary<string, string>
        {
            { "це", "ce" }, { "Це", "Ce" },
            { "цэ", "ce'" }, { "Цэ", "Ce'" },
            { "ци", "ci" }, { "Ци", "Ci" },
            { "цё", "cyo" }, { "Цё", "Cyo" },
            { "цы", "cy'" }, { "Цы", "Cy'" },
            { "цю", "cyu" }, { "Цю", "Cyu" },
            { "ця", "cya" }, { "Ця", "Cya" },
            { "цй", "cj" }, { "Цй", "Cj" }
        },
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
