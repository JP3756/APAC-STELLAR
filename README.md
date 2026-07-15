# APAC Stellar 2026

APAC Stellar 2026 is a modern fintech and Stellar-powered backend built with ASP.NET Core, Entity Framework Core, PostgreSQL, JWT authentication, and the Stellar SDK. The project includes a polished prototype page and a REST API for accounts, institutions, loans, investments, wallet operations, payments, and transactions.

## Creators

Built by:

- Cabaluna, John Paolo
- Evangelista, Jess Matthew
- Santos, Carl Joshua
- Sucgang, Jake

## What This App Does

- Handles user registration and authentication with ASP.NET Identity and JWT.
- Manages connected accounts, financial institutions, investments, loans, wallets, payments, and transactions.
- Integrates Stellar network support for asset and wallet operations.
- Sends email notifications through SMTP.
- Exposes API documentation through OpenAPI and Scalar in development.
- Includes rate limiting, CORS, and role-based authorization.

## Stack

- ASP.NET Core 9
- Entity Framework Core
- PostgreSQL
- ASP.NET Identity
- JWT Bearer authentication
- AutoMapper
- MailKit
- Stellar .NET SDK
- Scalar API reference

## Quick Actions

| Action | Command |
| --- | --- |
| Restore packages | `dotnet restore backend/ApacStellar2026.csproj` |
| Run the API | `dotnet run --project backend/ApacStellar2026.csproj` |
| Build the API | `dotnet build backend/ApacStellar2026.csproj` |
| Open API docs | Run in Development, then open the Scalar/OpenAPI endpoint |

## Project Layout

- `repo/index.html` - front-end prototype entry point
- `repo/backend` - ASP.NET Core API
- `repo/backend/Controllers` - REST controllers
- `repo/backend/Models` - domain models, DTOs, and enums
- `repo/backend/Services` - application services
- `repo/backend/Migrations` - EF Core migrations

## Getting Started

1. Install the .NET 9 SDK.
2. Install PostgreSQL and create a database for the app.
3. Update `backend/appsettings.Development.json` or use user secrets for your local connection string, JWT settings, SMTP settings, and Stellar configuration.
4. Run the database migrations if needed.
5. Start the API with the run command above.

## Configuration

The backend expects these settings:

- `ConnectionStrings:DefaultConnection`
- `Jwt:Key`, `Jwt:Issuer`, and `Jwt:Audience`
- `StellarSettings:TestnetUrl`, `StellarSettings:MainnetUrl`, `StellarSettings:Network`, and `StellarSettings:EncryptionKey`
- `Smtp:Host`, `Smtp:Port`, `Smtp:Username`, `Smtp:Password`, `Smtp:FromAddress`, `Smtp:FromName`, and `Smtp:UseSsl`

## API Highlights

- `/health` for a simple health check
- Identity endpoints from `MapIdentityApi<ApplicationUser>()`
- Controllers for authentication, connected accounts, financial institutions, investments, loans, Stellar assets, Stellar payments, Stellar wallets, and transactions

## Notes

- Development mode enables OpenAPI and Scalar automatically.
- The API uses rate limiting and role policies for protected endpoints.
- Keep secrets out of source control when moving from local development to production.
