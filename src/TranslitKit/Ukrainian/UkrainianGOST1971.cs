namespace TranslitKit;

/// <summary>
/// Ukrainian transliteration according to GOST 16876-71 (1971) standard.
/// Soviet-era romanization system.
/// </summary>
public class UkrainianGOST1971 : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UkrainianGOST1971"/> class.
    /// </summary>
    public UkrainianGOST1971() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "ґ", "g" },
            { "д", "d" }, { "е", "e" }, { "є", "je" }, { "ж", "zh" }, { "з", "z" },
            { "и", "i" }, { "і", "i" }, { "ї", "ji" }, { "й", "j" }, { "к", "k" },
            { "л", "l" }, { "м", "m" }, { "н", "n" }, { "о", "o" }, { "п", "p" },
            { "р", "r" }, { "с", "s" }, { "т", "t" }, { "у", "u" }, { "ф", "f" },
            { "х", "kh" }, { "ц", "c" }, { "ч", "ch" }, { "ш", "sh" }, { "щ", "shh" },
            { "ь", "'" }, { "ю", "ju" }, { "я", "ja" }
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
