namespace YellowMark.Contracts.Users;

/// <summary>
/// Request data model for user creation.
/// </summary>
public class CreateUserRequest
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
}
