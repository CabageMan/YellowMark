using YellowMark.Contracts.Currnecies;

namespace YellowMark.AppServices.Currencies.Services;

/// <summary>
/// Currencies service.
/// </summary>
public interface ICurrencyService
{
    /// <summary>
    /// Create new Currency instance from the request params. 
    /// </summary>
    /// <param name="request">Currency request model <see cref="CreateCurrencyRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created Currency id <see cref="Guid"/></returns>
    Task<Guid> AddCurrencyAsync(CreateCurrencyRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all currencies.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Currencies collection <see cref="CurrencyDto"/>.</returns>
    Task<IEnumerable<CurrencyDto>> GetCurrenciesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="CurrencyDto"/> by id.
    /// </summary>
    /// <param name="id">Currency id</param>
    /// <returns><see cref="CurrencyDto"/></returns>
    Task<CurrencyDto> GetCurrencyByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Check if currency exists by Id.
    /// </summary>
    /// <param name="id">Currency Id.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Bool ff currency exist</returns>
    Task<bool> CurrencyExistsWithId(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Update current Currency.
    /// </summary>
    /// <param name="id">Currency id</param>
    /// <param name="request">Creation model <see cref="CreateCurrencyRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated currency <see cref="CurrencyDto"/></returns>
    Task<CurrencyDto> UpdateCurrencyAsync(Guid id, CreateCurrencyRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Delete the Currency by id.
    /// </summary>
    /// <param name="id">Currency id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteCurrencyByIdAsync(Guid id, CancellationToken cancellationToken);
}
