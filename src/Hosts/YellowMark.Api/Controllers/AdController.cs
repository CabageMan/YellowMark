using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YellowMark.AppServices.Ads.Services;
using YellowMark.Contracts.Ads;
using YellowMark.Contracts.Pagination;

namespace YellowMark.Api.Controllers;

/// <summary>
/// Ad controller.
/// </summary>
[ApiController]
[Route("api/v1/ads")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class AdController : ControllerBase
{
    private readonly IAdService _adService;
    private readonly IValidator<CreateAdRequest> _adValidator;
    private readonly IValidator<GetAllRequestWithPagination> _paginationRequestValidator;
    private readonly IValidator<Guid> _guidValidator;

    /// <summary>
    /// Init instance of <see cref="AdController"/>.
    /// </summary>
    /// <param name="adService">Ad service.</param>
    /// <param name="adValidator">Creation Ad validator.</param>
    /// <param name="paginationRequestValidator">Pagination params validator</param>
    /// <param name="guidValidator">Guid validator.</param>
    public AdController(
        IAdService adService,
        IValidator<CreateAdRequest> adValidator,
        IValidator<GetAllRequestWithPagination> paginationRequestValidator,
        IValidator<Guid> guidValidator)
    {
        _adService = adService;
        _adValidator = adValidator;
        _paginationRequestValidator = paginationRequestValidator;
        _guidValidator = guidValidator;
    }

    /// <summary>
    /// Create new Ad. Available only for authorized users.
    /// </summary>
    /// <param name="request">Ad request model <see cref="CreateAdRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created ad Id <see cref="Guid"/></returns>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAd(CreateAdRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _adValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var addedAdId = await _adService.AddAdAsync(request, cancellationToken);
        return Created(new Uri($"{Request.Path}/{addedAdId}", UriKind.Relative), addedAdId);
    }

    /// <summary>
    /// Returns all ads list with pagination. Available for anonymous users.
    /// </summary>
    /// <param name="request">Pagination params <see cref="GetAllRequestWithPagination"/>.</param>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Ads list.</returns>
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(ResultWithPagination<AdDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllAds([FromQuery] GetAllRequestWithPagination request, CancellationToken cancellationToken)
    {
        var validationResult = await _paginationRequestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _adService.GetAdsAsync(request, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Return ad by id. Available for anonymous users.
    /// </summary>
    /// <param name="id">Ad id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Ad.</returns>
    [AllowAnonymous]
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(AdDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAdById(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _adService.GetAdByIdAsync(id, cancellationToken);
        if (result == null)
        {
            return NotFound("Could not find ad.");
        }

        return Ok(result);
    }

    /// <summary>
    /// Returns ads list filtered by title. Available for anonymous users.
    /// </summary>
    /// <param name="request">Request <see cref="AdByTitleRequest"/></param> 
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Ads list.</returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("by-title")]
    [ProducesResponseType(typeof(IEnumerable<AdDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllByTitle([FromQuery] AdByTitleRequest request, CancellationToken cancellationToken)
    {
        var result = await _adService.GetAdsByTitleAsync(request, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Returns ads list filtered by category. Available for anonymous users.
    /// </summary>
    /// <param name="categoryID">Category id.</param>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Ads list.</returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("by-category")]
    [ProducesResponseType(typeof(IEnumerable<AdDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllByCategory([FromQuery] Guid categoryID, CancellationToken cancellationToken)
    {
        var result = await _adService.GetAdsByCategoryAsync(categoryID, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Update Ad by Id. Available only for authorized users.
    /// </summary>
    /// <param name="id">Needed to update ad.</param>
    /// <param name="request">Ad request model <see cref="CreateAdRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated ad <see cref="AdDto"/></returns>
    [Authorize]
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(AdDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateAd(Guid id, CreateAdRequest request, CancellationToken cancellationToken)
    {
        var guidValidationResult = await _guidValidator.ValidateAsync(id, cancellationToken);
        if (!guidValidationResult.IsValid)
        {
            return BadRequest(guidValidationResult.ToString());
        }

        var adValidationResult = await _adValidator.ValidateAsync(request, cancellationToken);
        if (!adValidationResult.IsValid)
        {
            return BadRequest(adValidationResult.ToString());
        }

        var updatedAd = await _adService.UpdateAdAsync(id, request, cancellationToken);
        if (updatedAd == null)
        {
            return NotFound("Could not find ad to update.");
        }

        return Ok(updatedAd);
    }

    /// <summary>
    /// Delete Ad by Id. Available only for authorized users.
    /// </summary>
    /// <param name="id">Ad id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Task</returns>
    [Authorize]
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteAd(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        await _adService.DeleteAdByIdAsync(id, cancellationToken);

        return NoContent();
    }
}
