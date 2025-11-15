# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

ASP.NET Core 8.0 Razor Pages web application for real estate management ("Nepremicnine" means "Real Estate" in Slovenian). Academic project with Docker containerization and Azure deployment via GitHub Actions.

**Docker Hub**: https://hub.docker.com/repository/docker/borci1417/nepremicnine/general

## Technology Stack

- **.NET 8.0** - ASP.NET Core with Razor Pages
- **Entity Framework Core 8.0.11** - ORM with SQLite database
- **ASP.NET Identity** - Authentication system with custom `Uporabniki` model
- **MSTest** - Unit testing framework
- **Docker** - Multi-stage containerized deployment
- **Azure Web Apps** - Hosting platform

## Common Commands

### Build and Run
```bash
# Build the project
dotnet build RZ_nepremicnine.csproj

# Run the application locally
dotnet run --project RZ_nepremicnine.csproj

# Build Docker image
docker build -t nepremicnine .

# Run in Docker
docker run -p 8080:8080 nepremicnine
```

### Testing
```bash
# Run all tests
dotnet test RZ_Nepremicnine.test/RZ_Nepremicnine.test.csproj

# Run tests with detailed output
dotnet test RZ_Nepremicnine.test/RZ_Nepremicnine.test.csproj --logger "console;verbosity=detailed"

# Run tests with TRX output (for CI/CD)
dotnet test RZ_Nepremicnine.test/RZ_Nepremicnine.test.csproj --logger "trx;LogFileName=test_results.trx" --results-directory "RZ_Nepremicnine.test/TestResults"
```

### Database Migrations
```bash
# Add a new migration
dotnet ef migrations add <MigrationName>

# Update database to latest migration
dotnet ef database update

# Revert to specific migration
dotnet ef database update <MigrationName>

# Remove last migration
dotnet ef migrations remove
```

### Publish
```bash
# Publish for Release deployment
dotnet publish RZ_nepremicnine.csproj --configuration Release --output ./publish
```

## Architecture

### Razor Pages Pattern
This application uses the **Razor Pages** pattern (not MVC Controllers). Each page consists of:
- `.cshtml` file - UI markup and Razor syntax
- `.cshtml.cs` file - PageModel class with handlers (OnGet, OnPost, etc.)

Pages automatically map to routes based on their filesystem location under `Pages/`.

### Database Architecture

**DbContext**: `AppDbContext` inherits from `IdentityDbContext<Uporabniki>`

**Connection String**: SQLite database at `mydatabase.db` in the root directory

**Models**:
- `Uporabniki` - Custom user model extending `IdentityUser` (in Models/Uporabniki.cs)
- `Nepremicnina` - Property model (currently commented out, awaiting implementation)

The application uses ASP.NET Identity tables (AspNetUsers, AspNetRoles, etc.) automatically provided by the framework.

### Authentication Configuration

Configured in Program.cs:15-27:
- Email-based login (not username)
- Password minimum 8 characters, no complexity requirements
- Email uniqueness enforced
- No email/phone confirmation required
- Custom `Uporabniki` model instead of default `IdentityUser`

### Project Structure

```
Pages/
├── Account/          # Authentication pages (Login, Register, Logout)
├── Shared/           # Layouts and partials (_Layout.cshtml, _LoginPartial.cshtml)
├── Index.cshtml      # Home page
├── AddProperty.cshtml
└── Prodaj.cshtml     # Sales/Sell page

Data/
└── AppDbContext.cs   # EF Core database context

Models/
├── Uporabniki.cs     # User model (extends IdentityUser)
└── Nepremicnina.cs   # Property model (commented out)

ViewModels/           # Form models for pages
├── LoginViewModel.cs
├── RegisterViewModel.cs
├── ChangePasswordViewModel.cs
└── VerifyEmailViewModel.cs

Migrations/           # EF Core database migrations
wwwroot/              # Static files (CSS, JS, libraries)
```

## CI/CD Pipeline

GitHub Actions workflow (`.github/workflows/docker-build.yml`) triggers on push to `main`, `master`, or `test` branches:

1. **Docker Build** - Build and push to `borci1417/nepremicnine:latest`
2. **Unit Tests** - Run test suite and generate TRX report
3. **Publish** - Create Release build
4. **Deploy** - Deploy to Azure Web App `rgis-nepremicnine-test`

The Dockerfile uses a multi-stage build:
- Build stage: .NET SDK 8.0
- Runtime stage: ASP.NET Core 8.0 runtime
- Runs as non-root user on port 8080

## Development Notes

### Naming Conventions
- **Slovenian names** for domain models (Nepremicnina, Uporabniki)
- PascalCase for C# classes, properties, and methods
- Razor Pages handlers: OnGet, OnPost, OnPostAsync

### Current State
- **Working**: Login authentication, database migrations, Docker/Azure deployment
- **Incomplete**: Register page (empty), Logout page (empty), Property model (commented out)
- **Known Issue**: Index.cshtml has unresolved git merge conflict

### Adding New Pages
1. Create `.cshtml` and `.cshtml.cs` files in `Pages/` or subdirectory
2. Inherit from `PageModel` in code-behind
3. Implement OnGet/OnPost handlers
4. Use `@page` directive at top of .cshtml file
5. Route is auto-generated based on file path

### Working with Identity
Use dependency injection to access:
- `UserManager<Uporabniki>` - User CRUD operations
- `SignInManager<Uporabniki>` - Sign in/out operations
- `RoleManager<IdentityRole>` - Role management

Example from Pages/Account/Login.cshtml.cs:
```csharp
private readonly SignInManager<Uporabniki> _signInManager;

public async Task<IActionResult> OnPostAsync()
{
    var result = await _signInManager.PasswordSignInAsync(
        Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
    // ...
}
```

## Debugging

- Development environment uses detailed error pages
- Connection string in `appsettings.Development.json` for local dev
- Logging configured in appsettings.json (Microsoft.* at Warning level)
- SQLite database file created automatically on first run if migrations applied

## Deployment

The application deploys automatically via GitHub Actions when pushing to tracked branches. Manual deployment:

```bash
# Build Docker image
docker build -t nepremicnine .

# Test locally
docker run -p 8080:8080 nepremicnine

# Push to Docker Hub
docker tag nepremicnine borci1417/nepremicnine:latest
docker push borci1417/nepremicnine:latest
```

Azure deployment uses publish profile secret `AZURE_WEBAPP_PUBLISH_PROFILE_TEST`.
