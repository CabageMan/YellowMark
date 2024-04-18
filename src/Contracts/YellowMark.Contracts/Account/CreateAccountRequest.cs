namespace YellowMark.Contracts.Account;

/// <summary>
/// Request data model for registration.
/// </summary>
public class CreateAccountRequest
{
    /// <summary>
    /// Account email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Account Phone number.
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Account password.
    /// </summary>
    public string Password { get; set; } // TODO: Remove in future authentication with password and authentication with email/phone. 
}
