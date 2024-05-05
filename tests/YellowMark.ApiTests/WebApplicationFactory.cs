using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using YellowMark.Api;
using YellowMark.ApiTests.Categories;
using YellowMark.AppServices.Categories.Repositories;

namespace YellowMark.ApiTests;

/// <summary>
/// WebApplicationFactory class.
/// </summary>
public class WebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddScoped<ICategoryRepository, CategoryRepositoryStub>();
            services.AddScoped<IDistributedCache, MemoryDistributedCache>();
        });
    }
}
