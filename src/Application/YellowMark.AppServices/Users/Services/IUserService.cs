using YellowMark.Contracts;
using YellowMark.Contracts.Users;

namespace YellowMark.AppServices.Users.Services;

/// <summary>
/// Users service.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Returns all users.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Users collection of <see cref="UserDto"/>.</returns>
    Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="UserDto"/> by id.
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns><see cref="UserDto"/></returns>
    Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Create new User instance from request params. 
    /// </summary>
    /// <param name="model">User request model <see cref="CreateUserRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created user instance <see cref="UserDto"/></returns>
    Task<Guid> AddUserAsync(CreateUserRequest model, CancellationToken cancellationToken);

    /// <summary>
    /// Update current User.
    /// </summary>
    /// <param name="entity">Entity model <see cref="Domain.Users.Entity.User"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task UpdateUserAsync(Domain.Users.Entity.User entity, CancellationToken cancellationToken);

    /// <summary>
    /// Delete the User by id.
    /// </summary>
    /// <param name="id">User <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken);
}