using YellowMark.Domain.Base;

namespace YellowMark.Domain.Users.Entity;

/// <summary>
/// Class for User.
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
}