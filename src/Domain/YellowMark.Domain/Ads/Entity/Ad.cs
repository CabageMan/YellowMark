using YellowMark.Domain.Base;
using YellowMark.Domain.Categories.Entity;
using YellowMark.Domain.Comments.Entity;
using YellowMark.Domain.Currencies.Entity;

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
    /// Ad owner (<see cref="UserInfo"/>) id.
    /// </summary>
    public Guid UserInfoId { get; set; }

    /// <summary>
    /// Ad owner instance.
    /// </summary>
    public virtual Domain.UsersInfos.Entity.UserInfo UserInfo { get; set; }

    /// <summary>
    /// Ad Category id <see cref="Guid"/>.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Ad <see cref="Category"/>.
    /// </summary>
    public virtual Category Category { get; set; }

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