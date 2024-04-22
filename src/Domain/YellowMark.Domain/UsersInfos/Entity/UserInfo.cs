using YellowMark.Domain.Accounts.Entity;
using YellowMark.Domain.Ads.Entity;
using YellowMark.Domain.Base;
using YellowMark.Domain.Comments.Entity;

namespace YellowMark.Domain.UsersInfos.Entity;

/// <summary>
/// User entity.
/// </summary>
public class UserInfo : BaseEntity
{
    /// <summary>
    /// Account id related to the current user info.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// Account related to the current user info.
    /// </summary>
    public virtual Account Account { get; set; }

    /// <summary>
    /// User's first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// User's middle name.
    /// </summary>
    public string MiddleName { get; set; }

    /// <summary>
    /// User's last name.
    /// </summary>
    public string LastName { get; set; }

    // See Ad.cs Image
    // public Image Avatar { get; set; }

    /// <summary>
    /// User's birth date.
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Collection of ads.
    /// </summary>
    public virtual List<Ad> Ads { get; set; }

    /// <summary>
    /// Collection of comments <see cref="Comment"/>.
    /// </summary>
    public virtual List<Comment> Comments { get; set; }
}