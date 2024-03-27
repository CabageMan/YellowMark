using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using YellowMark.Domain.Base;

namespace YellowMark.Infrastructure.Repository;

/// <summary>
/// Implementation of the basic repository.
/// </summary>
public class WriteOnlyRepository<TEntity> : IWriteOnlyRepository<TEntity> where TEntity : BaseEntity
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
    /// Init <see cref="IWriteOnlyRepository"/> instance.
    /// </summary>
    /// <param name="context">Data Base context</param>
    public WriteOnlyRepository(DbContext context)
    {
        DbContext = context;
        DbSet = DbContext.Set<TEntity>();
    }


    /// <inheritdoc />
    public async Task AddAsync(TEntity model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);
        await DbSet.AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task UpdateAsync(TEntity model, CancellationToken cancellationToken)
    {
        DbSet.Update(model);
        return DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entityToDelete = DbSet.Find(id);
        ArgumentNullException.ThrowIfNull(entityToDelete);
        DbSet.Remove(entityToDelete);
        return DbContext.SaveChangesAsync(cancellationToken);
    }
}
