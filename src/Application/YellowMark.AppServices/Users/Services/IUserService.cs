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
    /// <returns>Users collection <see cref="UserDto"/>.</returns>
    Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Create new User instance from request params. 
    /// </summary>
    /// <param name="request">User request model <see cref="CreateUserRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created user instance <see cref="UserDto"/></returns>
    Task<UserDto> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken);
}