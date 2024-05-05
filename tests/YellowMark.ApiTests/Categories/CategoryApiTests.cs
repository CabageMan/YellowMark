using System.Net;
using System.Net.Http.Json;
using YellowMark.Contracts.Categories;

namespace YellowMark.ApiTests.Categories;

/// <summary>
/// Category API tets class.
/// </summary>
public class CategoryApiTests : IClassFixture<WebApplicationFactory>
{
    private readonly WebApplicationFactory _webApplicationFactory;

    /// <summary>
    /// Constructor for CategoryApiTests class.
    /// </summary>
    /// <param name="webApplicationFactory"></param>
    public CategoryApiTests(WebApplicationFactory webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Fact]
    public async Task ShouldSuccess_GetAll()
    {
        var httpClient = _webApplicationFactory.CreateClient();

        var response = await httpClient.GetAsync("api/v1/categories", CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var categories = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryDto>>();
        Assert.NotNull(categories);

        var factCategories = categories.ToArray();
        for (int i = 0; i < factCategories.Length - 1; i++)
        {
            var factCategory = factCategories[i];
            var expectedCategory = CategoryRepositoryStub.AllCategories[i];

            Assert.Equal(expectedCategory.Id, factCategory.Id);
            Assert.Equal(expectedCategory.Name, factCategory.Name);
        }
    }
}
