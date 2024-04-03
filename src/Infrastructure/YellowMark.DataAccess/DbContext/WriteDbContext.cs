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
    /// <param name="dbContextOptions">With type of <see cref="WriteDbContext"/> in <see cref="DbContextOptions"/></param>
    public WriteDbContext(DbContextOptions<WriteDbContext> dbContextOptions) 
        : base(dbContextOptions)
    { }


    /// <summary>
    /// A protected constructor that uses DbContextOptions without any type.
    /// </summary>
    /// <param name="dbContextOptions">Without any type in <see cref="DbContextOptions"/></param>
    protected WriteDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    { }

    /// <inheritdoc/>
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
