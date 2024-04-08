using YellowMark.Domain.Base;
using YellowMark.Domain.Subcategories.Entity;

namespace YellowMark.Domain.Categories.Entity;

public class Category : BaseEntity
{
    /// <summary>
    /// Category name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Subcategories collection <see cref="Subcategory"/>.
    /// </summary>
    public virtual List<Subcategory> Subcategories { get; set; }
}