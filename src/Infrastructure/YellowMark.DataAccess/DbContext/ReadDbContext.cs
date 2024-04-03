using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace YellowMark.DataAccess.DatabaseContext;

/// <summary>
/// Read-only database context.
/// </summary>
public class ReadDbContext : WriteDbContext
{

    /// <summary>
    /// Initialize an instance of <see cref="ReadDbContext"/>
    /// </summary>
    /// <param name="dbContextOptions"></param>
    public ReadDbContext(DbContextOptions<ReadDbContext> dbContextOptions) : base(dbContextOptions)
    { }

    /// <summary>
    /// A protected constructor that uses DbContextOptions without any type.
    /// </summary>
    /// <param name="dbContextOptions">Without any type in <see cref="DbContextOptions"/></param>
    protected ReadDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    { }

    /// <summary>
    /// Read-only context does not provide saving changes functionality.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Exception thrown on write attempt.</exception>
    public override int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }
}
