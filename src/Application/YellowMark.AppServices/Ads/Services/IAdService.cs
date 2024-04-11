using YellowMark.Contracts.Ads;

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
    /// Returns all ads.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Ads collection <see cref="AdDto"/>.</returns>
    Task<IEnumerable<AdDto>> GetAdsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="AdDto"/> by id.
    /// </summary>
    /// <param name="id">Ad id</param>
    /// <returns><see cref="AdDto"/></returns>
    Task<AdDto> GetAdByIdAsync(Guid id, CancellationToken cancellationToken);

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
