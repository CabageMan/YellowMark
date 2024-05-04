using YellowMark.Contracts.Categories;

namespace YellowMark.AppServices.Categories.Services;

/// <summary>
/// Categories service.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Create new Category instance from the request params. 
    /// </summary>
    /// <param name="request">Category request model <see cref="CreateCategoryRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created Category id <see cref="Guid"/></returns>
    Task<Guid> AddCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all categories.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Categories collection <see cref="CategoryDto"/>.</returns>
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="CategoryDto"/> by id.
    /// </summary>
    /// <param name="id">Category id</param>
    /// <returns><see cref="CategoryDto"/></returns>
    Task<CategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Check if category exists by Id.
    /// </summary>
    /// <param name="id">Category Id.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Bool if category exist.</returns>
    Task<bool> CategoryExistsWithId(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Update current Category.
    /// </summary>
    /// <param name="id">Category id</param>
    /// <param name="request">Update model <see cref="UpdateCategoryRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated category <see cref="CategoryDto"/></returns>
    Task<CategoryDto> UpdateCategoryAsync(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Delete the Category by id.
    /// </summary>
    /// <param name="id">Category id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteCategoryByIdAsync(Guid id, CancellationToken cancellationToken);
}
