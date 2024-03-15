using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Users.Repositories;
using YellowMark.Contracts.Users;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.User.Repository;

/// <inheritdoc />
public class UserRepository : IUserRepository
{
    /*
    private readonly IWriteOnlyRepository<Domain.Users.Entity.User> _writeOnlyrepository;
    private readonly IReadOnlyRepository<Domain.Users.Entity.User> _readOnlyrepository;

    /// <summary>
    /// Init UserRepository (<see cref="IUserRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    public UserRepository (
        IWriteOnlyRepository<Domain.Users.Entity.User> writeOnlyRepository,
        IReadOnlyRepository<Domain.Users.Entity.User> readOnlyRepository)
    {
        _writeOnlyrepository = writeOnlyRepository;
        _readOnlyrepository = readOnlyRepository;
    }
    */

    /// <inheritdoc />
    public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = UsersMockList();

        return await Task.Run(() => users.Select(user => new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            FullName = $"{user.LastName} {user.MiddleName} {user.FirstName}",
            Email = user.Email,
            Phone = user.Phone
        }), cancellationToken);
        /*
        var users = await _readOnlyrepository.GetAll().ToListAsync(cancellationToken);

        return users.Select(user => new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            FullName = $"{user.LastName} {user.MiddleName} {user.FirstName}",
            Email = user.Email,
            Phone = user.Phone
        });
        */
    }

    // Mock User's Data
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