using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Pagination;
using YellowMark.Contracts.UsersInfos;
using YellowMark.Domain.UsersInfos.Entity;

namespace YellowMark.AppServices.UsersInfos.Repositories;

/// <summary>
/// Users repository.
/// </summary>
public interface IUserInfoRepository
{
    /// <summary>
    /// Add new User instance.
    /// </summary>
    /// <param name="entity">User model <see cref="UserInfo"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task AddAsync(UserInfo entity, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the UserDto with pagination info. 
    /// </summary>
    /// <param name="request"><see cref="GetAllRequestWithPagination"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Result with pagination params <see cref="ResultWithPagination"/> with <see cref="UserInfoDto"/></returns>
    Task<ResultWithPagination<UserInfoDto>> GetAllAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the <see cref="UserInfoDto"/> matched to specification.
    /// </summary>
    /// <param name="specification">Filtering specification <see cref="Specification"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Collection of the <see cref="UserInfoDto"/></returns>
    Task<IEnumerable<UserInfoDto>> GetFiltered(Specification<UserInfo> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="UserInfo"/> by id.
    /// </summary>
    /// <param name="id">User id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="UserInfo"/></returns>
    Task<UserInfo> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Update current User.
    /// </summary>
    /// <param name="entity">Entity model <see cref="UserInfo"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task UpdateAsync(UserInfo entity, CancellationToken cancellationToken);

    /// <summary>
    /// Delete a User by id.
    /// </summary>
    /// <param name="id">User id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}