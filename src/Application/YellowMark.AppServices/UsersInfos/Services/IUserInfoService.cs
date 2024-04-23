using YellowMark.Contracts.Pagination;
using YellowMark.Contracts.UsersInfos;

namespace YellowMark.AppServices.UsersInfos.Services;

/// <summary>
/// Users service.
/// </summary>
public interface IUserInfoService
{
    /// <summary>
    /// Create new User instance from the request params. 
    /// </summary>
    /// <param name="request">User request model <see cref="CreateUserInfoRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created user id <see cref="Guid"/></returns>
    Task<Guid> AddUserAsync(CreateUserInfoRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all users.
    /// </summary>
    /// <param name="request">Pagination params <see cref="GetAllRequestWithPagination"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Result with pagination params <see cref="ResultWithPagination"/>.</returns>
    Task<ResultWithPagination<UserInfoDto>> GetUsersAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="UserInfoDto"/> by id.
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns><see cref="UserInfoDto"/></returns>
    Task<UserInfoDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all users matched the name.
    /// </summary>
    /// <param name="request">User request model <see cref="UserByNameRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Users collection of <see cref="UserInfoDto"/>.</returns>
    Task<IEnumerable<UserInfoDto>> GetUsersByNameAsync(UserInfoByNameRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns user matched the Account id.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>User <see cref="UserInfoDto"/>.</returns>
    Task<UserInfoDto> GetUserByAccountIdAsync(Guid accountId, CancellationToken cancellationToken);

    /// <summary>
    /// Update current User.
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="request">Creation model <see cref="CreateUserInfoRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated user <see cref="UserInfoDto"/></returns>
    Task<UserInfoDto> UpdateUserAsync(Guid id, CreateUserInfoRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Delete the User by id.
    /// </summary>
    /// <param name="id">User id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken);
}