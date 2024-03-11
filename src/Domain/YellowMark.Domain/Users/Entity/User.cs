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
}