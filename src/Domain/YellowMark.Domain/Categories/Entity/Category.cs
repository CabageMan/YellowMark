using YellowMark.Domain.Base;

namespace YellowMark.Domain.Categories.Entity;

public class Category : BaseEntity
{
    /// <summary>
    /// Category name.
    /// </summary>
    public string Name { get; set; }
}