using YellowMark.Domain.Users;

namespace YellowMark.Application.Contexts.User;

/// <inheritdoc />
public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    /// <inheritdoc />
    public Task<Domain.Users.User> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _userRepository.GetByIdAsync(userId, cancellationToken);
    }

    /// <inheritdoc />
    public Task<Guid> CreateAsync(CreateUserDto userModel, CancellationToken cancellationToken)
    {
        // Temp implementation
        var user = new Domain.Users.User() 
        {
            Id = new Guid()
        };
        return _userRepository.CreateAsync(user, cancellationToken);
    }
}