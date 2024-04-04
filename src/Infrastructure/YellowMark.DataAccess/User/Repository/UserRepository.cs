using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Npgsql.TypeMapping;
using YellowMark.AppServices.Users.Repositories;
using YellowMark.Contracts.Users;
using YellowMark.DataAccess.DatabaseContext;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.User.Repository;

/// <inheritdoc/>
public class UserRepository : IUserRepository
{
    private readonly IWriteOnlyRepository<Domain.Users.Entity.User, WriteDbContext> _writeOnlyrepository;
    private readonly IReadOnlyRepository<Domain.Users.Entity.User, ReadDbContext> _readOnlyrepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init UserRepository (<see cref="IUserRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    public UserRepository (
        IWriteOnlyRepository<Domain.Users.Entity.User, WriteDbContext> writeOnlyRepository,
        IReadOnlyRepository<Domain.Users.Entity.User, ReadDbContext> readOnlyRepository,
        IMapper mapper)
    {
        _writeOnlyrepository = writeOnlyRepository;
        _readOnlyrepository = readOnlyRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _readOnlyrepository
            .GetAll()
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public IQueryable<UserDto> GetFiltered(Expression<Func<UserDto, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _readOnlyrepository
            .GetAll()
            .Where(s => s.Id == id)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(Domain.Users.Entity.User entity, CancellationToken cancellationToken)
    {
        await _writeOnlyrepository.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Domain.Users.Entity.User entity, CancellationToken cancellationToken)
    {
        // TODO: Make it works. Need update entity before update.
        var user = await _readOnlyrepository.GetByIdAsync(entity.Id, cancellationToken);
        await _writeOnlyrepository.UpdateAsync(user, cancellationToken);
    }

    /// <inheritdoc/>
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}