using YellowMark.Domain.Ads.Entity;
using YellowMark.Domain.Base;

namespace YellowMark.Domain.Categories.Entity;

public class Category : BaseEntity
{
    /// <summary>
    /// Category name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Parent category id. Null for root category.
    /// </summary>
    public Guid? ParentCategoryId { get; set; }

    /// <summary>
    /// Parent category. Null for root category.
    /// </summary>
    public virtual Category? ParentCategory { get; set; }

    /// <summary>
    /// Subcategories collection <see cref="Subcategory"/>.
    /// </summary>
    public virtual List<Category> Subcategories { get; set; }

    /// <summary>
    /// Collection of ads.
    /// </summary>
    public virtual List<Ad> Ads { get; set; }
}