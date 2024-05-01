using AutoMapper;
using YellowMark.AppServices.Specifications;
using YellowMark.AppServices.UsersInfos.Repositories;
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
        var entity = await _userRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<UserInfo, UserInfoDto>(entity);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<UserInfoDto>> GetUsersByNameAsync(UserInfoByNameRequest request, CancellationToken cancellationToken)
    {
        Specification<UserInfo> specification = new UserInfoByNameSpecification(request.Name);

        return _userRepository.GetFiltered(specification, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<UserInfoDto> GetUserByAccountIdAsync(Guid accountId, CancellationToken cancellationToken)
    {
        Specification<UserInfo> specification = new UserInfoByAccounIdSpecification(accountId);
        var users = await _userRepository.GetFiltered(specification, cancellationToken);
        var user = users.First();
        if (user == null)
        {
            throw new InvalidDataException("Could not find user related to account.");
        }
        return user;
    }

    /// <inheritdoc/>
    public async Task<UserInfoDto> UpdateUserAsync(UpdateUserInfoRequest request, CancellationToken cancellationToken)
    {
        var updatedEntity = _mapper.Map<UpdateUserInfoRequest, UserInfo>(request);
        updatedEntity.Id = request.Id;
        updatedEntity.AccountId = request.AccountId;
        updatedEntity.CreatedAt = request.CreatedAt;

        await _userRepository.UpdateAsync(updatedEntity, cancellationToken);

        return _mapper.Map<UserInfo, UserInfoDto>(updatedEntity);
    }

    /// <inheritdoc/>
    public async Task DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteAsync(id, cancellationToken);
    }
}