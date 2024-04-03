using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YellowMark.DbMigrator.DatabaseContext;

namespace YellowMark.DbMigrator;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
                { services.AddServices(hostContext.Configuration); }
            ).Build();
        await MigrateDatabaseAsync(host.Services);
        await host.StartAsync();
    }

    private static async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var writeContext = scope.ServiceProvider.GetRequiredService<MigrationWriteDbContext>();
        await writeContext.Database.MigrateAsync();

        var readContext = scope.ServiceProvider.GetRequiredService<MigrationReadDbContext>();
        await readContext.Database.MigrateAsync();
    }
}
