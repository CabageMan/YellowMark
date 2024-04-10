using AutoMapper;
using YellowMark.AppServices.Specifications;
using YellowMark.AppServices.Users.Repositories;
using YellowMark.AppServices.Users.Specifications;
using YellowMark.Contracts.Pagination;
using YellowMark.Contracts.Users;
using YellowMark.Domain.Users.Entity;

namespace YellowMark.AppServices.Users.Services;

/// <inheritdoc cref="IUserService"/>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init <see cref="IUserRepository"/> instance.
    /// </summary>
    /// <param name="userRepository">Users repository</param>
    /// <param name="mapper">Users mapper.</param>
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateUserRequest, User>(request);
        await _userRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc/>
    public Task<ResultWithPagination<UserDto>> GetUsersAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken)
    {
        return _userRepository.GetAllAsync(request, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<UserDto>> GetUsersByNameAsync(UserByNameRequest request, CancellationToken cancellationToken)
    {
        Specification<User> specification = new ByNameSpecification(request.Name);
        if (request.BeOver18)
        {
            specification = specification.And(new BeOver18Specification());
        }
        return _userRepository.GetFiltered(specification, cancellationToken);
    }


    /// <inheritdoc />
    public async Task<UserDto> UpdateUserAsync(Guid id, CreateUserRequest request, CancellationToken cancellationToken)
    {
        // TODO: Need to fix. Get previous record and update it (Created at id wrong).
        var entity = _mapper.Map<CreateUserRequest, User>(request);
        entity.Id = id;

        await _userRepository.UpdateAsync(entity, cancellationToken);

        return _mapper.Map<User, UserDto>(entity);
    }

    /// <inheritdoc />
    public async Task DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteAsync(id, cancellationToken);
    }
}