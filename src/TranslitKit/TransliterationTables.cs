namespace TranslitKit;

/// <summary>
/// Provides access to all available transliteration tables organized by language.
/// Also provides methods for composing/stacking transliteration tables.
/// </summary>
public static class TransliterationTables
{
    /// <summary>
    /// Gets the special characters transliteration map (guillemets and numero sign).
    /// This map can be stacked with any language-specific transliteration table using
    /// <see cref="CompositeTransliterationTable.Combine(ITransliterationTable, ITransliterationTable)"/>.
    /// </summary>
    public static ITransliterationTable SpecialCharacters { get; } = new SpecialCharactersMap();

    /// <summary>
    /// Gets all Ukrainian transliteration tables (13 systems).
    /// </summary>
    public static IReadOnlyList<ITransliterationTable> AllUkrainian { get; } = new List<ITransliterationTable>
    {
        new UkrainianKMU(),
        new UkrainianSimple(),
        new UkrainianWWS(),
        new UkrainianBritish(),
        new UkrainianBGN(),
        new UkrainianISO9(),
        new UkrainianFrench(),
        new UkrainianGerman(),
        new UkrainianGOST1971(),
        new UkrainianGOST1986(),
        new UkrainianPassport2007(),
        new UkrainianNational1996(),
        new UkrainianPassport2004Alt()
    }.AsReadOnly();

    /// <summary>
    /// Gets all Russian transliteration tables (10 systems).
    /// </summary>
    public static IReadOnlyList<ITransliterationTable> AllRussian { get; } = new List<ITransliterationTable>
    {
        new RussianGOST2006(),
        new RussianSimple(),
        new RussianICAO(),
        new RussianTelegram(),
        new RussianInternationalPassport1997(),
        new RussianInternationalPassport1997Reduced(),
        new RussianDriverLicense(),
        new RussianISO9SystemA(),
        new RussianISO9SystemB(),
        new RussianISOR9Table2()
    }.AsReadOnly();

    /// <summary>
    /// Gets all transliteration tables for both Ukrainian and Russian (23 systems total).
    /// </summary>
    public static IReadOnlyList<ITransliterationTable> AllTransliterations { get; } =
        AllUkrainian.Concat(AllRussian).ToList().AsReadOnly();
}
