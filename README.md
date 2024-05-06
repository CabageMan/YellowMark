# YellowMark

A .Net application implementing the notice board functionality for SolarLab's .Net educational course.

## Running

**Application uses User-Secrets in Development mode.**
```
{
    "DbConnection": {
        "Username": <username>,
        "Password": <password>
    },
    "Jwt": {
        "SecretKey": <secret_key>
    }
}
```
User secrets are used by YellowMark.Api and YellowMark.DbMigrator projects to access to database and storing JWT secret key, so they should have the same UserSecretsId fields (described in .cproj files).

### Linux
Just run from IDE or in the src/Hosts/YellowMark.Api directory run:

```
dotnet run --environment <Environment>
```

For development mode it's important to pass 'Development' as --environment argument to use sensitive data from User Secrets.


## Database Migrations

Application uses database with replication. Primary database is used for write and read operations (mostly write), and replica - for read only. 
To run databases in Docker see
[repository](https://github.com/eremeykin/pg-primary-replica?source=post_page-----98c48f233bbf--------------------------------)

### Linux

**To make new migration in the YellowMark.DbMigrator project directory run:**

```
dotnet ef migrations add <migration_name> --project YellowMark.DbMigrator.csproj --context <DbContext>
```

MigrationDbContextFactory is implemented to apply different database contexts, but for now is available only one, so as DbContext for --context argument you should pass MigrationDbContext.

**To add changes to database you need to run the YellowMark.DbMigrator project:**\
Run it from IDE or from terminal using:

```
dotnet run --environment <Environment>
```

For development mode it's important to pass 'Development' as --environment argument to use sensitive data from User Secrets nested from YellowMark.Api project.

## Setup chaching

To run redis container pull its image to docker and execute:

```
docker run -p 6379:6379 --name yellowmark-redis -d redis
```

## Setup ELK stack

To run ELK stack in docker see 
[repository](https://github.com/deviantony/docker-elk)

## Using

After successful start the application before using it need to add User Roles to database. For convinience the 'AddUsersRoles' is added to Account controller (endpoint: /api/v1/account/roles). Just call it to add roles to database.\
To add 'Admin' and 'SuperUser' roles to logged in user there are two convenient methods: "api/v1/account/role/admin" and "api/v1/account/role/superuser".

