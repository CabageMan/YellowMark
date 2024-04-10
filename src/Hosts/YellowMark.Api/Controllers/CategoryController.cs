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
[Route("v1/categories")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IValidator<CreateCategoryRequest> _categoryValidator;
    private readonly IValidator<Guid> _guidValidator;

    /// <summary>
    /// Init instance of <see cref="CategoryController"/>
    /// </summary>
    /// <param name="categoryService"></param>
    /// <param name="categoryValidator"></param>
    /// <param name="guidValidator"></param>
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
        var validationResult = await _categoryValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) 
        {
            return BadRequest(validationResult.ToString());
        }
        
        var addedCategoryId = await _categoryService.AddCategoryAsync(request, cancellationToken);
        return Created(new Uri($"{Request.Path}/{addedCategoryId}", UriKind.Relative), addedCategoryId);
    }
}
