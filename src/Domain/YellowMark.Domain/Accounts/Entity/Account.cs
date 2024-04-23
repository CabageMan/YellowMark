using Microsoft.AspNetCore.Identity;

namespace YellowMark.Domain.Accounts.Entity;

/// <summary>
/// Class for Account entity.
/// </summary>
public class Account : IdentityUser<Guid>
{ }
