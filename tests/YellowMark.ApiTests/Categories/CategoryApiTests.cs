using System.Data;
using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using YellowMark.Contracts.Categories;
using YellowMark.DataAccess.DatabaseContext;

namespace YellowMark.ApiTests.Categories;

/// <summary>
/// Category API tets class.
/// </summary>
public class CategoryApiTests : IClassFixture<WebApplicationFactory>
{
    private readonly WebApplicationFactory _webApplicationFactory;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Constructor for CategoryApiTests class.
    /// </summary>
    /// <param name="webApplicationFactory">INJECTED WebApplicationFactory. The same for all tets.</param>
    public CategoryApiTests(WebApplicationFactory webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
        _httpClient = _webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task ShouldSuccess_GetAll()
    {
        var rawCategories = CategoryDataGenerator.GetRandomCategories(10).ToArray();
        using (var serviceScope = _webApplicationFactory.Services.CreateScope()) 
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ReadDbContext>();
            await dbContext.InitializeWithAsync(rawCategories);
        }

        var response = await _httpClient.GetAsync("api/v1/categories", CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var categories = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryDto>>();
        Assert.NotNull(categories);
        Assert.Equal(10, categories.Count());
        Assert.Contains(categories, c => rawCategories.Any(rc => rc.Id == c.Id && rc.Name == c.Name));

    }

    [Fact]
    public async Task ShouldSuccess_GetCategoryById()
    {
        var rawCategories = CategoryDataGenerator.GetRandomCategories(10).ToArray();
        var categoryToFind = rawCategories[5];
        using (var serviceScope = _webApplicationFactory.Services.CreateScope()) 
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ReadDbContext>();
            await dbContext.InitializeWithAsync(rawCategories);
        }

        var response = await _httpClient.GetAsync($"api/v1/categories/{categoryToFind.Id.ToString()}", CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var category = await response.Content.ReadFromJsonAsync<CategoryDto>();
        Assert.NotNull(category);
        Assert.Equal(categoryToFind.Id, category.Id);
        Assert.Equal(categoryToFind.Name, category.Name);
    }
}
