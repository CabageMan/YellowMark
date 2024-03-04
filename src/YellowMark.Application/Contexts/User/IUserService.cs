using YellowMark.Domain.Users;

namespace YellowMark.Application.Contexts.User;
/// <summary>
/// User services.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Domain.Users.User> GetByIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Guid> CreateAsync(CreateUserDto userModel, CancellationToken cancellationToken);
}