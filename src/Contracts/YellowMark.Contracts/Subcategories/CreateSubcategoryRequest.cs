namespace YellowMark.Contracts.Subcategories;

/// <summary>
/// Request data model for subcategory creation.
/// </summary>
public class CreateSubcategoryRequest
{
    /// <summary>
    /// Subcategory name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Parent category id.
    /// </summary>
    public Guid CategoryId { get; set; }
}
