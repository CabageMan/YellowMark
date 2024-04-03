# YellowMark

A .Net application implementing the notice board functionality for SolarLab's .Net educational course.

## Running

**Application uses User-Secrets in Development mode.**
```
{
    "DbConnection": {
        "Username": <username>,
        "Password": <password>
    }
}
```
User secrets are used by YellowMark.Api and YellowMark.DbMigrator projects to access to database, so they should have the same UserSecretsId fields (described in .cproj files).

### Linux
In the src/Hosts/YellowMark.Api directory run:

```
dotnet run --environment <Environment>
```

For development mode it's important to pass 'Development' as --environment argument to use sensitive data from User Secrets.


## Database Migrations

Application uses **two** databases: for writing and reading. So it's needed create two migrations respectively. First for write db context and second for read.

### Linux

**To make new migration in the YellowMark.DbMigrator project directory run:**

```
dotnet ef migrations add <migration_name> --project YellowMark.DbMigrator.csproj --context <DbContext>
```
As DbContext for --context argument you should pass MigrationWriteDbContext and MigrationReadDbContext.

**To add changes to database just run the YellowMark.DbMigrator project:**

```
dotnet run --environment <Environment>
```

For development mode it's important to pass 'Development' as --environment argument to use sensitive data from User Secrets.