using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using YellowMark.AppServices.UsersInfos.Services;
using YellowMark.Contracts.Account;
using YellowMark.Contracts.UsersInfos;
using YellowMark.Domain.Accounts.Entity;
using YellowMark.Domain.UserRoles;

namespace YellowMark.AppServices.Accounts.Services;

/// <inheritdoc cref="IAccountService"/>
public class AccountService : IAccountService
{
    private readonly UserManager<Account> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserInfoService _userInfoService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountService> _logger;

    /// <summary>
    /// Init <see cref="AccountService"/> instance.
    /// </summary>
    /// <param name="userManager">User manager <see cref="UserManager"/></param>
    /// <param name="roleManager">Role manager <see cref="IdentityRole"/></param>
    /// <param name="httpContextAccessor">Http context accessor <see cref="IHttpContextAccessor"/></param>
    /// <param name="userInfoService">Userinfo service <see cref="IUserInfoService"/></param>
    /// <param name="configuration">App configuration <see cref="IConfiguration"/></param>
    /// <param name="mapper">Account mapper <see cref="IMapper"/></param>
    /// <param name="logger">Logger <see cref="ILogger"/></param>
    public AccountService(
        UserManager<Account> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IHttpContextAccessor httpContextAccessor,
        IUserInfoService userInfoService,
        IConfiguration configuration,
        IMapper mapper,
        ILogger<AccountService> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
        _userInfoService = userInfoService;
        _configuration = configuration;
        _mapper = mapper;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<Guid> RegisterAccountAssync(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to find existed account with the same creds.");

        var existedAccount = await _userManager.FindByEmailAsync(request.Email);
        if (existedAccount != null)
        {
            throw new InvalidOperationException($"Acount with email {request.Email} is already existed.");
        }

        var account = new Account()
        {
            UserName = request.FirstName,
            Email = request.Email,
            PhoneNumber = request.Phone,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        _logger.LogInformation("Try to create new account");

        var registerResult = await _userManager.CreateAsync(account, request.Password);
        if (!registerResult.Succeeded)
        {
            var errors = registerResult.Errors.Select(error => error.Description);
            var errorString = string.Join("\n", errors);
            throw new InvalidOperationException($"Could not create account for {request.Email}. Reason: {errorString}");
        }

        _logger.LogInformation("Try to add role to the new account");

        var addRoleResult = await _userManager.AddToRoleAsync(account, UserRoles.User);
        if (!addRoleResult.Succeeded)
        {
            var errors = addRoleResult.Errors.Select(error => error.Description);
            var errorString = string.Join("\n", errors);
            throw new InvalidOperationException($"Could not add role for account {request.Email}. Reason: {errorString}");
        }

        _logger.LogInformation("Try to create related to account user info with account id {Id}", account.Id);

        var userInfo = _mapper.Map<CreateAccountRequest, CreateUserInfoRequest>(request);
        userInfo.AccountId = account.Id;
        var userInfoId = await _userInfoService.AddUserAsync(userInfo, cancellationToken);

        return userInfoId;
    }

    /// <inheritdoc/>
    public async Task<LoginDto> SignInIntoAccountAssync(SignInRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to find existed account.");

        var account = await _userManager.FindByEmailAsync(request.Email);
        if (account == null)
        {
            throw new InvalidOperationException($"Account with email {request.Email} does not exist.");
        }

        _logger.LogInformation("Check password.");

        var isPasswordValid = await _userManager.CheckPasswordAsync(account, request.Password);
        if (!isPasswordValid)
        {
            throw new InvalidOperationException($"Password for {request.Email} does not match.");
        }

        _logger.LogInformation("Try to get roles for account.");

        var accountRoles = await _userManager.GetRolesAsync(account);

        if (account.UserName == null || account.Email == null || account.PhoneNumber == null)
        {
            throw new InvalidOperationException("Could not get account claims.");
        }
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, account.UserName),
            new(ClaimTypes.Email, account.Email),
            new(ClaimTypes.MobilePhone, account.PhoneNumber)
        };
        foreach (var userRole in accountRoles)
        {
            authClaims.Add(new(ClaimTypes.Role, userRole));
        }

        _logger.LogInformation("Try to get user info related to account.");

        var userInfo = await _userInfoService.GetUserByAccountIdAsync(account.Id, cancellationToken);
        var token = GenerateJwtToken(authClaims);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginDto()
        {
            UserInfoId = userInfo.Id,
            JwtToken = tokenString,
            Roles = accountRoles.ToList()
        };
    }

    /// <inheritdoc/>
    public async Task<AccountInfoDto> UpdateAccountInfoAssync(UpdateAccountRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to find existed account from token.");

        var account = await GetAccountByTokenAsync();
        account.UserName = request.FirstName;
        account.Email = request.Email;
        account.PhoneNumber = request.Phone;

        _logger.LogInformation("Try to get related to account user info.");

        var currentUserInfo = await _userInfoService.GetUserByAccountIdAsync(account.Id, cancellationToken);

        var updatedUserInfo = _mapper.Map<UpdateAccountRequest, UpdateUserInfoRequest>(request);
        updatedUserInfo.Id = currentUserInfo.Id;
        updatedUserInfo.AccountId = account.Id;
        updatedUserInfo.CreatedAt = currentUserInfo.CreatedAt;

        _logger.LogInformation("Try to update account.");

        await _userManager.UpdateAsync(account);

        _logger.LogInformation("Try to update related user info.");

        var userInfoDto = await _userInfoService.UpdateUserAsync(updatedUserInfo, cancellationToken);

        var accountInfo = _mapper.Map<UserInfoDto, AccountInfoDto>(userInfoDto);
        accountInfo.Email = account.Email;
        accountInfo.Phone = account.PhoneNumber;

        return accountInfo;
    }

    /// <inheritdoc/>
    public async Task<AccountInfoDto> GetAccountInfoAssync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to find existed account from token info. And get info.");

        var existedAccount = await GetAccountByTokenAsync();
        var userInfo = await _userInfoService.GetUserByAccountIdAsync(existedAccount.Id, cancellationToken);
        var accountInfo = _mapper.Map<UserInfoDto, AccountInfoDto>(userInfo);
        accountInfo.Email = existedAccount.Email;
        accountInfo.Phone = existedAccount.PhoneNumber;

        return accountInfo;
    }

    /*
    /// <inheritdoc/>
    public Task SignOutFromAccoutnAssync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    */

    /// <inheritdoc/>
    public async Task DeleteAccountAssync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try to find existed account from token info. And get info.");

        var existedAccount = await GetAccountByTokenAsync();
        var userInfo = await _userInfoService.GetUserByAccountIdAsync(existedAccount.Id, cancellationToken);

        _logger.LogInformation("Delete account and related user infos.");

        await _userInfoService.DeleteUserByIdAsync(userInfo.Id, cancellationToken);
        await _userManager.DeleteAsync(existedAccount);
    }

    /// <inheritdoc/>
    public async Task<List<string>> CreateUserRolesAssync(CancellationToken cancellationToken)
    {
        var result = new List<string>();

        if (!await _roleManager.RoleExistsAsync(UserRoles.SuperUser))
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.SuperUser));
            result.Add(UserRoles.SuperUser);
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Admin));
            result.Add(UserRoles.Admin);
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.User));
            result.Add(UserRoles.User);
        }

        return result;
    }

    /// <inheritdoc/>
    public async Task<List<string>> AddAdminRoleAssync(CancellationToken cancellationToken)
    {
        return await AddRoleToAccountAssync(UserRoles.Admin);
    }

    /// <inheritdoc/>
    public async Task<List<string>> AddSuperUserRoleAssync(CancellationToken cancellationToken)
    {
        return await AddRoleToAccountAssync(UserRoles.SuperUser);
    }

    // Helpers.
    private JwtSecurityToken GenerateJwtToken(List<Claim> claims)
    {
        var secretKey = _configuration.GetSection("Jwt")["SecretKey"];
        if (secretKey == null)
        {
            throw new ArgumentNullException("Secret key is null.");
        }
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: DateTime.Now.AddHours(1),
            claims: claims,
            signingCredentials: credentials
        );
    }

    private async Task<Account> GetAccountByTokenAsync()
    {
        var context = _httpContextAccessor.HttpContext;
        ArgumentNullException.ThrowIfNull(context);

        var tokenString = await context.GetTokenAsync("access_token");
        ArgumentNullException.ThrowIfNull(tokenString);

        var emailClaim = new JwtSecurityTokenHandler()
            .ReadJwtToken(tokenString)
            .Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email);
        ArgumentNullException.ThrowIfNull(emailClaim);

        var email = emailClaim.Value;
        ArgumentNullException.ThrowIfNull(email);

        var existedAccount = await _userManager.FindByEmailAsync(email);
        if (existedAccount == null)
        {
            throw new InvalidOperationException($"Could not find account related to {email}.");
        }
        return existedAccount;
    }

    private async Task<List<string>> AddRoleToAccountAssync(string userRole)
    {
        var account = await GetAccountByTokenAsync();

        var accountRoles = await _userManager.GetRolesAsync(account);

        if (accountRoles.Contains(userRole))
        {
            return (List<string>)accountRoles;
        }

        var addRoleResult = await _userManager.AddToRoleAsync(account, userRole);
        if (!addRoleResult.Succeeded)
        {
            var errors = addRoleResult.Errors.Select(error => error.Description);
            var errorString = string.Join("\n", errors);
            throw new InvalidOperationException($"Could not add role for account {account.UserName}. Reason: {errorString}");
        }
        accountRoles.Add(userRole);

        return (List<string>)accountRoles;
    }
}
