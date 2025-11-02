# Dependabot Configuration

This document explains the Dependabot configuration for TranslitKit.

## Overview

Dependabot is GitHub's automated dependency management tool that:
- Monitors dependencies for security vulnerabilities and new versions
- Automatically creates pull requests with dependency updates
- Can automatically merge safe updates (if configured)
- Provides detailed change summaries in PR descriptions

## Configuration File

The configuration is defined in `.github/dependabot.yml` with two package ecosystems:

### 1. NuGet Package Dependencies

**Location:** `dependabot.yml` - NuGet ecosystem

**Schedule:**
- Updates check: **Weekly (Monday at 03:00 UTC)**
- Creates up to **10 open PRs** at a time

**Settings:**
- **Commit prefix:** `chore(deps):` for production dependencies
- **Commit prefix:** `chore(deps-dev):` for development dependencies
- **Labels:** `dependencies`, `nuget`
- **Reviewer:** @akolodkin
- **Rebase strategy:** Auto-rebase when out of date
- **Versioning:** Auto-detect semantic versioning

**Special Rules:**
- Breaking changes (major version bumps) are allowed but created as separate PRs
- `Microsoft.NET.Test.Sdk` major version updates are ignored (requires manual review)

### 2. GitHub Actions Workflow Dependencies

**Location:** `dependabot.yml` - GitHub Actions ecosystem

**Schedule:**
- Updates check: **Weekly (Monday at 03:30 UTC)**
- Creates up to **10 open PRs** at a time

**Settings:**
- **Commit prefix:** `chore(ci):`
- **Labels:** `dependencies`, `github-actions`
- **Reviewer:** @akolodkin
- **Rebase strategy:** Auto-rebase when out of date

## Dependencies Being Monitored

### NuGet Packages (Production)
- None (TranslitKit has zero external dependencies by design)

### NuGet Packages (Development/Testing)
- `Microsoft.NET.Test.Sdk` - xUnit test runner framework
- `xunit` - Unit testing framework
- `xunit.runner.visualstudio` - Test explorer integration
- `coverlet.collector` - Code coverage measurement

### GitHub Actions
- `actions/checkout@v4`
- `actions/setup-dotnet@v4`
- `actions/upload-artifact@v4`
- `softprops/action-gh-release@v1`

## How Dependabot Works

### 1. Automatic Checks
Dependabot checks for updates every Monday at 03:00 UTC (NuGet) and 03:30 UTC (Actions).

### 2. Pull Requests
When updates are available, Dependabot creates PRs with:
- Descriptive title: `chore(deps): update package-name to v1.2.3`
- Changelog in PR description
- Link to release notes
- Compatibility information

### 3. PR Review Workflow

```
1. Dependabot creates PR
        ↓
2. GitHub Actions CI runs automatically
   (build and test on Windows/Linux × .NET 8.0/9.0)
        ↓
3. Review PR and check:
   - All tests pass ✓
   - No breaking changes ✓
   - Changelog reviewed ✓
        ↓
4. Approve and merge
   OR request changes
```

## Managing Dependabot

### Merging Updates

Updates are automatically reviewed by CI. You can:

1. **Auto-merge safe updates** (if configured):
   ```bash
   # In GitHub: Enable auto-merge on PR
   ```

2. **Manual merge** (current setup):
   - Review the PR
   - Verify all CI checks pass
   - Click "Merge pull request"
   - Delete branch

### Handling Failed Updates

If tests fail after a Dependabot update:

1. **View test failure details** in the "Checks" tab
2. **Request changes** on the PR with specific feedback
3. Dependabot will automatically retry with fixes (if applicable)

### Ignoring Updates

To ignore specific updates (in `.github/dependabot.yml`):

```yaml
ignore:
  - dependency-name: "package-name"
    versions: ["1.2.3"]  # Ignore specific version
  - dependency-name: "package-name"
    update-types:
      - "version-update:semver-major"  # Ignore major bumps
```

### Checking Update History

View all Dependabot activity:
1. Go to GitHub: **Insights** → **Dependency graph** → **Dependabot**
2. See all past updates and security advisories

## Security Considerations

### Production vs Development
- **Production dependencies:** None (TranslitKit has zero external runtime dependencies)
- **Development dependencies:** Test frameworks and CI tools only

### Security Vulnerabilities
- Dependabot automatically creates security fix PRs
- Security fixes are high priority and should be merged quickly
- GitHub flags security advisories in the repository

### Automated Security Scanning
Additional GitHub security features (not configured but available):
- Code scanning with CodeQL
- Secret scanning
- Dependency scanning

## Customization

To modify Dependabot behavior, edit `.github/dependabot.yml`:

### Change update frequency:
```yaml
schedule:
  interval: daily      # daily, weekly, monthly
  day: monday         # monday, tuesday, etc. (weekly only)
  time: "03:00"       # UTC time HH:MM
```

### Adjust PR limits:
```yaml
open-pull-requests-limit: 20  # Max concurrent PRs
```

### Disable for specific packages:
```yaml
ignore:
  - dependency-name: "package-name"
```

### Change commit message format:
```yaml
commit-message:
  prefix: "chore(deps):"
  include: "scope"  # Include package scope in message
```

## Troubleshooting

### Dependabot not creating PRs

1. **Check enablement:**
   - Go to GitHub: **Settings** → **Code security and analysis**
   - Ensure "Dependabot version updates" is enabled

2. **Check configuration:**
   - Verify `.github/dependabot.yml` is valid YAML
   - Check for syntax errors

3. **Check schedule:**
   - Updates run on configured schedule (Monday 03:00 UTC for NuGet)
   - May take up to 1 hour to process

### Dependabot PRs keep failing tests

1. Review test failure logs
2. Create an issue if it's a genuine compatibility problem
3. Manually update the package if needed

### Too many open PRs

Adjust `open-pull-requests-limit` in configuration (current: 10)

## References

- [GitHub Dependabot Documentation](https://docs.github.com/en/code-security/dependabot)
- [Configuration Options](https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/configuration-options-for-the-dependabot.yml-file)
- [Best Practices](https://docs.github.com/en/code-security/dependabot/working-with-dependabot)
- [NuGet Ecosystem](https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/about-dependabot-version-updates#supported-repositories-and-ecosystems)
