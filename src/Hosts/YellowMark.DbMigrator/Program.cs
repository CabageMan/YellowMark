using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YellowMark.DbMigrator.DbContext;

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
        var context = scope.ServiceProvider.GetRequiredService<MigrationWriteDbContext>();
        await context.Database.MigrateAsync();
    }
}
