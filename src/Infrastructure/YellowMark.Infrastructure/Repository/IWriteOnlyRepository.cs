using System.Linq.Expressions;

namespace YellowMark.Infrastructure.Repository;

/// <summary>
/// Basic repository.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IWriteOnlyRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Add new instance of the entity.
    /// </summary>
    /// <param name="model">Entity model <see cref="TEntity"/></param>
    /// <returns></returns>
    Task AddAsync(TEntity model, CancellationToken cancellationToken);

    /// <summary>
    /// Update current instance of the entity.
    /// </summary>
    /// <param name="model">Entity model <see cref="TEntity"/></param>
    /// <returns></returns>
    Task UpdateAsync(TEntity model, CancellationToken cancellationToken);

    /// <summary>
    /// Delete an instance of the entity by id.
    /// </summary>
    /// <param name="id">Entity id</param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
