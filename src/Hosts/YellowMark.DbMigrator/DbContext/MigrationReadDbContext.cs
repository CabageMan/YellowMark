using System.Reflection;
using Microsoft.EntityFrameworkCore;
using YellowMark.DataAccess.DatabaseContext;

namespace YellowMark.DbMigrator.DatabaseContext;

/// <summary>
/// Database context for migration inherited from <see cref="ReadDbContext"/>.
/// </summary>
public class MigrationReadDbContext : ReadDbContext
{
    /// <summary>
    /// Initialize an instance of <see cref="MigrationReadDbContext"/>
    /// </summary>
    /// <param name="dbContextOptions">With type of <see cref="MigrationReadDbContext"/> in <see cref="DbContextOptions"/></param>
    public MigrationReadDbContext(DbContextOptions<MigrationReadDbContext> dbContextOptions) : base(dbContextOptions)
    { }
}
