using Microsoft.EntityFrameworkCore;
using YellowMark.Domain.Base;

namespace YellowMark.Infrastructure.Repository;

/// <inheritdoc cref="IReadOnlyRepository"/>
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
    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await DbSet.FindAsync(id, cancellationToken);
        if (entity == null)
        {
            throw new InvalidOperationException($"Instance of {typeof(TEntity).Name} not found");
        }
        return entity;
    }
}
