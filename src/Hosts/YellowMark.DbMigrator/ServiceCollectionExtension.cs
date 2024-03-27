using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace YellowMark.DbMigrator;

/// <summary>
/// Extension class for <see cref="IServiceCollection"/> to add dependecies.
/// </summary>
public static class ServiceCollectionExtension
{
    private const string PostgressWriteConnectionStringName = "WriteDB";
    // TODO: Investigate how migrate read and whrite databases and how replicate them!
    // For now use only write DB
    /*
    private const string PostgressReadConnectionStringName = "ReadDB";
    */

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

        /*
        var readConnectionString = configuration
            .GetConnectionString(PostgressReadConnectionStringName);
        */

        if (string.IsNullOrEmpty(writeConnectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{PostgressWriteConnectionStringName}' not found."
            );
        }

        /*
        if (string.IsNullOrEmpty(readConnectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{PostgressReadConnectionStringName}' not found."
            );
        }
        */

        services.AddDbContext<MigrationDbContext>(options => options.UseNpgsql(writeConnectionString));

        return services;
    }
}
