using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YellowMark.Domain.Base;

namespace YellowMark.Infrastructure.Repository;

/// <inheritdoc cref="IWriteOnlyRepository"/>
public class WriteOnlyRepository<TEntity, TContext> : IWriteOnlyRepository<TEntity, TContext> where TEntity : BaseEntity where TContext : IdentityDbContext<YellowMark.Domain.Accounts.Entity.Account, IdentityRole<Guid>, Guid>
{
    /// <summary>
    /// Database context inherited from <see cref="IdentityDbContext"/>.
    /// </summary>
    protected TContext DbContext { get; }

    /// <summary>
    /// <see cref="DbSet"/>
    /// </summary>
    protected DbSet<TEntity> DbSet { get; }

    /// <summary>
    /// Init <see cref="IWriteOnlyRepository"/> instance.
    /// </summary>
    /// <param name="context">Database context</param>
    public WriteOnlyRepository(TContext context)
    {
        DbContext = context;
        DbSet = DbContext.Set<TEntity>();
    }

    /// <inheritdoc/>
    public async Task AddAsync(TEntity model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);
        await DbSet.AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(TEntity model, CancellationToken cancellationToken)
    {
        DbSet.Update(model);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entityToDelete = DbSet.Find(id);
        ArgumentNullException.ThrowIfNull(entityToDelete);
        DbSet.Remove(entityToDelete);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}
