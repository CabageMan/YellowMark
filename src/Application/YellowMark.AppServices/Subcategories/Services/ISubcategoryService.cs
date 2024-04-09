using YellowMark.Contracts.Subcategories;

namespace YellowMark.AppServices.Subcategories.Services;

/// <summary>
/// Subcategories service.
/// </summary>
public interface ISubcategoryService
{
    /// <summary>
    /// Create new Subcategory instance from the request params. 
    /// </summary>
    /// <param name="request">Subcategory request model <see cref="CreateSubcategoryRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created Subcategory id <see cref="Guid"/></returns>
    Task<Guid> AddSubcategoryAsync(CreateSubcategoryRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all subcategories.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Subcategories collection <see cref="SubcategoryDto"/>.</returns>
    Task<IEnumerable<SubcategoryDto>> GetSubcategoriesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="SubcategoryDto"/> by id.
    /// </summary>
    /// <param name="id">Subcategory id</param>
    /// <returns><see cref="SubcategoryDto"/></returns>
    Task<SubcategoryDto> GetSubcategoryByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Update current Subcategory.
    /// </summary>
    /// <param name="id">Subcategory id</param>
    /// <param name="request">Creation model <see cref="CreateSubcategoryRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated subcategory <see cref="SubcategoryDto"/></returns>
    Task<SubcategoryDto> UpdateSubcategoryAsync(Guid id, CreateSubcategoryRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Delete the Subcategory by id.
    /// </summary>
    /// <param name="id">Subcategory id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteSubcategoryByIdAsync(Guid id, CancellationToken cancellationToken);
}
