using Microsoft.EntityFrameworkCore;
using YellowMark.DataAccess.DatabaseContext;

namespace YellowMark.DbMigrator.DatabaseContext;

/// <summary>
/// Database context for migration inherited from <see cref="WriteDbContext"/>.
/// </summary>
public class MigrationWriteDbContext : WriteDbContext
{
    /// <summary>
    /// Initialize an instance of <see cref="MigrationWriteDbContext"/>
    /// </summary>
    /// <param name="dbContextOptions">With type of <see cref="MigrationWriteDbContext"/> in <see cref="DbContextOptions"/></param>
    public MigrationWriteDbContext(DbContextOptions<MigrationWriteDbContext> dbContextOptions) : base(dbContextOptions)
    { }
}
