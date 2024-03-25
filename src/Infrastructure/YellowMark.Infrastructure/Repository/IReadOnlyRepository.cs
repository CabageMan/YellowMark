using System.Linq.Expressions;
using YellowMark.Domain.Base;

namespace YellowMark.Infrastructure.Repository;

/// <summary>
/// Basic repository.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IReadOnlyRepository<TEntity> where TEntity : BaseEntity
{

    /// <summary>
    /// Returns all instances of the entity <see cref="TEntity"/>.
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Returns all instances of the entity <see cref="TEntity"/> by predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Returns an instance of the entity by id.
    /// </summary>
    /// <param name="id">Entity id</param>
    /// <returns><see cref="TEntity?"/></returns>
    ValueTask<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
