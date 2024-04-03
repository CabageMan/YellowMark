using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace YellowMark.DataAccess.DatabaseContext;

/// <summary>
/// Write-only database context.
/// </summary>
public class WriteDbContext : DbContext
{

    /// <summary>
    /// Initialize an instance of <see cref="WriteDbContext"/>
    /// </summary>
    /// <param name="dbContextOptions"></param>
    public WriteDbContext(DbContextOptions<WriteDbContext> dbContextOptions) : base(dbContextOptions)
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
