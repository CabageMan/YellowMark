using System.Linq.Expressions;
using YellowMark.Contracts.Users;

namespace YellowMark.AppServices.Users.Repositories;

/// <summary>
/// Users repository.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Returns all instances of the UserDto. 
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>List of <see cref="UserDto"/></returns>
    Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the <see cref="UserDto"/> by predicate.
    /// </summary>
    /// <param name="predicate">Filtering predicate</param>
    /// <returns><see cref="UserDto"/></returns>
    IQueryable<UserDto> GetFiltered(Expression<Func<UserDto, bool>> predicate);

    /// <summary>
    /// Returns an instance of the <see cref="UserDto"/> by id.
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="UserDto"/></returns>
    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Add new User instance and return created user id.
    /// </summary>
    /// <param name="entity">User request model <see cref="Domain.Users.Entity.User"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task AddAsync(Domain.Users.Entity.User entity, CancellationToken cancellationToken);

    /// <summary>
    /// Update current User.
    /// </summary>
    /// <param name="entity">Entity model <see cref="Domain.Users.Entity.User"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task UpdateAsync(Domain.Users.Entity.User entity, CancellationToken cancellationToken);

    /// <summary>
    /// Delete a User by id.
    /// </summary>
    /// <param name="id">User id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}