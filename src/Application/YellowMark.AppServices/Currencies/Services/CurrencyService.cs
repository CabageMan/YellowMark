using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using YellowMark.AppServices.Currencies.Exceptions;
using YellowMark.AppServices.Currencies.Repositories;
using YellowMark.AppServices.Currencies.Specifications;
using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Currnecies;
using YellowMark.Domain.Currencies.Entity;

namespace YellowMark.AppServices.Currencies.Services;

/// <inheritdoc cref="ICurrencyService"/>
public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _distributedCache;

    /// <summary>
    /// Init <see cref="CurrencyService"/> instance.
    /// </summary>
    /// <param name="currencyRepository">Currency Repository (<see cref="ICurrencyRepository"/>)</param>
    /// <param name="mapper">Currency mapper</param>
    /// <param name="memoryCache">Memory cache <see cref="IMemoryCache"/></param>
    /// <param name="distributedCache">Distributed cache <see cref="IDistributedCache"/></param>
    public CurrencyService(
        ICurrencyRepository currencyRepository, 
        IMapper mapper,
        IMemoryCache memoryCache,
        IDistributedCache distributedCache)
    {
        _currencyRepository = currencyRepository;
        _mapper = mapper;
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddCurrencyAsync(CreateCurrencyRequest request, CancellationToken cancellationToken)
    {
        Specification<Currency> specification = new CurrencyByCodesSpecification(request.AlphabeticCode);
        var existedCurrency = await _currencyRepository.GetFiltered(specification, cancellationToken);       
        if (!existedCurrency.IsNullOrEmpty())
        {
            throw new CurrencyOperationException($"Currency with code {request.AlphabeticCode} is allready existed.");
        }

        var entity = _mapper.Map<CreateCurrencyRequest, Currency>(request);
        await _currencyRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CurrencyDto>> GetCurrenciesAsync(CancellationToken cancellationToken)
    {
        var cacheKey = "all_currencies";

        var currenciesSerialized = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrWhiteSpace(currenciesSerialized))
        {
            var cachedCurrencies = JsonSerializer.Deserialize<IReadOnlyCollection<CurrencyDto>>(currenciesSerialized);
            if (cachedCurrencies != null)
            {
                return cachedCurrencies;
            }
        }

        var currencies = await _currencyRepository.GetAllAsync(cancellationToken);
        currenciesSerialized = JsonSerializer.Serialize(currencies);
        await _distributedCache.SetStringAsync(
            cacheKey,
            currenciesSerialized,
            new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(15) },
            cancellationToken)
        ;

        return currencies;
    }

    /// <inheritdoc/>
    public async Task<CurrencyDto> GetCurrencyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cacheKey = $"currency_info_{id}";

        if (_memoryCache.TryGetValue<Currency>(cacheKey, out var result) && result != null)
        {
            return _mapper.Map<CurrencyDto>(result);
        }

        var currency = await _currencyRepository.GetByIdAsync(id, cancellationToken);

        _memoryCache.Set(
            cacheKey,
            currency,
            new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(15) }
        );

        return _mapper.Map<CurrencyDto>(currency);
    }

    /// <inheritdoc/>
    public async Task<CurrencyDto> UpdateCurrencyAsync(Guid id, CreateCurrencyRequest request, CancellationToken cancellationToken)
    {
        var currentEntity = await _currencyRepository.GetByIdAsync(id, cancellationToken);

        var updatedEntity = _mapper.Map<CreateCurrencyRequest, Currency>(request);
        updatedEntity.Id = id;
        updatedEntity.CreatedAt = currentEntity.CreatedAt;

        await  _currencyRepository.UpdateAsync(updatedEntity, cancellationToken);

        return _mapper.Map<Currency, CurrencyDto>(updatedEntity);
    }

    /// <inheritdoc/>
    public async Task DeleteCurrencyByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _currencyRepository.DeleteAsync(id, cancellationToken);
    }
}
