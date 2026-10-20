# Project Moonshine

**A standalone, credentialing-focused CRM built with C# and ASP.NET Core.**

Project Moonshine is an application foundation for organizations that administer credentialing programs. Rather than treating credentials as ordinary CRM attributes, its data model separates the credential awarded to a customer from the processes and individual requirements involved in administering it.

The repository contains an ASP.NET Core MVC web application, an Entity Framework Core data layer, and a test project. It represents an early implementation and is **not currently documented or verified as production-ready**.

## Credentialing model

The underlying domain model distinguishes:

- **Customers and people:** Core CRM records and associated addresses, email addresses, and phone numbers.
- **Credential types and credentials:** Definitions of credentials and records of credentials associated with customers.
- **Credential process types and processes:** Definitions of credentialing activities and process records linked to credentials.
- **Requirement types and requirements:** Reusable definitions of requirements and individual requirement records associated with a credential process.
- **Status types:** Status classifications referenced by credentials, credential processes, and individual requirements.

Conceptually:

```text
Customer / Person
       |
    Credential ---- Credential Type
       |
 Credential Process ---- Process Type
       |
 Credential Requirement ---- Requirement Type
       |
    Status Type
```

This is a conceptual overview, not a complete ER diagram. The source also contains association models for credential types, process types, and requirement types.

## Implemented application areas

The MVC application includes controllers for:

- Customers and people
- Addresses, email addresses, and phone numbers
- Credential types and credentials

The credential controller provides list, detail, create, edit, and delete actions. The data layer defines additional credentialing entities and relationships, but the presence of a model **does not imply that an end-to-end administrative workflow has been implemented**.

## Technology stack

| Component | Technology |
| --- | --- |
| Language | C# |
| Web application | ASP.NET Core MVC 2.1, Razor views |
| Data access | Entity Framework Core 2.1 |
| Database provider | Microsoft SQL Server |
| Database evolution | EF Core migrations |
| Tests | xUnit test project |

## Solution structure

```text
ProjectMoonshine.sln
├── ProjectMoonshine/                  # ASP.NET Core MVC web application
│   ├── Controllers/                  # MVC controllers
│   ├── Managers/                     # Application/service layer
│   ├── Views/                        # Razor views
│   ├── Program.cs
│   └── Startup.cs
├── ProjectMoonshine.EntityFramework/  # EF Core context, entities, migrations
│   ├── Models/
│   ├── Migrations/
│   └── ApplicationDbContext.cs
└── ProjectMoonshine.Tests/           # Test project
```

## Local development

> **Legacy runtime warning:** This solution targets .NET Core 2.1, which is out of support. The code may require an older toolchain and dependency remediation before it can be restored or run reliably. Do not deploy it to the public internet without upgrading, reviewing, and testing it.

1. Clone the repository:

   ```bash
   git clone https://github.com/BobHenegar/ProjectMoonshine.git
   cd ProjectMoonshine
   ```

2. Open `ProjectMoonshine.sln` in a compatible development environment.
3. Provision an isolated SQL Server development database.
4. Configure the `ConnectionStrings:Database` setting using a secure, **untracked** configuration source. Never commit live credentials.
5. Review the startup migration logic **before pointing the application at any database containing data**. The current `Startup.cs` can call `EnsureDeleted()` if it does not find the expected initial migration.
6. Restore dependencies, apply necessary compatibility fixes, and run the application in an isolated development environment.

The exact setup has not been validated against a clean machine. An upgrade to a supported .NET version is recommended before further development or deployment.

## Development status and limitations

This repository is best understood as a legacy credentialing CRM prototype or application foundation.

- Core CRM and credential CRUD infrastructure is present.
- The domain model includes credential processes, requirements, and statuses.
- Some workflow-related properties remain commented out.
- The requirement manager does not yet demonstrate a complete implementation.
- The included unit test project does not establish comprehensive automated coverage.
- Authentication, authorization, and production hardening have not been verified.

## Security notice

**Security review is required before deployment.** Historical tracked configuration includes a database connection string with embedded credentials. Any exposed password must be rotated, removed from active configuration, and addressed in repository history as appropriate. Removing a secret from the latest commit alone does not erase it from prior Git history.

Additionally, startup code includes potentially destructive database initialization behavior. Use only a disposable database until this logic has been reviewed and corrected.

## License

No license is asserted by this README. Refer to the repository for any subsequently added license terms.
