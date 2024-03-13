using YellowMark.Domain.Base;

namespace YellowMark.Domain.Subcategories.Entity;

/// <summary>
/// Class for subcategory.
/// </summary>
public class Subcategory : BaseEntity
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