using YellowMark.Contracts.Ads;

namespace YellowMark.AppServices.Ads.Repositories;

/// <summary>
/// Ads repository.
/// </summary>
public interface IAdRepository
{
    /// <summary>
    /// Returns all instances of the AdDto. 
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="AdDto"/></returns>
    Task<IEnumerable<AdDto>> GetAllAsync(CancellationToken cancellationToken);
}
