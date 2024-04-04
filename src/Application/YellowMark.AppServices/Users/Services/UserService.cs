using AutoMapper;
using YellowMark.AppServices.Users.Repositories;
using YellowMark.Contracts.Users;
using YellowMark.Domain.Users.Entity;

namespace YellowMark.AppServices.Users.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init <see cref="IUserRepository"/> instance.
    /// </summary>
    /// <param name="userRepository">Users repository</param>
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken cancellationToken)
    {
        return _userRepository.GetAllAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Guid> AddUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateUserRequest, User>(model);
        await _userRepository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }

    /// <inheritdoc />
    public Task UpdateUserAsync(User entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}