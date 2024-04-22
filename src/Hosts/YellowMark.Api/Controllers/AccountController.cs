using System.Net;
using Microsoft.AspNetCore.Mvc;
using YellowMark.AppServices.Accounts.Services;
using YellowMark.Contracts;
using YellowMark.Contracts.Account;

namespace YellowMark.Api.Controllers;

/// <summary>
/// Authentication and Authorization controller.
/// </summary>
[ApiController]
[Route("v1")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    /// <summary>
    /// Init instance of <see cref="AccountController"/>
    /// </summary>
    /// <param name="accountService"></param>
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request">Create account request model <see cref="CreateAccountRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Account Id</returns>
    [HttpPost]
    [Route("account")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RegisterAccount(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        // TODO: Validate Request
        var addedUserId = await _accountService.RegisterAccountAssync(request, cancellationToken);

        return StatusCode((int)HttpStatusCode.Created, addedUserId);
    }

    /// <summary>
    /// Sign in into account.
    /// </summary>
    /// <param name="request">Sign in request model <see cref="SignInRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Login info <see cref="LoginDto"/></returns>
    [HttpPost]
    [Route("session")]
    [ProducesResponseType(typeof(LoginDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> LoginIntoAccount(SignInRequest request, CancellationToken cancellationToken)
    {
        // TODO: Validate Request
        var loginInfo = await _accountService.SignInIntoAccountAssync(request, cancellationToken);

        return Ok(loginInfo);
    }

    /// <summary>
    /// Get account info.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Account info <see cref="AccountInfoDto"/></returns>
    [HttpGet]
    [Route("account")]
    [ProducesResponseType(typeof(AccountInfoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAccountInfo(CancellationToken cancellationToken)
    {
        var accountInfo = await _accountService.GetUserInfoAssync(cancellationToken);

        return Ok(accountInfo);
    }

    /// <summary>
    /// Sign out from current account.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("session")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
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
    [HttpDelete]
    [Route("account")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteAccount(CancellationToken cancellationToken)
    {
        await _accountService.DeleteAccountAssync(cancellationToken);

        return NoContent();
    }
}
