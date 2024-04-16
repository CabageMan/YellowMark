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

Application uses database with replication. Primary database is used for write and read operations (mostly write), and replica - for read only. 
To run databases in Docker see:
[repository](https://github.com/eremeykin/pg-primary-replica?source=post_page-----98c48f233bbf--------------------------------)

### Linux

**To make new migration in the YellowMark.DbMigrator project directory run:**

```
dotnet ef migrations add <migration_name> --project YellowMark.DbMigrator.csproj --context <DbContext>
```
MigrationDbContextFactory is implemented to apply different database contexts, but for now is available only one, so as DbContext for --context argument you should pass MigrationDbContext.

**To add changes to database just run the YellowMark.DbMigrator project:**

```
dotnet run --environment <Environment>
```

For development mode it's important to pass 'Development' as --environment argument to use sensitive data from User Secrets.