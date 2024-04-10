using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Currnecies;

namespace YellowMark.AppServices.Currencies.Repositories;

/// <summary>
/// Currency repository.
/// </summary>
public interface ICurrencyRepository
{
    /// <summary>
    /// Add new Currency instance.
    /// </summary>
    /// <param name="entity">Currency model <see cref="Domain.Currencies.Entity.Currency"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task AddAsync(Domain.Currencies.Entity.Currency entity, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the CurrencyDto.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CurrencyDto"/></returns>
    Task<IEnumerable<CurrencyDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the <see cref="CurrencyDto"/> matched to specification.
    /// </summary>
    /// <param name="specification">Filtering specification <see cref="Specification"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Collection of the <see cref="CurrencyDto"/></returns>
    Task<IEnumerable<CurrencyDto>> GetFiltered(Specification<Domain.Currencies.Entity.Currency> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="CurrencyDto"/> by id.
    /// </summary>
    /// <param name="id">Currency id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CurrencyDto"/></returns>
    Task<CurrencyDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Update current Currency.
    /// </summary>
    /// <param name="entity">Entity model <see cref="Domain.Currencies.Entity.Currency"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task UpdateAsync(Domain.Currencies.Entity.Currency entity, CancellationToken cancellationToken);

    /// <summary>
    /// Delete a Currency by id.
    /// </summary>
    /// <param name="id">Currency id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
