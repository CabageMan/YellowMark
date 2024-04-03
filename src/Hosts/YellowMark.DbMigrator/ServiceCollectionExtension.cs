using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using YellowMark.DbMigrator.DatabaseContext;

namespace YellowMark.DbMigrator;

/// <summary>
/// Extension class for <see cref="IServiceCollection"/> to add dependecies.
/// </summary>
public static class ServiceCollectionExtension
{
    private const string PostgressWriteConnectionStringName = "WriteDB";
    private const string PostgressReadConnectionStringName = "ReadDB";

    /// <summary>
    /// Configure database context for migration.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    /// <returns></returns>
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var writeConnectionString = configuration
            .GetConnectionString(PostgressWriteConnectionStringName);
        var readConnectionString = configuration
            .GetConnectionString(PostgressReadConnectionStringName);

        if (string.IsNullOrEmpty(writeConnectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{PostgressWriteConnectionStringName}' not found."
            );
        }
        if (string.IsNullOrEmpty(readConnectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{PostgressReadConnectionStringName}' not found."
            );
        }

        var dbUserName = configuration.GetSection("DbConnection")["Username"];
        var dbPassword = configuration.GetSection("DbConnection")["Password"]; 

        var writeConnectionStringBuilder = new NpgsqlConnectionStringBuilder(writeConnectionString)
        {
            Username = dbUserName,
            Password = dbPassword
        };
        var readConnectionStringBuilder = new NpgsqlConnectionStringBuilder(readConnectionString)
        {
            Username = dbUserName,
            Password = dbPassword
        };

        services.AddDbContext<MigrationWriteDbContext>(options =>
            options.UseNpgsql(writeConnectionStringBuilder.ConnectionString)
        );
        services.AddDbContext<MigrationReadDbContext>(options =>
            options.UseNpgsql(readConnectionStringBuilder.ConnectionString)
        );

        return services;
    }
}
