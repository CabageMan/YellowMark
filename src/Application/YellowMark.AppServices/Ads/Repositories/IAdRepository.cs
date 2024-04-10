using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Ads;
using YellowMark.Contracts.Pagination;

namespace YellowMark.AppServices.Ads.Repositories;

/// <summary>
/// Ads repository.
/// </summary>
public interface IAdRepository
{
    /// <summary>
    /// Add new Ad instance.
    /// </summary>
    /// <param name="entity">Ad model <see cref="Domain.Ads.Entity.Ad"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task AddAsync(Domain.Ads.Entity.Ad entity, CancellationToken cancellationToken);

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
    Task<IEnumerable<AdDto>> GetFiltered(Specification<Domain.Ads.Entity.Ad> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="AdDto"/> by id.
    /// </summary>
    /// <param name="id">Ad id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="AdDto"/></returns>
    Task<AdDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Update current Ad.
    /// </summary>
    /// <param name="entity">Entity model <see cref="Domain.Ads.Entity.Ad"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task UpdateAsync(Domain.Ads.Entity.Ad entity, CancellationToken cancellationToken);

    /// <summary>
    /// Delete a Ad by id.
    /// </summary>
    /// <param name="id">Ad id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
