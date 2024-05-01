using YellowMark.Contracts;
using YellowMark.Contracts.Account;

namespace YellowMark.AppServices.Accounts.Services;

/// <summary>
/// Account service.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Add new account.
    /// </summary>
    /// <param name="request">Create account request <see cref="CreateAccountRequest"/></param>
    /// <param name="cancellationToken">Operation cancelation token <see cref="CancellationToken"/></param>
    /// <returns>Related user info id <see cref="UserInfo"/></returns>
    Task<Guid> RegisterAccountAssync(CreateAccountRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Sign in into existing account.
    /// </summary>
    /// <param name="request">Sign in request <see cref="SignInRequest"/></param>
    /// <param name="cancellationToken">Operation cancelation token <see cref="CancellationToken"/></param>
    /// <returns><see cref="LoginDto"/></returns>
    Task<LoginDto> SignInIntoAccountAssync(SignInRequest request, CancellationToken cancellationToken);

    /*
    /// <summary>
    /// Sign out from existing account.
    /// </summary>
    /// <param name="cancellationToken">Operation cancelation token <see cref="CancellationToken"/></param>
    /// <returns></returns>
    Task SignOutFromAccoutnAssync(CancellationToken cancellationToken);
    */

    /// <summary>
    /// Get current account info.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="AccountInfoDto"/></returns>
    Task<AccountInfoDto> GetAccountInfoAssync(CancellationToken cancellationToken);

    /// <summary>
    /// Update current account info.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="AccountInfoDto"/></returns>
    Task<AccountInfoDto> UpdateAccountInfoAssync(UpdateAccountRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Delete existing account.
    /// </summary>
    /// <param name="cancellationToken">Operation cancelation token <see cref="CancellationToken"/></param>
    /// <returns></returns>
    Task DeleteAccountAssync(CancellationToken cancellationToken);

    /// <summary>
    /// Add user roles to database.
    /// Temp. convenient method.
    /// </summary>
    /// <returns>List of roles names.</returns>
    Task<List<string>> CreateUserRolesAssync(CancellationToken cancellationToken);

    /// <summary>
    /// Add admin role to current account.
    /// Temp. convenient method.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    Task<List<string>> AddAdminRoleAssync(CancellationToken cancellationToken);

    /// <summary>
    /// Add superUser role to current account.
    /// Temp. convenient method.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    Task<List<string>> AddSuperUserRoleAssync(CancellationToken cancellationToken);
}
