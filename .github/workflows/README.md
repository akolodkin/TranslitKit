# GitHub Actions Workflows

This directory contains GitHub Actions workflows for TranslitKit continuous integration and release automation.

## Workflows

### 1. CI Workflow (`ci.yml`)

**Triggers:**

- Push to `main` branch
- Pull requests targeting `main` branch

**What it does:**

- Runs on both Ubuntu Linux and Windows (across .NET 8.0 and 9.0)
- Restores NuGet packages
- Builds the solution in Release configuration
- Runs all 858 tests with code coverage analysis
- Collects test results (TRX format)
- Uploads code coverage reports to Codecov
- Uploads test results as artifacts

**Status Badges:**

```markdown
[![CI](https://github.com/akolodkin/TranslitKit/actions/workflows/ci.yml/badge.svg)](https://github.com/akolodkin/TranslitKit/actions/workflows/ci.yml)
[![codecov](https://codecov.io/gh/akolodkin/TranslitKit/branch/main/graph/badge.svg)](https://codecov.io/gh/akolodkin/TranslitKit)
```

**Code Coverage:**

- XPlat Code Coverage data collected during test execution
- Coverage reports uploaded to [Codecov.io](https://codecov.io/gh/akolodkin/TranslitKit)
- Coverage badge shows overall code coverage percentage
- Only one build matrix combination uploads to avoid duplicates (Ubuntu Linux + .NET 8.0)
- View detailed coverage reports on [Codecov dashboard](https://codecov.io/gh/akolodkin/TranslitKit)

### 2. Release Workflow (`release.yml`)

**Triggers:**

- Push of git tags matching `v*.*.*` pattern (e.g., `v1.0.0`, `v1.2.3`)

**What it does:**

1. Extracts version from the git tag (e.g., `v1.2.3` → `1.2.3`)
2. Updates the version in `TranslitKit.csproj`
3. Restores dependencies and builds in Release mode
4. Runs all tests to ensure quality
5. Creates NuGet package
6. Publishes package to NuGet.org
7. Creates a GitHub Release with the package as an artifact

**Status Badge:**

```markdown
[![Release](https://github.com/akolodkin/TranslitKit/actions/workflows/release.yml/badge.svg)](https://github.com/akolodkin/TranslitKit/actions/workflows/release.yml)
```

## Required Secrets

To use the release workflow, you need to configure the following GitHub secret:

### `NUGET_API_KEY`

This is your NuGet.org API key required to publish packages.

**How to set it up:**

1. **Get a NuGet API Key:**

   - Go to [NuGet.org](https://www.nuget.org/)
   - Sign in to your account
   - Go to [API Keys](https://www.nuget.org/account/apikeys)
   - Click "Create" to generate a new API key
   - Set the following:
     - **Key Name**: `TranslitKit GitHub Actions`
     - **Glob Pattern**: `TranslitKit`
     - **Expiration**: Choose appropriate duration (365 days recommended)
     - **Scopes**: Select "Push new packages and package versions"
   - Click "Create" and copy the generated API key

2. **Add Secret to GitHub:**
   - Go to your GitHub repository: https://github.com/akolodkin/TranslitKit
   - Navigate to Settings → Secrets and variables → Actions
   - Click "New repository secret"
   - Name: `NUGET_API_KEY`
   - Value: Paste your NuGet API key
   - Click "Add secret"

## How to Create a Release

Follow these steps to publish a new version to NuGet:

### 1. Update Version Information

**Update `CHANGELOG.md`:**

```markdown
## [1.0.1] - 2025-11-02

### Fixed

- Bug fix description

### Added

- New feature description
```

**Optionally update `PackageReleaseNotes` in `src/TranslitKit/TranslitKit.csproj`:**

```xml
<PackageReleaseNotes>Bug fixes and performance improvements.</PackageReleaseNotes>
```

### 2. Commit Changes

```bash
git add CHANGELOG.md
git commit -m "chore: prepare v1.0.1 release"
git push origin main
```

### 3. Create and Push Version Tag

```bash
# Create tag locally (version must match v*.*.* pattern)
git tag v1.0.1

# Push tag to GitHub (this triggers the release workflow)
git push origin v1.0.1
```

### 4. Monitor the Release

- Go to the [Actions tab](https://github.com/akolodkin/TranslitKit/actions)
- Watch the "Release to NuGet" workflow progress
- Once complete:
  - Package is published to [NuGet.org](https://www.nuget.org/packages/TranslitKit/)
  - GitHub Release is created at https://github.com/akolodkin/TranslitKit/releases

## Version Numbering

This project uses [Semantic Versioning](https://semver.org/):

- **MAJOR** version (`v2.0.0`): Incompatible API changes
- **MINOR** version (`v1.1.0`): New functionality in a backward-compatible manner
- **PATCH** version (`v1.0.1`): Backward-compatible bug fixes

## Troubleshooting

### Release Workflow Fails at Publish Step

**Error:** `401 Unauthorized` or `403 Forbidden`

**Solution:**

- Verify `NUGET_API_KEY` secret is set correctly
- Check if the API key has expired
- Ensure API key has "Push" permissions for the TranslitKit package

### Version Already Exists on NuGet

**Error:** `409 Conflict - Package already exists`

**Solution:**

- You cannot replace an existing version on NuGet
- Create a new version tag (e.g., if `v1.0.1` exists, use `v1.0.2`)
- The workflow uses `--skip-duplicate` flag to handle this gracefully

### Tests Fail During Release

**Solution:**

- The workflow will abort if tests fail
- Fix the failing tests locally
- Push fixes to main
- Delete and recreate the tag:
  ```bash
  git tag -d v1.0.1
  git push origin :refs/tags/v1.0.1
  git tag v1.0.1
  git push origin v1.0.1
  ```

## Code Coverage

### Overview

TranslitKit uses automated code coverage analysis to track test quality and ensure high standards:

- **Coverage Tool:** [Coverlet](https://github.com/coverlet-coverage/coverlet) (included in test project)
- **Coverage Format:** Cobertura XML (XPlat Code Coverage)
- **Coverage Reporting:** [Codecov.io](https://codecov.io/gh/akolodkin/TranslitKit)
- **Current Coverage:** 100% (858 comprehensive unit tests)

### How Coverage Works

1. **Collection:** During CI workflow, tests run with `--collect:"XPlat Code Coverage"`
2. **Report Generation:** Coverlet generates `coverage.cobertura.xml` files in Cobertura format
3. **Upload:** Codecov action uploads reports to codecov.io
4. **Results:** Coverage badge reflects overall line/branch coverage

### Local Coverage Testing

To generate and view coverage reports locally:

```bash
# Run tests with coverage
dotnet test --configuration Release --collect:"XPlat Code Coverage" TranslitKit.sln

# Reports are generated in TestResults/*/coverage.opencover.xml
# View detailed coverage reports by uploading to Codecov
```

### Coverage Dashboard

View detailed coverage statistics:
- **Codecov Dashboard:** https://codecov.io/gh/akolodkin/TranslitKit
- **Coverage by File:** View which files have gaps
- **Coverage Trends:** Track coverage over time
- **PR Coverage Analysis:** See coverage impact of pull requests

### Maintaining High Coverage

All changes should maintain or improve code coverage:

1. Write tests alongside implementation (TDD approach)
2. Use existing 858 tests as reference for testing patterns
3. Run coverage locally before pushing
4. Review PR coverage reports on Codecov

## Local Testing

Before creating a release, test locally:

```bash
# Restore and build
dotnet restore TranslitKit.sln
dotnet build --no-restore --configuration Release TranslitKit.sln

# Run tests
dotnet test --no-build --configuration Release TranslitKit.sln

# Create package locally
dotnet pack src/TranslitKit/TranslitKit.csproj --configuration Release --output ./artifacts

# Inspect package contents
dotnet nuget verify ./artifacts/TranslitKit.*.nupkg
```

## Additional Resources

### CI/CD
- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [NuGet Package Publishing](https://learn.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli)
- [Semantic Versioning](https://semver.org/)

### Code Coverage
- [Coverlet Documentation](https://github.com/coverlet-coverage/coverlet)
- [Codecov Integration Guide](https://docs.codecov.io/docs/about-the-codecov-bash-uploader)
- [XPlat Code Coverage Format](https://github.com/coverlet-coverage/coverlet/wiki/OpenCover)

### Project Resources
- [TranslitKit GitHub Repository](https://github.com/akolodkin/TranslitKit)
- [Codecov Dashboard](https://codecov.io/gh/akolodkin/TranslitKit)
- [NuGet Package](https://www.nuget.org/packages/TranslitKit/)
