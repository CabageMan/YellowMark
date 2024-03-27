using Microsoft.EntityFrameworkCore;

namespace YellowMark.DataAccess.YellowMarkDbContext;

/// <summary>
/// Data base context configurator
/// </summary>
/// <typeparam name="TContext"></typeparam>
public interface IDbContextOptionsConfigurator<TContext> where TContext : DbContext
{
    /// <summary>
    /// Make context configuration.
    /// </summary>
    /// <param name="optionsBuilder"><see cref="DbContextOptionsBuilder"/></param>
    void Configure(DbContextOptionsBuilder<TContext> optionsBuilder);
}
