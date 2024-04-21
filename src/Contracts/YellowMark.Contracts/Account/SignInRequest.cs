namespace YellowMark.Contracts.Account;

/// <summary>
/// Request data model for sign in.
/// </summary>
public class LoginAccountRequest
{

    /// <summary>
    /// Account email.
    /// </summary>
    public string Email { get; set; } // Logging by email and password

    /// <summary>
    /// Account password.
    /// </summary>
    public string Password { get; set; } // TODO: Remove in future authentication with password and authentication with email/phone. 
}
