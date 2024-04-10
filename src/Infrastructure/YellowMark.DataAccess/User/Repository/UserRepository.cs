using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Specifications;
using YellowMark.AppServices.Users.Repositories;
using YellowMark.Contracts.Pagination;
using YellowMark.Contracts.Users;
using YellowMark.DataAccess.DatabaseContext;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.User.Repository;

/// <inheritdoc cref="IUserRepository"/>
public class UserRepository : IUserRepository
{
    private readonly IWriteOnlyRepository<Domain.Users.Entity.User, WriteDbContext> _writeOnlyRepository;
    private readonly IReadOnlyRepository<Domain.Users.Entity.User, ReadDbContext> _readOnlyRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init UserRepository (<see cref="IUserRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    /// <param name="mapper"><see cref="IMapper"/></param>
    public UserRepository(
        IWriteOnlyRepository<Domain.Users.Entity.User, WriteDbContext> writeOnlyRepository,
        IReadOnlyRepository<Domain.Users.Entity.User, ReadDbContext> readOnlyRepository,
        IMapper mapper)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task AddAsync(Domain.Users.Entity.User entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ResultWithPagination<UserDto>> GetAllAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken)
    {
        var result = new ResultWithPagination<UserDto>();

        var query = _readOnlyRepository.GetAll();

        var elementsCount = await query.CountAsync(cancellationToken);
        result.AvailablePages = (int)Math.Ceiling((double)(elementsCount / request.BatchSize));

        var pagesToSkip = request.BatchSize * Math.Max((request.PageNumber - 1), 0);
        var paginationQuery = await query
            .OrderBy(user => user.Id)
            .Skip(pagesToSkip)
            .Take(request.BatchSize)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        result.Result = paginationQuery;

        return result;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<UserDto>> GetFiltered(Specification<Domain.Users.Entity.User> specification, CancellationToken cancellationToken)
    {
        // TODO: Add pagination
        return await _readOnlyRepository
            .GetAll()
            .Where(specification.ToExpression())
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _readOnlyRepository
            .GetAll()
            .Where(s => s.Id == id)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Domain.Users.Entity.User entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.UpdateAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.DeleteAsync(id, cancellationToken);
    }
}