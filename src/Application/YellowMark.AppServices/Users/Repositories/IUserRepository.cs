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
    /// <returns><see cref="UserDto"/></returns>
    Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Create new User instance from request params and return created user.
    /// </summary>
    /// <param name="request">User request model <see cref="CreateUserRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created user instance <see cref="UserDto"/></returns>
    Task AddAsync(Domain.Users.Entity.User entity, CancellationToken cancellationToken);
}