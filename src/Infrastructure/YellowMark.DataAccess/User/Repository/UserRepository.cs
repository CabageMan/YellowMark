using YellowMark.AppServices.Users.Repositories;
using YellowMark.Contracts.Users;

namespace YellowMark.DataAccess.User.Repository;

/// <inheritdoc />
public class UserRepository : IUserRepository
{
    /// <inheritdoc />
    public Task<IEnumerable<UserDto>> GetAll(CancellationToken cancellationToken)
    {
        var users = UserList();

        return Task.Run(() => users.Select(user => new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            FullName = $"{user.LastName} {user.MiddleName} {user.FirstName}"
        }), cancellationToken);
    }

    /// <summary>
    /// Mock Users Data
    /// </summary>
    /// <returns></returns>
    public static List<Domain.Users.Entity.User> UserList()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Blob",
                MiddleName = "Jr.",
                LastName = "Awesome"
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jack",
                MiddleName = "Captain",
                LastName = "Sparrow"
            }
        ];
    }
}