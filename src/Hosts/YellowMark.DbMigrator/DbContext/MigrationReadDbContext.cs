﻿using Microsoft.EntityFrameworkCore;
using YellowMark.DataAccess.DatabaseContext;

namespace YellowMark.DbMigrator.DatabaseContext;

/// <summary>
/// Database context for migration inherited from <see cref="YellowMarkDbContext"/>.
/// </summary>
public class MigrationReadDbContext : ReadDbContext
{
    public MigrationReadDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }
}
