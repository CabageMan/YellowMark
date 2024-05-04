using YellowMark.AppServices.UsersInfos.Services;
using Microsoft.AspNetCore.Mvc;
using YellowMark.Contracts.UsersInfos;
using YellowMark.Contracts.Pagination;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;

namespace YellowMark.Api.Controllers;

/// <summary>
/// Userifo controller.
/// </summary>
[ApiController]
[Route("api/v1/users")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class UserInfoController : ControllerBase
{
    private readonly IUserInfoService _userService;
    private readonly IValidator<GetAllRequestWithPagination> _paginationRequestValidator;
    private readonly IValidator<Guid> _guidValidator;

    /// <summary>
    /// Init instance of <see cref="UserInfoController"/>.
    /// </summary>
    /// <param name="userService">User service.</param>
    /// <param name="guidValidator">Guid validator.</param>
    /// <param name="paginationRequestValidator">Pagination params validator</param>
    public UserInfoController(
        IUserInfoService userService, 
        IValidator<Guid> guidValidator,
        IValidator<GetAllRequestWithPagination> paginationRequestValidator)
    {
        _userService = userService;
        _guidValidator = guidValidator;
        _paginationRequestValidator = paginationRequestValidator;
    }

    /// <summary>
    /// Returns all users list with pagination.
    /// </summary>
    /// <param name="request">Pagination params <see cref="GetAllRequestWithPagination"/>.</param>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Users list.</returns>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ResultWithPagination<UserInfoDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllRequestWithPagination request, CancellationToken cancellationToken)
    {
        var validationResult = await _paginationRequestValidator.ValidateAsync(request, cancellationToken); 
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _userService.GetUsersAsync(request, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Return user info by id.
    /// </summary>
    /// <param name="id">User id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>User.</returns>
    [Authorize]
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(UserInfoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken); 
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _userService.GetUserByIdAsync(id, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Returns users infos list filtered by name.
    /// </summary>
    /// <param name="request">Request <see cref="UserInfoByNameRequest"/></param> 
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Users list.</returns>
    [Authorize]
    [HttpGet]
    [Route("by-name")]
    [ProducesResponseType(typeof(IEnumerable<UserInfoDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllByName([FromQuery] UserInfoByNameRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.GetUsersByNameAsync(request, cancellationToken);
        return Ok(result);
    }
}