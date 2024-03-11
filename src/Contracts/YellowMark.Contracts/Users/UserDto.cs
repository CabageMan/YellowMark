namespace YellowMark.Contracts.Users;

/// <summary>
/// User Data Transfer Object.
/// </summary>
public class UserDto
{
    /// <summary>
    /// User record identifier. 
    /// </summary>
    public Guid Id { get; set; }

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

    /// <summary>
    /// User's full name. 
    /// </summary>
    public string FullName { get; set; }
}