using YellowMark.Contracts.Ads;
using YellowMark.Contracts.Comments;
using YellowMark.Contracts.Pagination;

namespace YellowMark.AppServices.Ads.Services;

/// <summary>
/// Ads service.
/// </summary>
public interface IAdService
{
    /// <summary>
    /// Create new Ad instance from the request params. 
    /// </summary>
    /// <param name="request">Ad request model <see cref="CreateAdRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created Ad id <see cref="Guid"/></returns>
    Task<Guid> AddAdAsync(CreateAdRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all ads with pagination.
    /// </summary>
    /// <param name="request">Pagination params <see cref="GetAllRequestWithPagination"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Result with pagination params <see cref="ResultWithPagination"/>.</returns>
    Task<ResultWithPagination<AdDto>> GetAdsAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="AdDto"/> by id.
    /// </summary>
    /// <param name="id">Ad id</param>
    /// <returns><see cref="AdDto"/></returns>
    Task<AdDto> GetAdByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all ads matched the title.
    /// </summary>
    /// <param name="request">Ad request model <see cref="AdByTitleRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Ads collection of <see cref="AdDto"/>.</returns>
    Task<IEnumerable<AdDto>> GetAdsByTitleAsync(AdByTitleRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all ads related to category.
    /// </summary>
    /// <param name="id">Category id</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Ads collection of <see cref="AdDto"/>.</returns>
    Task<IEnumerable<AdDto>> GetAdsByCategoryAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Check if ad exists by Id.
    /// </summary>
    /// <param name="id">Ad Id.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Bool if ad exist.</returns>
    Task<bool> AdExistsWithId(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Update current Ad.
    /// </summary>
    /// <param name="id">Ad id</param>
    /// <param name="request">Creation model <see cref="CreateAdRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated ad <see cref="AdDto"/></returns>
    Task<AdDto> UpdateAdAsync(Guid id, CreateAdRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Delete the Ad by id.
    /// </summary>
    /// <param name="id">Ad id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteAdByIdAsync(Guid id, CancellationToken cancellationToken);
}
