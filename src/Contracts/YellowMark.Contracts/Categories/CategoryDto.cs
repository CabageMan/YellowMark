namespace YellowMark.Contracts.Categories;

/// <summary>
/// Category data transfer object.
/// </summary>
public class CategoryDto
{
    /// <summary>
    /// Category Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Category name.
    /// </summary>
    public string Name { get; set; }

    // Think if it needed in the future.
    // public IEnumerable<Subcategory> Subcategories;
}