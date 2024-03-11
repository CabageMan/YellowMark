using YellowMark.Contracts.Users;

namespace YellowMark.AppServices.Users.Repositories;

/// <summary>
/// Users repository.
/// </summary>
public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAll(CancellationToken cancellationToken);
}