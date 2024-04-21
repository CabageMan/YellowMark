using Microsoft.AspNetCore.Identity;

namespace YellowMark.Domain.Accounts.Entity;

/// <summary>
/// Class for Account entity.
/// </summary>
public class Account : IdentityUser
{
    /// <summary>
    /// Account role.
    /// </summary>
    // public string Role { get; set; }

    /// <summary>
    /// User info related to account.
    /// </summary>
    public virtual Domain.Users.Entity.User User { get; set; }
}
