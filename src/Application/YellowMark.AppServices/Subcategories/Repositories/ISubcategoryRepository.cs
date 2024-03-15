using YellowMark.Contracts.Subcategories;

namespace YellowMark.AppServices.Subcategories.Repositories;

/// <summary>
/// Subcategories repository.
/// </summary>
public interface ISubcategoryRepository
{
    /// <summary>
    /// Returns all instances of the SubcategoryDto. 
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="SubcategoryDto"/></returns>
    Task<IEnumerable<SubcategoryDto>> GetAllAsync(CancellationToken cancellationToken);
}
