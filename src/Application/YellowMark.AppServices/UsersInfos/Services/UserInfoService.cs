using AutoMapper;
using YellowMark.AppServices.Specifications;
using YellowMark.AppServices.UsersInfos.Repositories;
using YellowMark.AppServices.Users.Specifications;
using YellowMark.Contracts.Pagination;
using YellowMark.Contracts.UsersInfos;
using YellowMark.Domain.UsersInfos.Entity;
using YellowMark.AppServices.UsersInfos.Specifications;

namespace YellowMark.AppServices.UsersInfos.Services;

/// <inheritdoc cref="IUserService"/>
public class UserInfoService : IUserInfoService
{
    private readonly IUserInfoRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init <see cref="UserInfoService"/> instance.
    /// </summary>
    /// <param name="userRepository">Users repository</param>
    /// <param name="mapper">Users mapper.</param>
    public UserInfoService(IUserInfoRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddUserAsync(CreateUserInfoRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateUserInfoRequest, UserInfo>(request);
        await _userRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc/>
    public Task<ResultWithPagination<UserInfoDto>> GetUsersAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken)
    {
        return _userRepository.GetAllAsync(request, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<UserInfoDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<UserInfoDto>> GetUsersByNameAsync(UserInfoByNameRequest request, CancellationToken cancellationToken)
    {
        Specification<UserInfo> specification = new UserInfoByNameSpecification(request.Name);
        if (request.BeOver18)
        {
            specification = specification.And(new UserMustBeOver18Specification());
        }
        return _userRepository.GetFiltered(specification, cancellationToken);
    }


    /// <inheritdoc/>
    public async Task<UserInfoDto> UpdateUserAsync(Guid id, CreateUserInfoRequest request, CancellationToken cancellationToken)
    {
        // TODO: Need to fix. Get previous record and update it (Created at id wrong).
        var entity = _mapper.Map<CreateUserInfoRequest, UserInfo>(request);
        entity.Id = id;

        await _userRepository.UpdateAsync(entity, cancellationToken);

        return _mapper.Map<UserInfo, UserInfoDto>(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteAsync(id, cancellationToken);
    }
}