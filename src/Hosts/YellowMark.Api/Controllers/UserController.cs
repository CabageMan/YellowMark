using YellowMark.AppServices.Users.Services;
using Microsoft.AspNetCore.Mvc;
using YellowMark.Contracts.Users;
using System.Net;
using YellowMark.Contracts;
using FluentValidation;

namespace YellowMark.Api.Controllers;

/// <summary>
/// User controller.
/// </summary>
// TODO: Check API versioning and route. For example: ...api/v1/users/...
[ApiController]
[Route("[controller]")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<CreateUserRequest> _userValidator;

    /// <summary>
    /// Init instance of <see cref="UserController"/>.
    /// </summary>
    /// <param name="userService">User service.</param>
    /// <param name="userValidator">Creation User validator.</param>
    public UserController(IUserService userService, IValidator<CreateUserRequest> userValidator)
    {
        _userService = userService;
        _userValidator = userValidator;
    }

    /// <summary>
    /// Returns users list.
    /// </summary>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Users list.</returns>
    [HttpGet]
    [Route("all")]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        // TODO: Implement all possible returning status codes.
        var result = await _userService.GetUsersAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Create new User.
    /// </summary>
    /// <param name="request">User request model <see cref="CreateUserRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created user <see cref="UserDto"/></returns>
    [HttpPost]
    [Route("user")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _userValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) 
        {
            return BadRequest(validationResult.ToString());
        }
        
        var result = await _userService.CreateUserAsync(request, cancellationToken);
        return Created(new Uri($"{Request.Path}/{result.Id}", UriKind.Relative), result);
    }
}