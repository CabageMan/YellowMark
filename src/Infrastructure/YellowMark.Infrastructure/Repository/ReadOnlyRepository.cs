using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace YellowMark.Infrastructure.Repository;

public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// <see cref="DbContext"/>
    /// </summary>
    protected DbContext DbContext { get; }

    /// <summary>
    /// <see cref="DbSet"/>
    /// </summary>
    protected DbSet<TEntity> DbSet { get; }

    /// <summary>
    /// Init <see cref="IRepository"/> instance.
    /// </summary>
    /// <param name="context">Data Base context</param>
    public ReadOnlyRepository(DbContext context)
    {
        DbContext = context;
        DbSet = DbContext.Set<TEntity>();
    }

    /// <inheritdoc />
    public IQueryable<TEntity> GetAll()
    {
        return DbSet.AsNoTracking();
    }

    /// <inheritdoc />
    public IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return DbSet.Where(predicate).AsNoTracking();
    }

    /// <inheritdoc />
    public ValueTask<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return DbSet.FindAsync(id, cancellationToken);
    }
}
