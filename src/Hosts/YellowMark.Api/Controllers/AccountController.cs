using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using YellowMark.AppServices.Accounts.Services;
using YellowMark.AppServices.Validators;
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
    private readonly IValidator<CreateAccountRequest> _createAccountValidator;
    private readonly IValidator<SignInRequest> _loginValidator;
    private readonly IValidator<UpdateAccountRequest> _updateAccountValidator;
    private readonly ILogger<AccountController> _logger;

    /// <summary>
    /// Init instance of <see cref="AccountController"/>
    /// </summary>
    /// <param name="accountService">Account service <see cref="IAccountService"/></param>
    /// <param name="createAccountValidator">Creation account validator <see cref="CreateAccountValidator"/>.</param>
    /// <param name="loginValidator">Signin info validator <see cref="SigninValidator"/>.</param>
    /// <param name="updateAccountValidator">Updating account validator <see cref="UpdateAccountValidator"/>.</param>
    /// <param name="logger">Logger <see cref="ILogger"/></param>
    public AccountController(
        IAccountService accountService,
        IValidator<CreateAccountRequest> createAccountValidator,
        IValidator<SignInRequest> loginValidator,
        IValidator<UpdateAccountRequest> updateAccountValidator,
        ILogger<AccountController> logger)
    {
        _accountService = accountService;
        _createAccountValidator = createAccountValidator;
        _loginValidator = loginValidator;
        _updateAccountValidator = updateAccountValidator;
        _logger = logger;
    }

    /// <summary>
    /// Register new account.
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
        using var loggerScope = _logger.BeginScope("Register new account operation");
        _logger.LogInformation("Register account request");

        var validationResult = await _createAccountValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        var addedUserInfoId = await _accountService.RegisterAccountAssync(request, cancellationToken);

        _logger.LogInformation("Account successfully registered. Created UserInfo with id {Id}", addedUserInfoId);

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
        using var loggerScope = _logger.BeginScope("Sign in into account operation");
        _logger.LogInformation("Sign in into account request");

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

        _logger.LogInformation("Successful login with token {Token}", loginInfo.JwtToken);

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
        using var loggerScope = _logger.BeginScope("Get account info operation");
        _logger.LogInformation("Get account info request");
         
        var accountInfo = await _accountService.GetAccountInfoAssync(cancellationToken);

        if (accountInfo == null)
        {
            return NotFound();
        }

        _logger.LogInformation("Successful got user info for user {Name}", accountInfo.FirstName);

        return Ok(accountInfo);
    }

    /// <summary>
    /// Update account info.
    /// </summary>
    /// <param name="request">Update account info request <see cref="UpdateAccountRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Account info <see cref="AccountInfoDto"/></returns>
    [Authorize]
    [HttpPut]
    [Route("account")]
    [ProducesResponseType(typeof(AccountInfoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateAccountInfo(UpdateAccountRequest request, CancellationToken cancellationToken)
    {
        using var loggerScope = _logger.BeginScope("Update account info operation");
        _logger.LogInformation("Validate request");

        var validationResult = await _updateAccountValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToString());
        }

        _logger.LogInformation("Update account info request");

        var accountInfo = await _accountService.UpdateAccountInfoAssync(request, cancellationToken);

        if (accountInfo == null)
        {
            return NotFound();
        }

        _logger.LogInformation("Successful update account info.");

        return Ok(accountInfo);
    }

    /*
    /// <summary>
    /// Sign out from current account.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete]
    [Route("session")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        await _accountService.SignOutFromAccoutnAssync(cancellationToken);
        // TODO: Need to implement
        return NoContent();
    }
    */

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
        using var loggerScope = _logger.BeginScope("Deletion account operation");
        _logger.LogInformation("Delete account request");

        await _accountService.DeleteAccountAssync(cancellationToken);

        _logger.LogInformation("Successfull deleted.");

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
