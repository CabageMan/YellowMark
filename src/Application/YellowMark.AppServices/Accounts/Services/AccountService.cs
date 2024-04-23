﻿using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
            UserName = request.FirstName,
            Email = request.Email,
            PhoneNumber = request.Phone,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var registerResult = await _userManager.CreateAsync(account, request.Password);
        if (!registerResult.Succeeded)
        {
            var errors = registerResult.Errors.Select(error => error.Description);
            var errorString = string.Join("\n", errors);
            throw new InvalidOperationException($"Could not create account for {request.Email}. Reason: {errorString}");
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
    public async Task<LoginDto> SignInIntoAccountAssync(SignInRequest request, CancellationToken cancellationToken)
    {
        var account = await _userManager.FindByEmailAsync(request.Email);
        if (account == null)
        {
            throw new InvalidOperationException($"Account with email {request.Email} does not exist.");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(account, request.Password);
        if (!isPasswordValid)
        {
            throw new InvalidOperationException($"Password for {request.Email} does not match.");
        }

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
    public Task<AccountInfoDto> GetUserInfoAssync(CancellationToken cancellationToken)
    {
        // Get logged in user. 
        // Get user info.
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task SignOutFromAccoutnAssync(CancellationToken cancellationToken)
    {
        // Logout.
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task DeleteAccountAssync(CancellationToken cancellationToken)
    {
        // Logout.
        // Remove UserInfo record.
        // Remove Accoun record.
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
}