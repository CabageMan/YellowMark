using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Currencies.Repositories;
using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Currnecies;
using YellowMark.DataAccess.DatabaseContext;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.Currency.Repository;


/// <inheritdoc cref="ICurrencyRepository"/>
public class CurrencyRepository : ICurrencyRepository
{
    private readonly IWriteOnlyRepository<Domain.Currencies.Entity.Currency, WriteDbContext> _writeOnlyRepository;
    private readonly IReadOnlyRepository<Domain.Currencies.Entity.Currency, ReadDbContext> _readOnlyRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init CurrencyRepository (<see cref="ICurrencyRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    /// <param name="mapper"><see cref="IMapper"/></param>
    public CurrencyRepository (
        IWriteOnlyRepository<Domain.Currencies.Entity.Currency, WriteDbContext> writeOnlyRepository,
        IReadOnlyRepository<Domain.Currencies.Entity.Currency, ReadDbContext> readOnlyRepository,
        IMapper mapper)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task AddAsync(Domain.Currencies.Entity.Currency entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CurrencyDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _readOnlyRepository
            .GetAll()
            .ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Domain.Currencies.Entity.Currency> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _readOnlyRepository
            .GetAll()
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CurrencyDto>> GetFiltered(Specification<Domain.Currencies.Entity.Currency> specification, CancellationToken cancellationToken)
    {
        return await _readOnlyRepository
            .GetAll()
            .Where(specification.ToExpression())
            .ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsWithId(Guid id, CancellationToken cancellationToken)
    {
        return await _readOnlyRepository.ExistsWithId(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Domain.Currencies.Entity.Currency entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.UpdateAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.DeleteAsync(id, cancellationToken);
    }
}
