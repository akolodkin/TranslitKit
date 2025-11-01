namespace TranslitKit;

/// <summary>
/// Russian transliteration for driver's licenses.
/// Special rules for ё handling and soft sign combinations.
/// </summary>
public class RussianDriverLicense : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RussianDriverLicense"/> class.
    /// </summary>
    public RussianDriverLicense() : base(
        mainTable: new Dictionary<string, string>
        {
            { "а", "a" }, { "б", "b" }, { "в", "v" }, { "г", "g" }, { "д", "d" },
            { "е", "e" }, { "ё", "ye" }, { "ж", "zh" }, { "з", "z" }, { "и", "i" },
            { "й", "y" }, { "к", "k" }, { "л", "l" }, { "м", "m" }, { "н", "n" },
            { "о", "o" }, { "п", "p" }, { "р", "r" }, { "с", "s" }, { "т", "t" },
            { "у", "u" }, { "ф", "f" }, { "х", "kh" }, { "ц", "ts" }, { "ч", "ch" },
            { "ш", "sh" }, { "щ", "shch" }, { "ъ", "'" }, { "ы", "y" }, { "ь", "'" },
            { "э", "e" }, { "ю", "yu" }, { "я", "ya" }
        },
        specialCases: new Dictionary<string, string>
        {
            { "ье", "'ye" }, { "Ье", "'Ye" },
            { "ъе", "'ye" }, { "Ъе", "'Ye" },
            { "ьё", "'yo" }, { "Ьё", "'Yo" },
            { "ъё", "'yo" }, { "Ъё", "'Yo" },
            { "чё", "che" }, { "Чё", "Che" },
            { "шё", "she" }, { "Шё", "She" },
            { "щё", "shche" }, { "Щё", "Shche" },
            { "жё", "zhe" }, { "Жё", "Zhe" },
            { "ьи", "'yi" }, { "Ьи", "'Yi" }
        },
        firstCharacters: new Dictionary<string, string>
        {
            { "е", "ye" }, { "Е", "Ye" },
            { "ё", "yo" }, { "Ё", "Yo" }
        },
        deleteChars: null)
    {
    }
}
