using YellowMark.Domain.Base;
using YellowMark.Domain.Comments.Entity;
using YellowMark.Domain.Currencies.Entity;
using YellowMark.Domain.Subcategories.Entity;

namespace YellowMark.Domain.Ads.Entity;

/// <summary>
/// Class for Ad Entity.
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

    /// <summary>
    /// Collection of comments <see cref="Domain.Files.Entity.File"/>.
    /// </summary>
    public virtual List<Domain.Files.Entity.File> Files { get; set; }

    /// <summary>
    /// Goods price specified in the ad.
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// Ad owner (<see cref="User"/>) id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Ad owner instance.
    /// </summary>
    public virtual Domain.Users.Entity.User User { get; set; }

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
    public Guid? CurrencyId { get; set; }

    /// <summary>
    /// Goods <see cref="Currency"/> specified in the ad. 
    /// </summary>
    public virtual Currency Currency { get; set; }

    /// <summary>
    /// Collection of comments <see cref="Comment"/>.
    /// </summary>
    public virtual List<Comment> Comments { get; set; }
}