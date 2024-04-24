using YellowMark.AppServices.UsersInfos.Services;
using Microsoft.AspNetCore.Mvc;
using YellowMark.Contracts.UsersInfos;
using YellowMark.Contracts.Pagination;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace YellowMark.Api.Controllers;

/// <summary>
/// User controller.
/// </summary>
[ApiController]
[Route("v1/users")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class UserInfoController : ControllerBase
{
    private readonly IUserInfoService _userService;
    private readonly IValidator<CreateUserInfoRequest> _userValidator;
    private readonly IValidator<Guid> _guidValidator;

    /// <summary>
    /// Init instance of <see cref="UserInfoController"/>.
    /// </summary>
    /// <param name="userService">User service.</param>
    /// <param name="userValidator">Creation User validator.</param>
    /// <param name="guidValidator">Guid validator.</param>
    public UserInfoController(
        IUserInfoService userService, 
        IValidator<CreateUserInfoRequest> userValidator,
        IValidator<Guid> guidValidator)
    {
        _userService = userService;
        _userValidator = userValidator;
        _guidValidator = guidValidator;
    }

    /// <summary>
    /// Create new User.
    /// </summary>
    /// <param name="request">User request model <see cref="CreateUserInfoRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created user Id <see cref="Guid"/></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateUser(CreateUserInfoRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _userValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) 
        {
            return BadRequest(validationResult.ToString());
        }
        
        var addedUserId = await _userService.AddUserAsync(request, cancellationToken);
        return Created(new Uri($"{Request.Path}/{addedUserId}", UriKind.Relative), addedUserId);
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
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllRequestWithPagination request, CancellationToken cancellationToken)
    {
        // TODO: Implement pagination validator.
        // TODO: Implement all possible returning status codes.
        var result = await _userService.GetUsersAsync(request, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Return user by id.
    /// </summary>
    /// <param name="id">User id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>User.</returns>
    [Authorize]
    [HttpGet("{id:Guid}")]
    // [Route("{id:Guid}")]
    [ProducesResponseType(typeof(UserInfoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken); 
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var result = await _userService.GetUserByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Returns users list filtered by name.
    /// </summary>
    /// <param name="request">Request <see cref="UserInfoByNameRequest"/></param> 
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Users list.</returns>
    [HttpGet]
    [Route("by-name")]
    [ProducesResponseType(typeof(IEnumerable<UserInfoDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllByName([FromQuery] UserInfoByNameRequest request, CancellationToken cancellationToken)
    {
        // TODO: Implement all possible returning status codes.
        var result = await _userService.GetUsersByNameAsync(request, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Update User by Id.
    /// </summary>
    /// <param name="id">Needed to update user id.</param>
    /// <param name="request">User request model <see cref="CreateUserInfoRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated user <see cref="UserInfoDto"/></returns>
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(UserInfoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateUser(Guid id, CreateUserInfoRequest request, CancellationToken cancellationToken)
    {
        var guidValidationResult = await _guidValidator.ValidateAsync(id, cancellationToken); 
        if (!guidValidationResult.IsValid) 
        {
            return BadRequest(guidValidationResult.ToString());
        }

        var userValidationResult = await _userValidator.ValidateAsync(request, cancellationToken);
        if (!userValidationResult.IsValid) 
        {
            return BadRequest(userValidationResult.ToString());
        }
        // TODO: Handle exceptions

        var updatedUser = await _userService.UpdateUserAsync(id, request, cancellationToken);
        return Ok(updatedUser);
    }

    /// <summary>
    /// Delete User by Id.
    /// </summary>
    /// <param name="id">User id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Task</returns>
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await _guidValidator.ValidateAsync(id, cancellationToken); 
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        await _userService.DeleteUserByIdAsync(id, cancellationToken);
        // TODO: Handle exceptions

        return NoContent();
    }
}