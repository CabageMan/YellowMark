using YellowMark.Domain.Ads.Entity;
using YellowMark.Domain.Base;

namespace YellowMark.Domain.Comments.Entity;

/// <summary>
/// Class for Comment.
/// </summary>
public class Comment : BaseEntity
{
    /// <summary>
    /// Comment text.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Author's (User) Id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Author <see cref="User"/>
    /// </summary>
    public virtual Domain.UsersInfos.Entity.UserInfo UserInfo { get; set; }

    /// <summary>
    /// Ad Id.
    /// </summary>
    public Guid AdId { get; set; }

    /// <summary>
    /// <see cref="Ad"/>
    /// </summary>
    public virtual Ad Ad { get; set; }
}