using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using YellowMark.AppServices.Currencies.Services;
using YellowMark.Contracts.Currnecies;

namespace YellowMark.Api.Controllers;

/// <summary>
/// Currency Controller
/// </summary>
[ApiController]
[Route("v1/currencies")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;
    private readonly IValidator<CreateCurrencyRequest> _currencyValidator;
    private readonly IValidator<Guid> _guidValidator;

    /// <summary>
    /// Init instance of <see cref="CurrencyController"/>
    /// </summary>
    /// <param name="currencyService">Currency service <see cref="ICurrencyService"/></param>
    /// <param name="currencyValidator">Currency validator <see cref="IValidator"/></param>
    /// <param name="guidValidator">Guid Validator <see cref="IValidator"/></param>
    public CurrencyController(
        ICurrencyService currencyService,
        IValidator<CreateCurrencyRequest> currencyValidator,
        IValidator<Guid> guidValidator)
    {
        _currencyService = currencyService;
        _currencyValidator = currencyValidator;
        _guidValidator = guidValidator;
    }

    /// <summary>
    /// Create new Currency.
    /// </summary>
    /// <param name="request">Currency request model <see cref="CreateCurrencyRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created currency Id <see cref="Guid"/></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateCurrency(CreateCurrencyRequest request, CancellationToken cancellationToken)
    {
        // TODO: This method is available only for Admin.
        var validationResult = await _currencyValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) 
        {
            return BadRequest(validationResult.ToString());
        }
        var addedCurrencyId = await _currencyService.AddCurrencyAsync(request, cancellationToken);
        return Created(new Uri($"{Request.Path}/{addedCurrencyId}", UriKind.Relative), addedCurrencyId);
    }

    /// <summary>
    /// Returns all currencies list.
    /// </summary>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Currencies list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CurrencyDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllCurrencies(CancellationToken cancellationToken)
    {
        // TODO: Implement all possible returning status codes.
        var result = await _currencyService.GetCurrenciesAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Return currency by id.
    /// </summary>
    /// <param name="id">Currency id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Currency <see cref="CurrencyDto"/>.</returns>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(CurrencyDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetCurrencyById(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken); 
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _currencyService.GetCurrencyByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Update Currency by Id.
    /// </summary>
    /// <param name="id">Needed to update currency.</param>
    /// <param name="request">Currency request model <see cref="CreateCurrencyRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated currency <see cref="CurrencyDto"/></returns>
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(CurrencyDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateCurrency(Guid id, CreateCurrencyRequest request, CancellationToken cancellationToken)
    {
        // TODO: Should be available only for admin.
        var guidValidationResult = await _guidValidator.ValidateAsync(id, cancellationToken); 
        if (!guidValidationResult.IsValid) 
        {
            return BadRequest(guidValidationResult.ToString());
        }

        var currencyValidationResult = await _currencyValidator.ValidateAsync(request, cancellationToken);
        if (!currencyValidationResult.IsValid) 
        {
            return BadRequest(currencyValidationResult.ToString());
        }
        // TODO: Handle exceptions

        var updatedCurrency = await _currencyService.UpdateCurrencyAsync(id, request, cancellationToken);
        return Ok(updatedCurrency);
    }

    /// <summary>
    /// Delete Currency by Id.
    /// </summary>
    /// <param name="id">Currency id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Task</returns>
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteCurrency(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Should be available only for admin.
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken); 
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        await _currencyService.DeleteCurrencyByIdAsync(id, cancellationToken);
        // TODO: Handle exceptions

        return NoContent();
    }
}
