namespace TranslitKit;

/// <summary>
/// Currency symbols transliteration map for converting currency symbols to their ISO 4217 codes.
/// This map can be stacked/composed with any language-specific transliteration table.
/// </summary>
public class CurrencyMap : TransliterationTableBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CurrencyMap"/> class.
    /// </summary>
    public CurrencyMap() : base(
        mainTable: new Dictionary<string, string>
        {
            // ISO currency symbols from Eastern Europe and post-Soviet states
            { "₴", "UAH" },    // Ukrainian Hryvnia
            { "₽", "RUB" },    // Russian Ruble
            { "₼", "AZN" },    // Azerbaijani Manat
            { "₾", "GEL" },    // Georgian Lari

            // Asian currencies
            { "₹", "INR" },    // Indian Rupee
            { "₨", "PKR" },    // Pakistani Rupee
            { "₩", "KRW" },    // South Korean Won
            { "¥", "CNY" },    // Chinese Yuan / Japanese Yen (context-dependent, using CNY as default)

            // Middle East and Africa
            { "₪", "ILS" },    // Israeli Shekel
            { "₦", "NGN" },    // Nigerian Naira
            { "₵", "GHS" },    // Ghanaian Cedi

            // Americas
            { "₡", "CRC" },    // Costa Rican Colón
            { "₲", "PYG" },    // Paraguayan Guaraní
            { "₱", "PHP" },    // Philippine Peso
            { "₸", "UAH" },    // Ukrainian Hryvnia (alternate symbol)

            // Turkish and other
            { "₺", "TRY" },    // Turkish Lira
            { "₿", "BTC" },    // Bitcoin

            // Common currency symbols
            { "$", "USD" },    // US Dollar
            { "€", "EUR" },    // Euro
            { "£", "GBP" },    // British Pound
            { "¢", "CENT" },   // Cent
            { "¤", "CURR" },   // Generic currency symbol
        },
        specialCases: null,
        firstCharacters: null,
        deleteChars: null)
    {
    }
}
