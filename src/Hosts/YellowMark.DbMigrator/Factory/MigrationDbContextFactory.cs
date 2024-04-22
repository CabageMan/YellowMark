using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace YellowMark.DbMigrator.Factory;

/// <summary>
/// Generic migration database context factory.
/// </summary>
public class MigrationDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : IdentityDbContext<YellowMark.Domain.Accounts.Entity.Account, IdentityRole<Guid>, Guid>
{
    private const string ConnectionStringName = "WriteDB";

    /// <inheritdoc />
    public TContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddUserSecrets(Assembly.GetExecutingAssembly());

        var configuration = builder.Build();

        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{ConnectionStringName}' not found."
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
