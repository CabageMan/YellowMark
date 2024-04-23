using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using YellowMark.AppServices.UsersInfos.Services;
using YellowMark.Contracts;
using YellowMark.Contracts.Account;
using YellowMark.Contracts.UsersInfos;
using YellowMark.Domain.Accounts.Entity;
using YellowMark.Domain.UserRoles;

namespace YellowMark.AppServices.Accounts.Services;

/// <inheritdoc cref="IAccountService"/>
public class AccountService : IAccountService
{
    private readonly UserManager<Account> _userManager;
    private readonly SignInManager<Account> _signInManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IUserInfoService _userInfoService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init <see cref="AccountService"/> instance.
    /// </summary>
    /// <param name="userManager">User manager <see cref="UserManager"/></param>
    /// <param name="signInManager">Sign in manager <see cref="SignInManager"/></param>
    /// <param name="roleManager">Role manager <see cref="IdentityRole"/></param>
    /// <param name="configuration">App configuration <see cref="IConfiguration"/></param>
    /// <param name="mapper">Account mapper <see cref="IMapper"/></param>
    public AccountService(
        UserManager<Account> userManager,
        SignInManager<Account> signInManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IUserInfoService userInfoService,
        IConfiguration configuration,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _userInfoService = userInfoService;
        _configuration = configuration;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> RegisterAccountAssync(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var existedAccount = await _userManager.FindByEmailAsync(request.Email);
        if (existedAccount != null)
        {
            throw new InvalidOperationException($"Acount with email {request.Email} is already existed.");
        }

        var account = new Account()
        {
            Email = request.Email,
            PhoneNumber = request.Phone,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var registerResult = await _userManager.CreateAsync(account, request.Password);
        if (!registerResult.Succeeded)
        {
            throw new InvalidOperationException($"Could not create account for {request.Email}. Reason: {registerResult.Errors}");
        }


        var addRoleResult = await _userManager.AddToRoleAsync(account, UserRoles.User);
        if (!addRoleResult.Succeeded)
        {
            throw new InvalidOperationException($"Could not create account for {request.Email}");
        }

        var userInfo = _mapper.Map<CreateAccountRequest, CreateUserInfoRequest>(request);
        userInfo.AccountId = account.Id;
        var userInfoId = await _userInfoService.AddUserAsync(userInfo, cancellationToken);

        return userInfoId;
    }

    /// <inheritdoc/>
    public Task<LoginDto> SignInIntoAccountAssync(SignInRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<AccountInfoDto> GetUserInfoAssync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task SignOutFromAccoutnAssync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task DeleteAccountAssync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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

    // Helpers.
    private string GenerateJwtToken()
    {

        //  Salt password.
        var token = new JwtSecurityToken();
        return "";
    }
}
