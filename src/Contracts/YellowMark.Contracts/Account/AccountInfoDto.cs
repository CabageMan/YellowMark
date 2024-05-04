namespace YellowMark.Contracts.Account;

/// <summary>
/// Account info Data Transfer Object.
/// </summary>
public class AccountInfoDto
{
    /// <summary>
    /// User Info record identifier. 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// User middle name.
    /// </summary>
    public string MiddleName { get; set; }

    /// <summary>
    /// User last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// User full name. 
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// User email. 
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// User phone. 
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// User birth date.
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// User roles.
    /// </summary>
    public List<string> UserRoles { get; set; }
}
