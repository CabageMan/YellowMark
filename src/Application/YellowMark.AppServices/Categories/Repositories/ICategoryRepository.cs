using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Categories;

namespace YellowMark.AppServices.Categories.Repositories;

/// <summary>
/// Categories repositories.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Add new Category instance.
    /// </summary>
    /// <param name="entity">Category model <see cref="Domain.Categories.Entity.Category"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task AddAsync(Domain.Categories.Entity.Category entity, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the CategoryDto. 
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Collection of the <see cref="CategoryDto"/></returns>
    Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the <see cref="CategoryDto"/> matched to specification.
    /// </summary>
    /// <param name="specification">Filtering specification <see cref="Specification"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Collection of the <see cref="CategoryDto"/></returns>
    Task<IEnumerable<CategoryDto>> GetFiltered(Specification<Domain.Categories.Entity.Category> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="CategoryDto"/> by id.
    /// </summary>
    /// <param name="id">Category id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CategoryDto"/></returns>
    Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Update current Category.
    /// </summary>
    /// <param name="entity">Entity model <see cref="Domain.Categories.Entity.Category"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task UpdateAsync(Domain.Categories.Entity.Category entity, CancellationToken cancellationToken);

    /// <summary>
    /// Delete a Category by id.
    /// </summary>
    /// <param name="id">Category id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
