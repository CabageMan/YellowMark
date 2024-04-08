using YellowMark.Domain.Ads.Entity;
using YellowMark.Domain.Base;
using YellowMark.Domain.Comments.Entity;

namespace YellowMark.Domain.Users.Entity;

/// <summary>
/// User entity.
/// </summary>
public class User : BaseEntity
{
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

    // Investigate a better way to store images.
    // - Store in data base.
    // - Store in S3 and get Image URL. For example https://min.io/
    // - Store with MongoDB GridFS https://www.mongodb.com/docs/manual/core/gridfs/.
    // See Ad.cs Image
    // public Image Avatar { get; set; }

    /// <summary>
    /// User's email.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// User's phone.
    /// </summary>
    public string Phone { get; set; }

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