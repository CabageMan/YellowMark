using YellowMark.AppServices.Users.Repositories;
using YellowMark.Contracts;
using YellowMark.Contracts.Users;

namespace YellowMark.AppServices.Users.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Init <see cref="IUserRepository"/> instance.
    /// </summary>
    /// <param name="userRepository">Users repository</param>
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken cancellationToken)
    {
        return _userRepository.GetAllAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<UserDto> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        return _userRepository.CreateUserAsync(request, cancellationToken);
    }
}