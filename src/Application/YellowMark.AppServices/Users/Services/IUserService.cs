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
}