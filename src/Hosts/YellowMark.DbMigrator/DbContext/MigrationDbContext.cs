using Microsoft.EntityFrameworkCore;
using YellowMark.DataAccess.DatabaseContext;

namespace YellowMark.DbMigrator.DatabaseContext;

/// <summary>
/// Database context for migration inherited from <see cref="WriteDbContext"/>.
/// </summary>
public class MigrationDbContext : WriteDbContext
{
    /// <summary>
    /// Initialize an instance of <see cref="MigrationDbContext"/>
    /// </summary>
    /// <param name="dbContextOptions">With type of <see cref="MigrationDbContext"/> in <see cref="DbContextOptions"/></param>
    public MigrationDbContext(DbContextOptions<MigrationDbContext> dbContextOptions) : base(dbContextOptions)
    { }
}
