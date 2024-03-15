using YellowMark.Contracts.Categories;

namespace YellowMark.AppServices.Categories.Repositories;

/// <summary>
/// Categories repositories.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Returns all instances of the CategoryDto. 
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CategoryDto"/></returns>
    Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken);
}
