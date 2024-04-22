using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Specifications;
using YellowMark.AppServices.UsersInfos.Repositories;
using YellowMark.Contracts.Pagination;
using YellowMark.Contracts.UsersInfos;
using YellowMark.DataAccess.DatabaseContext;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.UserInfo.Repository;

/// <inheritdoc cref="IUserRepository"/>
public class UserInfoRepository : IUserInfoRepository
{
    private readonly IWriteOnlyRepository<Domain.UsersInfos.Entity.UserInfo, WriteDbContext> _writeOnlyRepository;
    private readonly IReadOnlyRepository<Domain.UsersInfos.Entity.UserInfo, ReadDbContext> _readOnlyRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init UserRepository (<see cref="IUserRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    /// <param name="mapper"><see cref="IMapper"/></param>
    public UserInfoRepository(
        IWriteOnlyRepository<Domain.UsersInfos.Entity.UserInfo, WriteDbContext> writeOnlyRepository,
        IReadOnlyRepository<Domain.UsersInfos.Entity.UserInfo, ReadDbContext> readOnlyRepository,
        IMapper mapper)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task AddAsync(Domain.UsersInfos.Entity.UserInfo entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ResultWithPagination<UserInfoDto>> GetAllAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken)
    {
        var result = new ResultWithPagination<UserInfoDto>();

        var query = _readOnlyRepository.GetAll();

        var elementsCount = await query.CountAsync(cancellationToken);
        result.AvailablePages = (int)Math.Ceiling((double)(elementsCount / request.BatchSize));

        var pagesToSkip = request.BatchSize * Math.Max((request.PageNumber - 1), 0);
        var paginationQuery = await query
            .OrderBy(user => user.Id)
            .Skip(pagesToSkip)
            .Take(request.BatchSize)
            .ProjectTo<UserInfoDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        result.Result = paginationQuery;

        return result;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<UserInfoDto>> GetFiltered(Specification<Domain.UsersInfos.Entity.UserInfo> specification, CancellationToken cancellationToken)
    {
        // TODO: Add pagination
        return await _readOnlyRepository
            .GetAll()
            .Where(specification.ToExpression())
            .ProjectTo<UserInfoDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<UserInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _readOnlyRepository
            .GetAll()
            .Where(s => s.Id == id)
            .ProjectTo<UserInfoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Domain.UsersInfos.Entity.UserInfo entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.UpdateAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.DeleteAsync(id, cancellationToken);
    }
}