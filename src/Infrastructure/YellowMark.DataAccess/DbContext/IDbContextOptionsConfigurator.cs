using Microsoft.EntityFrameworkCore;

namespace YellowMark.DataAccess.DatabaseContext;

/// <summary>
/// Database context configurator.
/// </summary>
/// <typeparam name="TContext">DbContext type inherited from <see cref="DbContext"/></typeparam>
public interface IDbContextOptionsConfigurator<TContext> where TContext : DbContext
{
    /// <summary>
    /// Make context configuration.
    /// </summary>
    /// <param name="optionsBuilder"><see cref="DbContextOptionsBuilder"/></param>
    void Configure(DbContextOptionsBuilder<TContext> optionsBuilder);
}
