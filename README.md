# TranslitKit

[![NuGet](https://img.shields.io/nuget/v/TranslitKit.svg)](https://www.nuget.org/packages/TranslitKit/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A .NET library for transliterating Ukrainian and Russian text from Cyrillic to Latin characters. This is a C# port of the Python [translit-ua](https://github.com/dchaplinsky/translit-ua) library.

## Features

- **13 Ukrainian transliteration systems** including:
  - UkrainianKMU (National 2010 - default)
  - UkrainianSimple, UkrainianWWS, UkrainianBritish
  - UkrainianBGN, UkrainianISO9, UkrainianFrench
  - UkrainianGerman, UkrainianGOST1971, UkrainianGOST1986
  - UkrainianPassport2007, UkrainianNational1996, UkrainianPassport2004Alt

- **10 Russian transliteration systems** including:
  - RussianGOST2006 (GOST 2006)
  - RussianSimple, RussianICAO, RussianTelegram
  - RussianInternationalPassport1997, RussianDriverLicense
  - RussianISO9SystemA, RussianISO9SystemB, RussianISOR9Table2

## Installation

```bash
dotnet add package TranslitKit
```

## Quick Start

```csharp
using TranslitKit;

// Ukrainian transliteration (uses UkrainianKMU by default)
string result = Translit.Convert("Берег моря");
// Output: "Bereh moria"

// Specify a different Ukrainian system
result = Translit.Convert("Привіт, світ!", new UkrainianSimple());
// Output: "Pryvit, svit!"

// Russian transliteration
result = Translit.Convert("Не выходи из комнаты", new RussianSimple());
// Output: "Ne vyhodi iz komnaty"

// Control case preservation
result = Translit.Convert("ПРИВІТ", new UkrainianKMU(), preserveCase: true);
// Output: "PRYVIT"
```

## Available Transliteration Systems

### Ukrainian Systems

| System | Description |
|--------|-------------|
| `UkrainianKMU` | National 2010 (most recent, approved by Cabinet of Ministers) - **Default** |
| `UkrainianSimple` | Simplified transliteration |
| `UkrainianWWS` | WWS transliteration system |
| `UkrainianBritish` | British Standard |
| `UkrainianBGN` | BGN/PCGN romanization |
| `UkrainianISO9` | ISO 9 standard |
| `UkrainianFrench` | French transliteration system |
| `UkrainianGerman` | German (Duden) transliteration |
| `UkrainianGOST1971` | GOST 1971 (Soviet standard) |
| `UkrainianGOST1986` | GOST 1986 (Soviet standard) |
| `UkrainianPassport2007` | Passport 2007 standard |
| `UkrainianNational1996` | National 1996 standard |
| `UkrainianPassport2004Alt` | Passport 2004 alternative |

### Russian Systems

| System | Description |
|--------|-------------|
| `RussianGOST2006` | GOST 2006 (Russian Federation standard) |
| `RussianSimple` | Simplified transliteration |
| `RussianICAO` | ICAO romanization |
| `RussianTelegram` | Telegram transliteration |
| `RussianInternationalPassport1997` | International Passport 1997 |
| `RussianInternationalPassport1997Reduced` | Passport 1997 (reduced) |
| `RussianDriverLicense` | Driver License transliteration |
| `RussianISO9SystemA` | ISO 9 System A |
| `RussianISO9SystemB` | ISO 9 System B |
| `RussianISOR9Table2` | ISO R/9 Table 2 |

## Advanced Usage

### Working with Collections

```csharp
using TranslitKit;

// Access all Ukrainian systems
var allUkrainian = TransliterationTables.AllUkrainian;
foreach (var table in allUkrainian)
{
    string result = Translit.Convert("Україна", table);
    Console.WriteLine(result);
}

// Access all Russian systems
var allRussian = TransliterationTables.AllRussian;

// Access all systems (Ukrainian + Russian)
var allSystems = TransliterationTables.AllTransliterations;
```

### Case Preservation

```csharp
// Default behavior: preserve case if all uppercase
string input = "КИЇВ";
string output = Translit.Convert(input);
// Output: "KYIV" (uppercase preserved)

// Disable case preservation
output = Translit.Convert(input, new UkrainianKMU(), preserveCase: false);
// Output: "Kyiv" (case not preserved)
```

## How It Works

The library implements a 5-stage transliteration algorithm:

1. **Character Deletion** - Remove soft signs and apostrophes
2. **Special Cases** - Handle context-specific multi-character sequences (e.g., "зг" → "zgh")
3. **First Characters** - Apply word-initial variants (e.g., "є" → "ye" at start, "ie" otherwise)
4. **Main Transliteration** - Character-by-character conversion
5. **Case Preservation** - Maintain uppercase if original was all caps

## Reverse Transliteration (Latin → Cyrillic)

**TranslitKit does not support reverse transliteration** (converting Latin text back to Cyrillic). This is an intentional design decision due to fundamental limitations inherent in reverse transliteration.

### Why Reverse Transliteration Is Not Supported

#### 1. Ambiguity - Multiple Characters Map to Same Output

Many different Cyrillic characters transliterate to the same Latin representation:

```csharp
// Ukrainian examples:
"и" → "y"
"і" → "i"
"ї" → "i"  // mid-word

// Reverse: Which character did "i" come from?
"i" → "і"? or "ї"? or "й"? (impossible to determine)
```

#### 2. Information Loss from Character Deletion

Some characters are deleted during transliteration and cannot be recovered:

```csharp
// Forward transliteration:
"м'ясо" → "miaso"  // apostrophe (') is deleted

// Reverse is impossible:
"miaso" → "мясо"? or "м'ясо"? or "мяcо"?
```

#### 3. Context-Dependent Rules Cannot Be Reversed

Different contexts produce different outputs from the same character:

```csharp
// Ukrainian є has different transliterations:
"Єва"        → "Yeva"         // є at word start → "ye"
"моє"        → "moie"         // є mid-word → "ie"
"поєднання"  → "poiednannia"  // є mid-word → "ie"

// Reverse: What did "ye" come from?
"Yeva" → "Єва"? or "Йева"? or "Ева"?
```

#### 4. Special Contextual Sequences

Special case rules create one-way transformations:

```csharp
// Ukrainian KMU special case:
"розгром" → "rozghrom"  // "зг" becomes "zgh"

// Reverse is ambiguous:
"rozghrom" → "розгром"? or "розгхром"?
```

### Recommended Approaches

If you need bidirectional text conversion, consider these alternatives:

1. **Store Both Versions** - Keep the original Cyrillic text alongside the transliteration:
   ```csharp
   var original = "Київ";
   var transliterated = Translit.Convert(original, new UkrainianKMU());
   // Store both: original = "Київ", transliterated = "Kyiv"
   ```

2. **Use Transliteration for Display Only** - Treat Latin output as a read-only representation for URLs, filenames, or display purposes while maintaining Cyrillic as the source of truth.

3. **Implement Custom Reverse Logic** - If reverse transliteration is critical, you'll need:
   - Custom dictionary-based lookup tables
   - Statistical or machine learning approaches for disambiguation
   - Language-specific heuristics
   - Acceptance that results will be probabilistic and potentially incorrect

4. **Specialized Bidirectional Libraries** - Look for libraries specifically designed for bidirectional conversion, though these face the same fundamental ambiguity challenges.

### Use Cases Where One-Way Is Sufficient

TranslitKit is designed for scenarios where Cyrillic → Latin conversion is sufficient:

- **URL slugs**: `"Київ"` → `"kyiv"` for `example.com/city/kyiv`
- **Filenames**: `"Звіт 2024.pdf"` → `"zvit-2024.pdf"`
- **Search indexing**: Make Cyrillic content searchable via Latin queries
- **International documents**: Passport names, official documents requiring Latin script
- **Data export**: Converting Cyrillic data for systems that don't support Unicode
- **Display purposes**: Showing Cyrillic names in Latin-only contexts

In all these cases, the original Cyrillic text should remain the authoritative source.

## Requirements

- .NET 8.0 or higher

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

This library is a C# port of [translit-ua](https://github.com/dchaplinsky/translit-ua) by Dmitry Chaplinsky.

## Support

For issues and questions, please use the [GitHub Issues](https://github.com/yourusername/TranslitKit/issues) page.
