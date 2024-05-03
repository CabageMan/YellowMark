namespace YellowMark.Contracts.Categories;

/// <summary>
/// Request data model for category creation.
/// </summary>
public class CreateCategoryRequest
{
    /// <summary>
    /// Category name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Parent category id. Null for root category.
    /// </summary>
    public Guid? ParentCategoryId { get; set; }
}
