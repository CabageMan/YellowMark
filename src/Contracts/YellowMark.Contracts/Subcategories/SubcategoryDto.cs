
namespace YellowMark.Contracts.Subcategories;

/// <summary>
/// Subcategory data transfer object.
/// </summary>
public class SubcategoryDto
{
    /// <summary>
    /// Subcategory Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Subcategory name.
    /// </summary>
    public string Name { get; set; }
}