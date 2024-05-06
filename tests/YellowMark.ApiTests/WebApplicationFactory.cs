using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using YellowMark.Api;
using YellowMark.DataAccess.DatabaseContext;

namespace YellowMark.ApiTests;

/// <summary>
/// WebApplicationFactory class.
/// </summary>
public class WebApplicationFactory : WebApplicationFactory<Program>
{ 
    private static InMemoryDatabaseRoot? _databaseRoot;

    /// <summary>
    /// Root database <see cref="InMemoryDatabaseRoot"/>
    /// </summary>
    public InMemoryDatabaseRoot DatabaseRoot
    {
        get
        {
            return _databaseRoot ??= new InMemoryDatabaseRoot();
        }
    }

    /// <inheritdoc/>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            /*
            // Mock repository to test without database
            services.AddScoped<ICategoryRepository, CategoryRepositoryStub>();
            */

            services.AddScoped<IDistributedCache, MemoryDistributedCache>();

            RemoveDescriptor<DbContextOptions<WriteDbContext>>(services);
            RemoveDescriptor<DbContextOptions<ReadDbContext>>(services);

            string inMemoryDatabaseName = "YellowMark";
            services.AddDbContext<WriteDbContext>((container, options) =>
            {
                options.UseInMemoryDatabase(inMemoryDatabaseName, DatabaseRoot);
            });
            services.AddDbContext<ReadDbContext>((container, options) =>
            {
                options.UseInMemoryDatabase(inMemoryDatabaseName, DatabaseRoot);
            });
        });
    }

    // Helpers:
    private static void RemoveDescriptor<T>(IServiceCollection services)
    {
        var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T));
        if (dbContextDescriptor != null)
        {
            services.Remove(dbContextDescriptor);
        }
    }
}
