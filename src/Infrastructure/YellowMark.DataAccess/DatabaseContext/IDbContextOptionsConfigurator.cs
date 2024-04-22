using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace YellowMark.DataAccess.DatabaseContext;

/// <summary>
/// Database context configurator.
/// </summary>
/// <typeparam name="TContext">DbContext type inherited from <see cref="IdentityDbContext"/></typeparam>
public interface IDbContextOptionsConfigurator<TContext> where TContext : IdentityDbContext<YellowMark.Domain.Accounts.Entity.Account, IdentityRole<Guid>, Guid>
{
    /// <summary>
    /// Make context configuration.
    /// </summary>
    /// <param name="optionsBuilder"><see cref="DbContextOptionsBuilder"/></param>
    void Configure(DbContextOptionsBuilder<TContext> optionsBuilder);
}
