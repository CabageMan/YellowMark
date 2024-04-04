using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql;
using YellowMark.DbMigrator.DatabaseContext;

namespace YellowMark.DbMigrator.Factory;

/// <summary>
/// Migration database context factory.
/// </summary>
public class MigrationDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
{

    private const string WriteConnectionStringName = "WriteDB";
    private const string ReadConnectionStringName = "ReadDB";

    /// <inheritdoc />
    public TContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddUserSecrets(Assembly.GetExecutingAssembly());

        var configuration = builder.Build();

        var connectionStringName = typeof(TContext) == typeof(MigrationWriteDbContext)
            ? WriteConnectionStringName
            : typeof(TContext) == typeof(MigrationReadDbContext)
            ? ReadConnectionStringName : "";

        var connectionString = configuration.GetConnectionString(connectionStringName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{connectionStringName}' not found."
            );
        }

        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Username = configuration.GetSection("DbConnection")["Username"],
            Password = configuration.GetSection("DbConnection")["Password"]
        };

        var optionsBuilder = new DbContextOptionsBuilder<TContext>();
        optionsBuilder.UseNpgsql(connectionStringBuilder.ConnectionString);

        var dbContext = (TContext?)Activator.CreateInstance(typeof(TContext), optionsBuilder.Options);

        if (dbContext == null)
        {
            throw new InvalidOperationException($"Could not create dbContext of type {typeof(TContext).Name}.");
        }

        return dbContext;
    }
}
