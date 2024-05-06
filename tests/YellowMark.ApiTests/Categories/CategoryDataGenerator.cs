using Bogus;
using YellowMark.Domain.Categories.Entity;

namespace YellowMark.ApiTests.Categories;

/// <summary>
/// Test data generator.
/// </summary>
public static class CategoryDataGenerator
{
    private static Faker<Category> _categoryFaker = new Faker<Category>()
        .StrictMode(false)
        .RuleFor(c => c.Id, _ => Guid.NewGuid())
        .RuleFor(c => c.Name, (f, c) => f.Commerce.Categories(1)[0]);

    /// <summary>
    /// Generate random categories.
    /// </summary>
    /// <param name="number">The number of generated categories.</param>
    /// <returns>Collection of the categories.</returns>
    public static IEnumerable<Category> GetRandomCategories(int number)
    {
        return _categoryFaker.Generate(number);
    }
}
