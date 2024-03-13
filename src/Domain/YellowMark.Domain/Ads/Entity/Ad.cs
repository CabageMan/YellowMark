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

    // Investigate a better way to store images.
    // - Store in data base.
    // - Store in S3 and get Image URL. For example https://min.io/
    // - Store with MongoDB GridFS https://www.mongodb.com/docs/manual/core/gridfs/.
    // See User.cs Avatar
    // public Image Image { get; set; }

    /// <summary>
    /// Goods <see cref="Currency"/> id specified in the ad. 
    /// </summary>
    public Guid CurrencyId { get; set; }

    /// <summary>
    /// Goods price specified in the ad.
    /// </summary>
    public double Price { get; set; }
}