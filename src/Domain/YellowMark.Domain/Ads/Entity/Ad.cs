using YellowMark.Domain.Base;

namespace YellowMark.Domain.Ads.Entity;

/// <summary>
/// Class for Ad.
/// </summary>
public class Ad : BaseEntity
{
    /// <summary>
    /// Ad's owner (user) id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Ad's category <see cref="CategoryDto"/>
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Ad's subcategory <see cref="SubcategoryDto"/>
    /// </summary>
    public string Subcategory { get; set; }

    /// <summary>
    /// Ad's decription.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Goods currency specified in the ad. 
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Goods price specified in the ad.
    /// </summary>
    public double Price { get; set; }
}