# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2025-11-01

### Added

#### Core Features
- Initial release of cx.core.translit
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

[1.0.0]: https://github.com/yourusername/cx.core.translit/releases/tag/v1.0.0
