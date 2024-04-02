using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql;
using YellowMark.DbMigrator.DbContext;

namespace YellowMark.DbMigrator.Factory;

/// <summary>
/// Migration database context factory.
/// </summary>
public class MigrationDbContextFactory : IDesignTimeDbContextFactory<MigrationWriteDbContext>
{

    private const string PostgressWriteConnectionStringName = "WriteDB";
    // TODO: Investigate how migrate read and whrite databases and how replicate them!
    // For now use only write DB
    /*
    private const string PostgressReadConnectionStringName = "ReadDB";
    */

    /// <inheritdoc />
    public MigrationWriteDbContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddUserSecrets(Assembly.GetExecutingAssembly());
        var configuration = builder.Build();

        var connectionString = configuration.GetConnectionString(PostgressWriteConnectionStringName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{PostgressWriteConnectionStringName}' not found."
            );
        }

        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Username = configuration.GetSection("DbConnection")["Username"],
            Password = configuration.GetSection("DbConnection")["Password"]
        };

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<MigrationWriteDbContext>();
        dbContextOptionsBuilder.UseNpgsql(connectionStringBuilder.ConnectionString);
        
        return new MigrationWriteDbContext(dbContextOptionsBuilder.Options);
    }
}
