using YellowMark.Contracts;
using YellowMark.Contracts.Pagination;
using YellowMark.Contracts.Users;

namespace YellowMark.AppServices.Users.Services;

/// <summary>
/// Users service.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Create new User instance from the request params. 
    /// </summary>
    /// <param name="request">User request model <see cref="CreateUserRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created user id <see cref="Guid"/></returns>
    Task<Guid> AddUserAsync(CreateUserRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all users.
    /// </summary>
    /// <param name="request">Pagination params <see cref="GetAllRequestWithPagination"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Result with pagination params <see cref="ResultWithPagination"/>.</returns>
    Task<ResultWithPagination<UserDto>> GetUsersAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="UserDto"/> by id.
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns><see cref="UserDto"/></returns>
    Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all users matched the name.
    /// </summary>
    /// <param name="request">User request model <see cref="UserByNameRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Users collection of <see cref="UserDto"/>.</returns>
    Task<IEnumerable<UserDto>> GetUsersByNameAsync(UserByNameRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Update current User.
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="request">Creation model <see cref="CreateUserRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated user <see cref="UserDto"/></returns>
    Task<UserDto> UpdateUserAsync(Guid id, CreateUserRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Delete the User by id.
    /// </summary>
    /// <param name="id">User id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken);
}