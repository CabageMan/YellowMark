using YellowMark.AppServices.Users.Repositories;
using YellowMark.Contracts.Users;

namespace YellowMark.DataAccess.User.Repository;

/// <inheritdoc />
public class UserRepository : IUserRepository
{
    /// <inheritdoc />
    public Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = UsersMockList();

        return Task.Run(() => users.Select(user => new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            FullName = $"{user.LastName} {user.MiddleName} {user.FirstName}",
            Email = user.Email,
            Phone = user.Phone
        }), cancellationToken);
    }

    /// <summary>
    /// Mock User's Data
    /// </summary>
    /// <returns></returns>
    private static List<Domain.Users.Entity.User> UsersMockList()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Blob",
                MiddleName = "Jr.",
                LastName = "Awesome",
                Email = "blob.awesome@email.com",
                Phone = "+71112345678"
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jack",
                MiddleName = "Captain",
                LastName = "Sparrow",
                Email = "captain.jack@blackpearl.com",
                Phone = "+76669876543"
            }
        ];
    }
}