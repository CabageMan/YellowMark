using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using YellowMark.AppServices.Subcategories.Services;
using YellowMark.Contracts.Subcategories;

namespace YellowMark.Api.Controllers;

/// <summary>
/// Subcategory Controller
/// </summary>
[ApiController]
[Route("v1/subcategories")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class SubcategoryController : ControllerBase
{
    private readonly ISubcategoryService _subcategoryService;
    private readonly IValidator<CreateSubcategoryRequest> _subcategoryValidator;
    private readonly IValidator<Guid> _guidValidator;

    /// <summary>
    /// Init instance of <see cref="SubcategoryController"/>
    /// </summary>
    /// <param name="subcategoryService"></param>
    /// <param name="subcategoryValidator"></param>
    /// <param name="guidValidator"></param>
    public SubcategoryController(
        ISubcategoryService subcategoryService,
        IValidator<CreateSubcategoryRequest> subcategoryValidator,
        IValidator<Guid> guidValidator)
    {
        _subcategoryService = subcategoryService;
        _subcategoryValidator = subcategoryValidator;
        _guidValidator = guidValidator;
    }

    /// <summary>
    /// Create new Subcategory.
    /// </summary>
    /// <param name="request">Subcategory request model <see cref="CreateSubcategoryRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created subcategory Id <see cref="Guid"/></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateSubcategory(CreateSubcategoryRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _subcategoryValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) 
        {
            return BadRequest(validationResult.ToString());
        }
        
        var addedSubcategoryId = await _subcategoryService.AddSubcategoryAsync(request, cancellationToken);
        return Created(new Uri($"{Request.Path}/{addedSubcategoryId}", UriKind.Relative), addedSubcategoryId);
    }
}
