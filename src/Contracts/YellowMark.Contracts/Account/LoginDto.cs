namespace YellowMark.Contracts.Account;

/// <summary>
/// Data Transfer Object for login info.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Related user info id.
    /// </summary>
    public Guid UserInfoId { get; set; }

    /// <summary>
    /// Result of sign in.
    /// </summary>
    public string JwtToken { get; set; }

    /// <summary>
    /// Account (user) role.
    /// </summary>
    public List<string> Roles { get; set; }
}
