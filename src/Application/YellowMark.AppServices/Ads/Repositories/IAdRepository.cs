using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Ads;
using YellowMark.Contracts.Pagination;
using YellowMark.Domain.Ads.Entity;

namespace YellowMark.AppServices.Ads.Repositories;

/// <summary>
/// Ads repository.
/// </summary>
public interface IAdRepository
{
    /// <summary>
    /// Add new Ad instance.
    /// </summary>
    /// <param name="entity">Ad model <see cref="Ad"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task AddAsync(Ad entity, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the AdDto with pagination info. 
    /// </summary>
    /// <param name="request"><see cref="GetAllRequestWithPagination"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Result with pagination params <see cref="ResultWithPagination"/> with <see cref="AdDto"/></returns>
    Task<ResultWithPagination<AdDto>> GetAllAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the <see cref="AdDto"/> matched to specification.
    /// </summary>
    /// <param name="specification">Filtering specification <see cref="Specification"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Collection of the <see cref="AdDto"/></returns>
    Task<IEnumerable<AdDto>> GetFiltered(Specification<Ad> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="Ad"/> by id.
    /// </summary>
    /// <param name="id">Ad id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Ad"/></returns>
    Task<Ad> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Check if ad exists.
    /// </summary>
    /// <param name="id">Ad Id.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Bool if ad existst.</returns>
    Task<bool> ExistsWithId(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Update current Ad.
    /// </summary>
    /// <param name="entity">Entity model <see cref="Ad"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task UpdateAsync(Ad entity, CancellationToken cancellationToken);

    /// <summary>
    /// Delete a Ad by id.
    /// </summary>
    /// <param name="id">Ad id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
