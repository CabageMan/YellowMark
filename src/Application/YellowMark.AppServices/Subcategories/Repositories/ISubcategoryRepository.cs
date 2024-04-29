using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Subcategories;
using YellowMark.Domain.Subcategories.Entity;

namespace YellowMark.AppServices.Subcategories.Repositories;

/// <summary>
/// Subcategories repository.
/// </summary>
public interface ISubcategoryRepository
{
    /// <summary>
    /// Add new Subcategory instance.
    /// </summary>
    /// <param name="entity">Subcategory model <see cref="Subcategory"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task AddAsync(Subcategory entity, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the SubcategoryDto. 
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Collection of <see cref="SubcategoryDto"/></returns>
    Task<IEnumerable<SubcategoryDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the <see cref="SubcategoryDto"/> matched to specification.
    /// </summary>
    /// <param name="specification">Filtering specification <see cref="Specification"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Collection of the <see cref="SubcategoryDto"/></returns>
    Task<IEnumerable<SubcategoryDto>> GetFiltered(Specification<Subcategory> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="SubcategoryDto"/> by id.
    /// </summary>
    /// <param name="id">Subcategory id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="SubcategoryDto"/></returns>
    Task<Subcategory> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Update current Subcategory.
    /// </summary>
    /// <param name="entity">Entity model <see cref="Subcategory"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task UpdateAsync(Subcategory entity, CancellationToken cancellationToken);

    /// <summary>
    /// Delete a Subcategory by id.
    /// </summary>
    /// <param name="id">Subcategory id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
