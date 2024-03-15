using YellowMark.Contracts.Currnecies;

namespace YellowMark.AppServices.Currencies.Repositories;

/// <summary>
/// Currency repository.
/// </summary>
public interface ICurrencyRepository
{
    /// <summary>
    /// Returns all instances of the CurrencyDto.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CurrencyDto"/></returns>
    Task<IEnumerable<CurrencyDto>> GetAllAsync(CancellationToken cancellationToken);
}
