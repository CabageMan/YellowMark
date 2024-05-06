namespace YellowMark.ApiTests.Currencies;

/// <summary>
/// Currencies API tets class.
/// </summary>
public class CurrencyApiTests : IClassFixture<WebApplicationFactory>
{
    private readonly WebApplicationFactory _webApplicationFactory;

    /// <summary>
    /// Constructor for CurrenciesApiTests class.
    /// </summary>
    /// <param name="webApplicationFactory"></param>
    public CurrencyApiTests(WebApplicationFactory webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }
}
