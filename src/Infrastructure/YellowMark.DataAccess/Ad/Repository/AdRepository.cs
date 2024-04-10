using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Ads.Repositories;
using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Ads;
using YellowMark.Contracts.Pagination;
using YellowMark.DataAccess.DatabaseContext;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.Ad.Repository;

/// <inheritdoc cref="IAdRepository"/>
public class AdRepository : IAdRepository
{
    private readonly IWriteOnlyRepository<Domain.Ads.Entity.Ad, WriteDbContext> _writeOnlyRepository;
    private readonly IReadOnlyRepository<Domain.Ads.Entity.Ad, ReadDbContext> _readOnlyRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init AdRepository (<see cref="IAdRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    /// <param name="mapper"><see cref="IMapper"/></param>
    public AdRepository(
        IWriteOnlyRepository<Domain.Ads.Entity.Ad, WriteDbContext> writeOnlyRepository,
        IReadOnlyRepository<Domain.Ads.Entity.Ad, ReadDbContext> readOnlyRepository,
        IMapper mapper)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task AddAsync(Domain.Ads.Entity.Ad entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ResultWithPagination<AdDto>> GetAllAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken)
    {
        var result = new ResultWithPagination<AdDto>();

        var query = _readOnlyRepository.GetAll();

        var elementsCount = await query.CountAsync(cancellationToken);
        result.AvailablePages = (int)Math.Ceiling((double)(elementsCount / request.BatchSize));

        var pagesToSkip = request.BatchSize * Math.Max((request.PageNumber - 1), 0);
        var paginationQuery = await query
            .OrderBy(ad => ad.Id)
            .Skip(pagesToSkip)
            .Take(request.BatchSize)
            .ProjectTo<AdDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        result.Result = paginationQuery;

        return result;
    }

    /// <inheritdoc/>
    public async Task<AdDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _readOnlyRepository
            .GetAll()
            .Where(s => s.Id == id)
            .ProjectTo<AdDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<AdDto>> GetFiltered(Specification<Domain.Ads.Entity.Ad> specification, CancellationToken cancellationToken)
    {
        // TODO: Add pagination
        return await _readOnlyRepository
            .GetAll()
            .Where(specification.ToExpression())
            .ProjectTo<AdDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Domain.Ads.Entity.Ad entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.UpdateAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.DeleteAsync(id, cancellationToken);
    }
}
