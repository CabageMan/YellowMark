using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Users.Repositories;
using YellowMark.Contracts.Users;
using YellowMark.DataAccess.DatabaseContext;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.User.Repository;

/// <inheritdoc />
public class UserRepository : IUserRepository
{
    private readonly IWriteOnlyRepository<Domain.Users.Entity.User, WriteDbContext> _writeOnlyrepository;
    private readonly IReadOnlyRepository<Domain.Users.Entity.User, ReadDbContext> _readOnlyrepository;

    /// <summary>
    /// Init UserRepository (<see cref="IUserRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    public UserRepository (
        IWriteOnlyRepository<Domain.Users.Entity.User, WriteDbContext> writeOnlyRepository,
        IReadOnlyRepository<Domain.Users.Entity.User, ReadDbContext> readOnlyRepository)
    {
        _writeOnlyrepository = writeOnlyRepository;
        _readOnlyrepository = readOnlyRepository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        /*
        var users = UsersMockList();

        return await Task.Run(() => users.Select(user => new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            FullName = $"{user.LastName} {user.MiddleName} {user.FirstName}",
            Email = user.Email,
            Phone = user.Phone,
            BirthDate = user.BirthDate
        }), cancellationToken);
        */
        var users = await _readOnlyrepository.GetAll().ToListAsync(cancellationToken);

        return users.Select(user => new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            FullName = $"{user.LastName} {user.MiddleName} {user.FirstName}",
            Email = user.Email,
            Phone = user.Phone,
            BirthDate = user.BirthDate
        });
    }

    /// <inheritdoc />
    public async Task AddAsync(Domain.Users.Entity.User entity, CancellationToken cancellationToken)
    {
        await _writeOnlyrepository.AddAsync(entity, cancellationToken);
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
                Phone = "+71112345678",
                BirthDate = new DateOnly(2013, 9, 23)
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jack",
                MiddleName = "Captain",
                LastName = "Sparrow",
                Email = "captain.jack@blackpearl.com",
                Phone = "+76669876543",
                BirthDate = new DateOnly(1970, 6, 18)
            }
        ];
    }
}