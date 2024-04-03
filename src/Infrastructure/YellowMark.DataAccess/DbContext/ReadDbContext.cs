using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace YellowMark.DataAccess.DatabaseContext;

/// <summary>
/// Read-only database context.
/// </summary>
public class ReadDbContext : DbContext
{

    /// <summary>
    /// Initialize an instance of <see cref="ReadDbContext"/>
    /// </summary>
    /// <param name="dbContextOptions"></param>
    public ReadDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
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

    /// <summary>
    /// Read-only context does not provide saving changes functionality.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public override int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }
}
