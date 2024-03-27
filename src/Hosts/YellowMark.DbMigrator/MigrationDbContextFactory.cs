using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace YellowMark.DbMigrator;

/// <summary>
/// Migration database context factory.
/// </summary>
public class MigrationDbContextFactory : IDesignTimeDbContextFactory<MigrationDbContext>
{

    private const string PostgressWriteConnectionStringName = "WriteDB";
    // TODO: Investigate how migrate read and whrite databases and how replicate them!
    // For now use only write DB
    /*
    private const string PostgressReadConnectionStringName = "ReadDB";
    */

    /// <inheritdoc />
    public MigrationDbContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var configuration = builder.Build();
        var connectionString = configuration.GetConnectionString(PostgressWriteConnectionStringName);

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<MigrationDbContext>();
        dbContextOptionsBuilder.UseNpgsql(connectionString);
        return new MigrationDbContext(dbContextOptionsBuilder.Options);
    }
}
