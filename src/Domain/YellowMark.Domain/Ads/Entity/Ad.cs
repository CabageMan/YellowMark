using YellowMark.Domain.Base;
using YellowMark.Domain.Comments.Entity;
using YellowMark.Domain.Currencies.Entity;
using YellowMark.Domain.Subcategories.Entity;
using YellowMark.Domain.Users.Entity;

namespace YellowMark.Domain.Ads.Entity;

/// <summary>
/// Class for Ad.
/// </summary>
public class Ad : BaseEntity
{
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
    /// Goods price specified in the ad.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Ad owner (<see cref="User"/>) id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Ad owner instance.
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// Ad Subcategory id <see cref="Guid"/>.
    /// </summary>
    public Guid SubcategoryId { get; set; }

    /// <summary>
    /// Ad <see cref="Subcategory"/>.
    /// </summary>
    public virtual Subcategory Subcategory { get; set; }

    /// <summary>
    /// Goods Currency id specified in the ad (<see cref="Guid"/>). 
    /// </summary>
    public Guid CurrencyId { get; set; }

    /// <summary>
    /// Goods <see cref="Currency"/> specified in the ad. 
    /// </summary>
    public virtual Currency Currency { get; set; }

    /// <summary>
    /// Collection of comments <see cref="Comment"/>.
    /// </summary>
    public virtual List<Comment> Comments { get; set; }
}