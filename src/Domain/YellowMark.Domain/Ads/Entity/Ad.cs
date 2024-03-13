using YellowMark.Domain.Base;
using YellowMark.Domain.Categories.Entity;
using YellowMark.Domain.Subcategories.Entity;

namespace YellowMark.Domain.Ads.Entity;

/// <summary>
/// Class for Ad.
/// </summary>
public class Ad : BaseEntity
{
    /// <summary>
    /// Ad owner (<see cref="User"/>) id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Ad <see cref="Category"/> id.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Ad <see cref="Subcategory"/> id.
    /// </summary>
    public Guid SubcategoryId { get; set; }

    /// <summary>
    /// Ad title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Ad decription.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Goods <see cref="Currency"/> id specified in the ad. 
    /// </summary>
    public Guid CurrencyId { get; set; }

    /// <summary>
    /// Goods price specified in the ad.
    /// </summary>
    public double Price { get; set; }
}