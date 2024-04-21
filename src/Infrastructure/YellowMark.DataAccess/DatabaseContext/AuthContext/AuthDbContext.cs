using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace YellowMark.DataAccess.DatabaseContext.AuthContext;

/// <summary>
/// Identity database context.
/// </summary>
public class AuthDbContext : IdentityDbContext<YellowMark.Domain.Accounts.Entity.Account>
{
    /// <summary>
    /// Initialize an instance of <see cref="AuthDbContext"/>
    /// </summary>
    /// <param name="dbContextOptions"></param>
    public AuthDbContext(DbContextOptions<AuthDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
        // Database.EnsureCreated();
    }
}
