# cx.core.translit

[![NuGet](https://img.shields.io/nuget/v/cx.core.translit.svg)](https://www.nuget.org/packages/cx.core.translit/)
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
dotnet add package cx.core.translit
```

## Quick Start

```csharp
using cx.core.translit;

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
using cx.core.translit;

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

## Requirements

- .NET 8.0 or higher

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

This library is a C# port of [translit-ua](https://github.com/dchaplinsky/translit-ua) by Dmitry Chaplinsky.

## Support

For issues and questions, please use the [GitHub Issues](https://github.com/yourusername/cx.core.translit/issues) page.
