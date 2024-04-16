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
    private const string PostgressConnectionStringName = "WriteDB";

    /// <summary>
    /// Configure database context for migration.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    /// <returns></returns>
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString(PostgressConnectionStringName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{PostgressConnectionStringName}' not found."
            );
        }

        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Username = configuration.GetSection("DbConnection")["Username"],
            Password = configuration.GetSection("DbConnection")["Password"]
        };

        services.AddDbContext<MigrationDbContext>(options =>
            options.UseNpgsql(connectionStringBuilder.ConnectionString)
        );

        return services;
    }
}
