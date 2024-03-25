using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace YellowMark.DataAccess;

/// <summary>
/// Data Base context.
/// </summary>
public class YellowMarkDbContext : DbContext
{

    /// <summary>
    /// Initialize an instance of <see cref="YellowMarkDbContext"/>
    /// </summary>
    /// <param name="dbContextOptions"></param>
    public YellowMarkDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    { }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
            )
        );
    }
}
