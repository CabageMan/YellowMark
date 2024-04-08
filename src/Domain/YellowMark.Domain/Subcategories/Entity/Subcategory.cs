using YellowMark.Domain.Ads.Entity;
using YellowMark.Domain.Base;
using YellowMark.Domain.Categories.Entity;

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

    /// <summary>
    /// Parent category <see cref="Category"/>
    /// </summary>
    public virtual Category Category { get; set; }

    /// <summary>
    /// Collection of ads.
    /// </summary>
    public virtual List<Ad> Ads { get; set; }
}