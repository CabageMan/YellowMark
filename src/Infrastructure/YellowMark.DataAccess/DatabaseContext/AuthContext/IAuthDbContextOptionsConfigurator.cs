using Microsoft.EntityFrameworkCore;

namespace YellowMark.DataAccess.DatabaseContext.AuthContext;

/// <summary>
/// Identity database context configurator.
/// </summary>
public interface IAuthDbContextOptionsConfigurator
{
    /// <summary>
    /// Configure AuthDbContext.
    /// </summary>
    /// <param name="optionsBuilder"><see cref="DbContextOptionsBuilder"/></param>
    void Configure(DbContextOptionsBuilder<AuthDbContext> optionsBuilder);
}
