using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using YellowMark.AppServices.Categories.Services;
using YellowMark.Contracts.Categories;

namespace YellowMark.Api.Controllers;

/// <summary>
/// Category Controller
/// </summary>
[ApiController]
[Route("api/v1/categories")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IValidator<CreateCategoryRequest> _categoryValidator;
    private readonly IValidator<Guid> _guidValidator;

    /// <summary>
    /// Init instance of <see cref="CategoryController"/>
    /// </summary>
    /// <param name="categoryService">Category service <see cref="ICategoryService"/></param>
    /// <param name="categoryValidator">Categories validator <see cref="IValidator"/></param>
    /// <param name="guidValidator">Guid validator <see cref="IValidator"/></param>
    public CategoryController(
        ICategoryService categoryService,
        IValidator<CreateCategoryRequest> categoryValidator,
        IValidator<Guid> guidValidator)
    {
        _categoryService = categoryService;
        _categoryValidator = categoryValidator;
        _guidValidator = guidValidator;
    }

    /// <summary>
    /// Create new Category.
    /// </summary>
    /// <param name="request">Category request model <see cref="CreateCategoryRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created category Id <see cref="Guid"/></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        // TODO: This method is available only for Admin.
        var validationResult = await _categoryValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) 
        {
            return BadRequest(validationResult.ToString());
        }
        
        var addedCategoryId = await _categoryService.AddCategoryAsync(request, cancellationToken);
        return Created(new Uri($"{Request.Path}/{addedCategoryId}", UriKind.Relative), addedCategoryId);
    }

    /// <summary>
    /// Returns all categories list.
    /// </summary>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Categories list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        // TODO: Implement all possible returning status codes.
        var result = await _categoryService.GetCategoriesAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Return category by id.
    /// </summary>
    /// <param name="id">Category id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Category <see cref="CategoryDto"/>.</returns>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetCategoryById(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken); 
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _categoryService.GetCategoryByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Update Category by Id.
    /// </summary>
    /// <param name="id">Needed to update category id.</param>
    /// <param name="request">Category request model <see cref="CreateCategoryRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated category <see cref="CategoryDto"/></returns>
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(CategoryDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateCategory(Guid id, CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        // TODO: Should be available only for admin.
        var guidValidationResult = await _guidValidator.ValidateAsync(id, cancellationToken); 
        if (!guidValidationResult.IsValid) 
        {
            return BadRequest(guidValidationResult.ToString());
        }

        var categoryValidationResult = await _categoryValidator.ValidateAsync(request, cancellationToken);
        if (!categoryValidationResult.IsValid) 
        {
            return BadRequest(categoryValidationResult.ToString());
        }
        // TODO: Handle exceptions

        var updatedCategory = await _categoryService.UpdateCategoryAsync(id, request, cancellationToken);
        return Ok(updatedCategory);
    }

    /// <summary>
    /// Delete Category by Id.
    /// </summary>
    /// <param name="id">Category id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Task</returns>
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Should be available only for admin.
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken); 
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        await _categoryService.DeleteCategoryByIdAsync(id, cancellationToken);
        // TODO: Handle exceptions

        return NoContent();
    }
}
