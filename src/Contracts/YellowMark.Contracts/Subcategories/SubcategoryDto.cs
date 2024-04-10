
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
    /// Subcategory creation date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Subcategory update date.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Subcategory name.
    /// </summary>
    public string Name { get; set; }
}