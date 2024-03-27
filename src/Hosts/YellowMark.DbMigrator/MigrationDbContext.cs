using Microsoft.EntityFrameworkCore;
using YellowMark.DataAccess.YellowMarkDbContext;

namespace YellowMark.DbMigrator;

/// <summary>
/// Database context for migration inherited from <see cref="YellowMarkDbContext"/>.
/// </summary>
public class MigrationDbContext : YellowMarkDbContext
{

    /// <inheritdoc />
    public MigrationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    { }
}
