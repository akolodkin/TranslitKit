# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**TranslitKit** is a .NET library that converts Cyrillic text (Ukrainian and Russian) to Latin characters. It's a C# port of the Python [translit-ua](https://github.com/dchaplinsky/translit-ua) library, providing 23 different transliteration systems (13 Ukrainian, 10 Russian).

**Important limitation**: This library only supports one-way transliteration (Cyrillic → Latin). Reverse transliteration is intentionally not supported due to fundamental ambiguity issues (multiple Cyrillic characters can map to the same Latin output, apostrophes are deleted, context-dependent rules can't be reversed).

## Build and Test Commands

### Building
```bash
# Restore packages
dotnet restore TranslitKit.sln

# Build (use --no-restore per project convention)
dotnet build --no-restore TranslitKit.sln

# Build in Release mode
dotnet build --no-restore -c Release TranslitKit.sln
```

### Testing
```bash
# Run all tests (858 tests total)
dotnet test --no-build TranslitKit.sln

# Run tests with detailed output
dotnet test --no-build --verbosity normal TranslitKit.sln

# Run specific test class
dotnet test --no-build --filter "FullyQualifiedName~TranslitTests"

# Run tests for specific transliteration system
dotnet test --no-build --filter "FullyQualifiedName~UkrainianKMUTests"
dotnet test --no-build --filter "FullyQualifiedName~RussianGOST2006Tests"
```

### Packaging
```bash
# Create NuGet package
dotnet pack src/TranslitKit/TranslitKit.csproj -c Release

# Package is created in: src/TranslitKit/bin/Release/TranslitKit.{version}.nupkg
```

## Architecture

### Core Components

The library follows a simple, clean architecture with three main layers:

1. **Public API** (`Translit` static class)
   - Single entry point: `Translit.Convert(string source, ITransliterationTable table, bool preserveCase = true)`
   - Default overload uses `UkrainianKMU` when no table specified

2. **Transliteration Tables** (23 implementations)
   - All implement `ITransliterationTable` interface
   - All inherit from `TransliterationTableBase` abstract class
   - Located in `Ukrainian/` and `Russian/` subdirectories
   - Each defines its own character mappings via constructor dictionaries

3. **Algorithm** (5-stage pipeline in `Translit.Convert`)
   - **Stage 1**: Character deletion (soft signs, apostrophes via `DeletePattern` regex)
   - **Stage 2**: Special cases (context-specific multi-char sequences via `SpecialCases` dictionary)
   - **Stage 3**: First characters (word-initial variants via `FirstCharacters` dictionary, uses `\b` word boundaries)
   - **Stage 4**: Main transliteration (char-by-char via `MainTranslitTable` dictionary)
   - **Stage 5**: Case preservation (uppercase only if source was all uppercase)

### Key Design Patterns

- **Strategy Pattern**: `ITransliterationTable` allows different transliteration algorithms
- **Template Method**: `TransliterationTableBase` provides common initialization logic, subclasses provide data
- **Static Factory**: `TransliterationTables` class provides collections of all available tables

### Table Implementation Pattern

Each transliteration system (e.g., `UkrainianKMU`, `RussianGOST2006`) follows this pattern:

```csharp
public class UkrainianKMU : TransliterationTableBase
{
    public UkrainianKMU() : base(
        mainTable: new Dictionary<string, string> { /* lowercase mappings */ },
        specialCases: new Dictionary<string, string> { /* optional */ },
        firstCharacters: new Dictionary<string, string> { /* optional */ },
        deleteChars: new[] { /* optional */ }
    ) { }
}
```

The base class automatically generates uppercase variants for `MainTranslitTable` using `CapitalizeFirst()` helper.

### Important Implementation Details

1. **Uppercase Generation**: When a lowercase character maps to multiple chars (e.g., `'ж'` → `"zh"`), the uppercase variant capitalizes only the first character (`'Ж'` → `"Zh"`). This is handled in `TransliterationTableBase.BuildMainTranslitTable()`.

2. **Word Boundary Detection**: `FirstCharacters` patterns use regex `\b` word boundaries to detect word-initial positions. This means characters at the start of words or after punctuation/spaces.

3. **Case Preservation Logic**: The `preserveCase` parameter only affects all-uppercase input. Mixed case or title case input retains natural casing from the transliteration tables.

4. **Null Safety**: The API is null-safe with nullable reference types enabled. `source` can be null (returns empty string), but `table` cannot be null (throws `ArgumentNullException`).

## Project Structure

```
src/TranslitKit/                    # Main library
  ├── ITransliterationTable.cs      # Interface defining table contract
  ├── TransliterationTableBase.cs   # Base class with common logic
  ├── Translit.cs                   # Main API with 5-stage algorithm
  ├── TransliterationTables.cs      # Static collections of all tables
  ├── Ukrainian/                    # 13 Ukrainian systems
  │   ├── UkrainianKMU.cs          # Default (National 2010)
  │   ├── UkrainianSimple.cs
  │   └── ...
  └── Russian/                      # 10 Russian systems
      ├── RussianGOST2006.cs       # Default (GOST 2006)
      ├── RussianSimple.cs
      └── ...

tests/TranslitKit.Tests/            # xUnit test project
  ├── TranslitTests.cs              # Core algorithm tests
  ├── ITransliterationTableTests.cs # Interface contract tests
  ├── TransliterationTableBaseTests.cs
  ├── Ukrainian/                    # Tests for each Ukrainian system
  └── Russian/                      # Tests for each Russian system
```

## Development Workflow

This project was developed using **Test-Driven Development (TDD)**:
- Tests were written first for each feature
- Implementation followed to make tests pass
- Refactoring was done after tests passed

### Adding a New Transliteration System

1. Create new class in `Ukrainian/` or `Russian/` folder
2. Inherit from `TransliterationTableBase`
3. Define mappings as dictionaries in constructor
4. Create corresponding test class in `tests/TranslitKit.Tests/[Ukrainian|Russian]/`
5. Write comprehensive tests including edge cases
6. Add to appropriate collection in `TransliterationTables.cs`

### Test Naming Convention

Tests follow the pattern: `MethodName_Scenario_ExpectedResult`

Example: `Convert_UkrainianTextWithSpecialCases_HandlesZgCorrectly`

## Target Framework

- **.NET 8.0 and .NET 9.0** (uses C# 12 features compatible with both versions)
- Nullable reference types enabled
- No external dependencies (only .NET BCL)
- AOT-compatible design
- Multi-targeted NuGet package (includes binaries for both net8.0 and net9.0)

## NuGet Package Details

- **Package ID**: TranslitKit
- **License**: MIT
- **Repository**: GitHub (update URL in .csproj before publishing)
- **XML Documentation**: Enabled (`GenerateDocumentationFile: true`)
- **Current Version**: 1.0.0

## Testing Philosophy

The library has **858 comprehensive tests** covering:
- All 23 transliteration systems
- Edge cases (null, empty, all-caps, mixed case)
- All stages of the 5-stage algorithm
- Special character handling (apostrophes, soft signs, word boundaries)
- Case preservation logic
- Unicode handling

When modifying transliteration logic, ensure all 858 tests continue to pass.

## Reference Implementation

This library matches the behavior of the Python [translit-ua](https://github.com/dchaplinsky/translit-ua) library. When in doubt about expected behavior, refer to the Python implementation or the comprehensive test suite.
