using System.Linq.Expressions;

namespace YellowMark.Infrastructure.Repository;

/// <summary>
/// Basic repository.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> where TEntity : class
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
    /// <returns><see cref="TEntity"/></returns>
    Task<TEntity> GetByIdAsync(Guid id);

    /// <summary>
    /// Add new instance of the entity.
    /// </summary>
    /// <param name="model">Entity model <see cref="TEntity"/></param>
    /// <returns></returns>
    Task AddAsync(TEntity model);

    /// <summary>
    /// Update current instance of the entity.
    /// </summary>
    /// <param name="model">Entity model <see cref="TEntity"/></param>
    /// <returns></returns>
    Task UpdateAsync(TEntity model);

    /// <summary>
    /// Delete an instance of the entity by id.
    /// </summary>
    /// <param name="id">Entity id</param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);
}
