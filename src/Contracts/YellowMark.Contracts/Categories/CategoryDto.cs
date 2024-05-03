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
    /// Subcategories collection <see cref="CategoryDto"/>.
    /// </summary>
    public virtual List<CategoryDto> Subcategories { get; set; }
}