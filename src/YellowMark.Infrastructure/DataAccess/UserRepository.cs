using YellowMark.Application.Contexts.User;
using YellowMark.Domain.Users;

namespace YellowMark.Infrastructure.DataAccess;

/// <summary>
/// May be refactor in future. IUserRepository interface is situated in Application.
/// </summary>
public class UserRepository : IUserRepository
{
    public Task<Guid> CreateAsync(User userModel, CancellationToken cancellationToken)
    {
        /// Interract with DB
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        /// Interract with DB
        throw new NotImplementedException();
    }
}