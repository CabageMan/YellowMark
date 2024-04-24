using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using YellowMark.AppServices.Accounts.Services;
using YellowMark.Contracts;
using YellowMark.Contracts.Account;

namespace YellowMark.Api.Controllers;

/// <summary>
/// Authentication and Authorization controller.
/// </summary>
[ApiController]
[Route("api/v1")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IValidator<CreateAccountRequest> _accountValidator;
    private readonly IValidator<SignInRequest> _loginValidator;

    /// <summary>
    /// Init instance of <see cref="AccountController"/>
    /// </summary>
    /// <param name="accountService">Account service <see cref="IAccountService"/></param>
    /// <param name="accountValidator">Creation account validator.</param>
    /// <param name="loginValidator">Signin info validator.</param>
    public AccountController(
        IAccountService accountService,
        IValidator<CreateAccountRequest> accountValidator,
        IValidator<SignInRequest> loginValidator)
    {
        _accountService = accountService;
        _accountValidator = accountValidator;
        _loginValidator = loginValidator;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request">Create account request model <see cref="CreateAccountRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Related user info id <see cref="Guid"/>.</returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("account")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RegisterAccount(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _accountValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var addedUserInfoId = await _accountService.RegisterAccountAssync(request, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, addedUserInfoId);
    }

    /// <summary>
    /// Sign in into account.
    /// </summary>
    /// <param name="request">Sign in request model <see cref="SignInRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Login info <see cref="LoginDto"/></returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("session")]
    [ProducesResponseType(typeof(LoginDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> LoginIntoAccount(SignInRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _loginValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var loginInfo = await _accountService.SignInIntoAccountAssync(request, cancellationToken);

        if (loginInfo == null)
        {
            return Unauthorized();
        }

        return Ok(loginInfo);
    }

    /// <summary>
    /// Get account info.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Account info <see cref="AccountInfoDto"/></returns>
    [Authorize]
    [HttpGet]
    [Route("account")]
    [ProducesResponseType(typeof(AccountInfoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAccountInfo(CancellationToken cancellationToken)
    {
        var accountInfo = await _accountService.GetUserInfoAssync(cancellationToken);

        if (accountInfo == null)
        {
            return NotFound();
        }

        return Ok(accountInfo);
    }

    /// <summary>
    /// Sign out from current account.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    // [Authorize]
    [HttpDelete]
    [Route("session")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        await _accountService.SignOutFromAccoutnAssync(cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Delete current account.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete]
    [Route("account")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteAccount(CancellationToken cancellationToken)
    {
        await _accountService.DeleteAccountAssync(cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Add Users Roles.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Login info <see cref="LoginDto"/></returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("account/roles")]
    [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> AddUsersRoles(CancellationToken cancellationToken)
    {
        var result = await _accountService.CreateUserRolesAssync(cancellationToken);
        if (result.IsNullOrEmpty())
        {
            return Conflict("Roles are already added.");
        }
        return StatusCode((int)HttpStatusCode.Created, result);
    }
}
