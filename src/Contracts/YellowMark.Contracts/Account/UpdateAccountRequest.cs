namespace YellowMark.Contracts.Account;

/// <summary>
/// Update account info request.
/// </summary>
public class UpdateAccountRequest
{
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
    /// User email. 
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// User phone. 
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Set user phone visibility to other users in ads. Default is false.
    /// </summary>
    public bool ShowPhone { get; set; } = false;

    /// <summary>
    /// User birth date.
    /// </summary>
    public DateOnly BirthDate { get; set; }
}
