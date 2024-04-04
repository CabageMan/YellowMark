using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YellowMark.Domain.Base;

namespace YellowMark.Infrastructure.Repository;

/// <inheritdoc/>
public class ReadOnlyRepository<TEntity, TContext> : IReadOnlyRepository<TEntity, TContext> where TEntity : BaseEntity where TContext : DbContext 
{
    /// <summary>
    /// Ratabase context inherited from <see cref="DbContext"/>. 
    /// </summary>
    protected TContext DbContext { get; }

    /// <summary>
    /// <see cref="DbSet"/>
    /// </summary>
    protected DbSet<TEntity> DbSet { get; }

    /// <summary>
    /// Init <see cref="IReadOnlyRepository"/> instance.
    /// </summary>
    /// <param name="context">Database context</param>
    public ReadOnlyRepository(TContext context)
    {
        DbContext = context;
        DbSet = DbContext.Set<TEntity>();
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> GetAll()
    {
        return DbSet.AsNoTracking();
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return DbSet.Where(predicate).AsNoTracking();
    }

    /// <inheritdoc/>
    public ValueTask<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return DbSet.FindAsync(id, cancellationToken);
    }
}
