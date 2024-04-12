using AutoMapper;
using YellowMark.AppServices.Currencies.Repositories;
using YellowMark.Contracts.Currnecies;
using YellowMark.Domain.Currencies.Entity;

namespace YellowMark.AppServices.Currencies.Services;

/// <inheritdoc cref="ICurrencyService"/>
public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init <see cref="CurrencyService"/> instance.
    /// </summary>
    /// <param name="currencyRepository">Currency Repository (<see cref="ICurrencyRepository"/>)</param>
    /// <param name="mapper">Currency mapper</param>
    public CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper)
    {
        _currencyRepository = currencyRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddCurrencyAsync(CreateCurrencyRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateCurrencyRequest, Currency>(request);
        await _currencyRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc/>
    public Task<IEnumerable<CurrencyDto>> GetCurrenciesAsync(CancellationToken cancellationToken)
    {
        return _currencyRepository.GetAllAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<CurrencyDto> GetCurrencyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _currencyRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<CurrencyDto> UpdateCurrencyAsync(Guid id, CreateCurrencyRequest request, CancellationToken cancellationToken)
    {
        // TODO: Need to fix. Get previous record and update it (Created at id wrong).
        var entity = _mapper.Map<CreateCurrencyRequest, Currency>(request);
        entity.Id = id;

        await  _currencyRepository.UpdateAsync(entity, cancellationToken);

        return _mapper.Map<Currency, CurrencyDto>(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteCurrencyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _currencyRepository.DeleteAsync(id, cancellationToken);
    }
}
