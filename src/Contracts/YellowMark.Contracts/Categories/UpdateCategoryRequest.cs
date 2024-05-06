namespace YellowMark.Contracts.Categories;

/// <summary>
/// Request data model for category update.
/// </summary>
public class UpdateCategoryRequest
{
    /// <summary>
    /// Category to update request.
    /// </summary>
    public Guid Id { get; set; }   

    /// <summary>
    /// Category name.
    /// </summary>
    public string Name { get; set; }
}
