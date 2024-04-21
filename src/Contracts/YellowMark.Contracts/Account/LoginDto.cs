namespace YellowMark.Contracts;

/// <summary>
/// Data Transfer Object for login info.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Account (user) id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Result of sign in.
    /// </summary>
    public string JwtToken { get; set; }

    /// <summary>
    /// Account (user) role.
    /// </summary>
    public string Role { get; set; }
}
