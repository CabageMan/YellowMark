# YellowMark

A .Net application implementing the notice board functionality for SolarLab's .Net educational course.

## Running

Application uses User-Secrets in Development mode.
```
{
    "DbConnection": {
        "Username": <username>,
        "Password": <password>
    }
}
```
User secrets are used by YellowMark.Api and YellowMark.DbMigrator projects to access to database, so they should have the same UserSecretsId fields.

### Linux
In the src/Hosts/YellowMark.Api directory run:

```
dotnet run --environment Development
```

## Database Migrations

### Linux

To make new migration in the YellowMark.DbMigrator project directory run:

```
dotnet ef migrations add Initial_Create --project YellowMark.DbMigrator.csproj
```

To add changes to database just run the YellowMark.DbMigrator project:

```
dotnet run --environment Development
```