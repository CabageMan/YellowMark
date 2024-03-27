using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace YellowMark.DbMigrator;

class Program
{
    static async void Main(string[] args)
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
        var context = scope.ServiceProvider.GetRequiredService<MigrationDbContext>();
        await context.Database.MigrateAsync();
    }
}
