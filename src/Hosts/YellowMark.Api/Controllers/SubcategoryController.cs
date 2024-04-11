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
    /// <param name="subcategoryService">Subcatergory service <see cref="ISubcategoryService"/></param>
    /// <param name="subcategoryValidator">Subcategory validator <see cref="IValidator"/></param>
    /// <param name="guidValidator">Guid validato <see cref="IValidator"/></param>
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

    /// <summary>
    /// Returns all subcategories list.
    /// </summary>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Subcategories list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SubcategoryDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllSubcategories(CancellationToken cancellationToken)
    {
        // TODO: Implement all possible returning status codes.
        var result = await _subcategoryService.GetSubcategoriesAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Return subcategory by id.
    /// </summary>
    /// <param name="id">Subcategory id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Subcategory <see cref="SubcategoryDto"/>.</returns>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(SubcategoryDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetSubcategoryById(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken); 
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _subcategoryService.GetSubcategoryByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Update Subcategory by Id.
    /// </summary>
    /// <param name="id">Needed to update subcategory id.</param>
    /// <param name="request">Subcategory request model <see cref="CreateSubcategoryRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated subcategory <see cref="SubcategoryDto"/></returns>
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(SubcategoryDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateSubcategory(Guid id, CreateSubcategoryRequest request, CancellationToken cancellationToken)
    {
        // TODO: Should be available only for admin.
        var guidValidationResult = await _guidValidator.ValidateAsync(id, cancellationToken); 
        if (!guidValidationResult.IsValid) 
        {
            return BadRequest(guidValidationResult.ToString());
        }

        var subcategoryValidationResult = await _subcategoryValidator.ValidateAsync(request, cancellationToken);
        if (!subcategoryValidationResult.IsValid) 
        {
            return BadRequest(subcategoryValidationResult.ToString());
        }
        // TODO: Handle exceptions

        var updatedCategory = await _subcategoryService.UpdateSubcategoryAsync(id, request, cancellationToken);
        return Ok(updatedCategory);
    }

    /// <summary>
    /// Delete Subcategory by Id.
    /// </summary>
    /// <param name="id">Subcategory id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Task</returns>
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteSubcategory(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Should be available only for admin.
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken); 
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        await _subcategoryService.DeleteSubcategoryByIdAsync(id, cancellationToken);
        // TODO: Handle exceptions

        return NoContent();
    }
}
