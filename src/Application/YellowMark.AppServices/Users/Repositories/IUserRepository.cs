using System.Linq.Expressions;
using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Pagination;
using YellowMark.Contracts.Users;
using YellowMark.Domain.Users.Entity;

namespace YellowMark.AppServices.Users.Repositories;

/// <summary>
/// Users repository.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Add new User instance and return created user id.
    /// </summary>
    /// <param name="entity">User request model <see cref="Domain.Users.Entity.User"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task AddAsync(Domain.Users.Entity.User entity, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the UserDto with pagination info. 
    /// </summary>
    /// <param name="request"><see cref="GetAllRequestWithPagination"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Result with pagination params <see cref="ResultWithPagination"/> with <see cref="UserDto"/></returns>
    Task<ResultWithPagination<UserDto>> GetAllAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the <see cref="UserDto"/> matched to specification.
    /// </summary>
    /// <param name="specification">Filtering specification <see cref="Specification"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Collection of the <see cref="UserDto"/></returns>
    Task<IEnumerable<UserDto>> GetFiltered(Specification<Domain.Users.Entity.User> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="UserDto"/> by id.
    /// </summary>
    /// <param name="id">User id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="UserDto"/></returns>
    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

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