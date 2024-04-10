using YellowMark.Contracts.Subcategories;

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
    /// Category creation date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Category update date.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Category name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Subcategories collection <see cref="SubcategoryDto"/>.
    /// </summary>
    public virtual List<SubcategoryDto> Subcategories { get; set; }
}