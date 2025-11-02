# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.3] - 2025-11-02

### Added

#### Composable Transliteration Maps

A new architecture for combining multiple transliteration tables together, enabling flexible handling of special characters and domain-specific extensions.

- **New Class: `SpecialCharactersMap`** (`src/TranslitKit/SpecialCharactersMap.cs`)
  - Built-in transliteration map for Unicode characters commonly used in Cyrillic documents
  - Handles guillemets (French quotation marks): `«` → `"`, `»` → `"`
  - Handles numero sign: `№` → `No`
  - Can be stacked with any language-specific transliteration table
  - Implements `ITransliterationTable` interface

- **New Class: `CompositeTransliterationTable`** (`src/TranslitKit/CompositeTransliterationTable.cs`)
  - Implements `ITransliterationTable`
  - Enables composing/stacking two transliteration tables together
  - Static factory method: `CompositeTransliterationTable.Combine(firstTable, secondTable)`
  - Intelligently merges character mappings from both tables (second table takes precedence)
  - Combines `SpecialCases` and `FirstCharacters` dictionaries from both tables
  - Handles `DeletePattern` regex intelligently

- **New Class: `CurrencyMap`** (`src/TranslitKit/CurrencyMap.cs`)
  - Pre-built transliteration map for 25+ currency symbols
  - Supports Eastern European currencies: `₴` → `UAH`, `₽` → `RUB`, `₼` → `AZN`, `₾` → `GEL`
  - Supports Asian currencies: `₹` → `INR`, `₨` → `PKR`, `₩` → `KRW`, `¥` → `CNY`
  - Supports Middle East/Africa: `₪` → `ILS`, `₦` → `NGN`, `₵` → `GHS`
  - Supports Americas: `₡` → `CRC`, `₲` → `PYG`, `₱` → `PHP`
  - Supports other currencies: `₺` → `TRY`, `₿` → `BTC`, `$` → `USD`, `€` → `EUR`, `£` → `GBP`, `¢` → `CENT`, `¤` → `CURR`
  - Can be composed with any language-specific transliteration table
  - Implements `ITransliterationTable` interface

- **Enhanced: `TransliterationTables`** (`src/TranslitKit/TransliterationTables.cs`)
  - New static property: `SpecialCharacters` - provides access to the built-in special characters map
  - Updated documentation to reference composition API

### Benefits

- **DRY Principle**: Unicode character mappings defined once, reused across all 23 transliteration systems
- **No Duplication**: Original 23 transliteration tables remain completely unchanged
- **Extensibility**: Easy to create custom transliteration maps for domain-specific characters
- **Flexibility**: Compose any two transliteration tables together in any order
- **Backward Compatible**: All existing code continues to work without any changes

### Usage Examples

Basic composition:

```csharp
var table = CompositeTransliterationTable.Combine(
    TransliterationTables.SpecialCharacters,
    new UkrainianKMU());

var result = Translit.Convert("«Привіт» №1", table);
// Output: "Pryvit" No1
```

### Use Cases

- **International documents**: Process documents with guillemets and numero signs
- **Financial reports**: Handle currency symbols and mathematical operators
- **Legal documents**: Handle copyright, trademark, and other legal symbols
- **Technical documentation**: Handle special technical characters and formatting marks
- **Data conversion**: Normalize text with special punctuation marks
- **Search indexing**: Include special characters in full-text search indexing
- **Document processing**: Handle OCR output with varied formatting

- Multi-target support for .NET 8.0 and .NET 9.0
- GitHub Actions CI/CD workflows:
  - `ci.yml` - Continuous integration on push/PR (builds and tests on Windows & Linux against .NET 8.0 and 9.0)
  - `release.yml` - Automated NuGet package publishing on version tags
- Comprehensive GitHub Actions documentation in `.github/workflows/README.md`

### Changed

- Project reorganization: renamed from `cx.core.translit` to `TranslitKit`
- Updated all project folder names and solution structure to match new name
- Updated all internal namespaces to `TranslitKit`
- Updated repository URLs to point to `akolodkin/TranslitKit`

## [1.0.0] - 2025-11-01

### Added

#### Core Features
- Initial release of TranslitKit
- Complete C# port of Python translit-ua library
- Full .NET 8.0+ support with nullable reference types
- Comprehensive XML documentation for all public APIs

#### Ukrainian Transliteration Systems (13 total)
- `UkrainianKMU` - National 2010 standard (Cabinet of Ministers) - Default
- `UkrainianSimple` - Simplified transliteration system
- `UkrainianWWS` - Scholarly (WWS) system with diacritics
- `UkrainianBritish` - British Standard system
- `UkrainianBGN` - BGN/PCGN romanization system
- `UkrainianISO9` - ISO 9 international standard
- `UkrainianFrench` - French phonetics system
- `UkrainianGerman` - German (Duden) phonetics system
- `UkrainianGOST1971` - Soviet-era GOST 16876-71 standard
- `UkrainianGOST1986` - GOST 7.79-2000 (1986 variant)
- `UkrainianPassport2007` - Passport standard 2007
- `UkrainianNational1996` - National standard 1996
- `UkrainianPassport2004Alt` - Passport 2004 alternative

#### Russian Transliteration Systems (10 total)
- `RussianGOST2006` - GOST R 52535.1-2006 official standard - Default
- `RussianSimple` - Basic romanization system
- `RussianICAO` - ICAO standard (2013) for travel documents
- `RussianTelegram` - Telegraph communication system
- `RussianInternationalPassport1997` - Standard passport system
- `RussianInternationalPassport1997Reduced` - Passport variant with compression
- `RussianDriverLicense` - Driver's license system
- `RussianISO9SystemA` - ISO 9:1995 System A with diacritics
- `RussianISO9SystemB` - ISO 9:1995 System B with digraphs
- `RussianISOR9Table2` - ISO/R 9:1968 Table 2

#### Core Functionality
- `Translit` static class with Convert methods
- `ITransliterationTable` interface for custom implementations
- `TransliterationTableBase` abstract base class
- `TransliterationTables` static class with collections:
  - `AllUkrainian` - All 13 Ukrainian systems
  - `AllRussian` - All 10 Russian systems
  - `AllTransliterations` - All 23 systems combined

#### Features
- 5-stage transliteration algorithm:
  1. Character deletion (soft signs, apostrophes)
  2. Special case patterns (context-specific sequences)
  3. First character patterns (word-initial variants)
  4. Main transliteration (character-by-character)
  5. Case preservation (optional uppercase preservation)
- Support for special cases (e.g., зг→zgh)
- Word-boundary detection for context-sensitive rules
- Automatic uppercase variant generation
- Case preservation option
- Null-safe API design

#### Testing
- 858 comprehensive unit tests using xUnit
- 100% test coverage of core algorithms
- Test-Driven Development (TDD) approach
- Tests for all 23 transliteration systems
- Edge case coverage (null, empty, all-caps, mixed case)

#### Documentation
- Complete README.md with examples
- XML documentation comments for IntelliSense
- MIT License
- Comprehensive plan.md tracking implementation

### Technical Details
- Target Framework: .NET 8.0
- Language: C# 12
- Testing Framework: xUnit 2.5.3
- No external dependencies (uses only .NET BCL)
- Nullable reference types enabled
- AOT-compatible design

[1.0.0]: https://github.com/yourusername/TranslitKit/releases/tag/v1.0.0
