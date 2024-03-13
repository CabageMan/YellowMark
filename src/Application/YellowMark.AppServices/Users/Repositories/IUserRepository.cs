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
}