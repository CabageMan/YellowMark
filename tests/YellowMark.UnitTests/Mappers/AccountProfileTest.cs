using AutoFixture;
using AutoMapper;
using YellowMark.ComponentRegistrar.MapperProfiles;
using YellowMark.Contracts;
using YellowMark.Contracts.Account;
using YellowMark.Contracts.UsersInfos;

namespace YellowMark.UnitTests.Mappers;

/// <summary>
/// Account mapper profile tests <see cref="AccountProfile"/>.
/// </summary>
public class AccountProfileTest : BaseUnitTest
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Init AccountProfileTest instance with calling constructor of base class.
    /// </summary>
    public AccountProfileTest() : base()
    {
        _mapper = new MapperConfiguration(cfg => 
            cfg.AddProfile(new AccountProfile())
        ).CreateMapper();
    }

    [Fact]
    public void ShouldCorrect_ValidateConfiguration()
    {
        var profile = new AccountProfile();

        var mapper = new MapperConfiguration(cfg => cfg.AddProfile(profile));

        mapper.AssertConfigurationIsValid();
    }

    [Fact]
    public void ShouldCorrect_Map_CreateAccountRequest_to_CreateUserInfoRequest()
    {
        var account = Fixture
            .Build<CreateAccountRequest>()
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();

        var userInfo = _mapper.Map<CreateUserInfoRequest>(account);

        Assert.NotNull(userInfo);
        Assert.Equal(account.FirstName, userInfo.FirstName);
        Assert.Equal(account.MiddleName, userInfo.MiddleName);
        Assert.Equal(account.LastName, userInfo.LastName);
        Assert.Equal(account.BirthDate, userInfo.BirthDate);
    }

    [Fact]
    public void ShouldCorrect_Map_UserInfoDto_to_AccountInfoDto()
    {
        var userInfo = Fixture
            .Build<UserInfoDto>()
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();

        var accountInfo = _mapper.Map<AccountInfoDto>(userInfo);

        Assert.NotNull(accountInfo);
        Assert.Equal(userInfo.Id, accountInfo.Id);
        Assert.Equal(userInfo.FirstName, accountInfo.FirstName);
        Assert.Equal(userInfo.MiddleName, accountInfo.MiddleName);
        Assert.Equal(userInfo.LastName, accountInfo.LastName);
        Assert.Equal(userInfo.FullName, accountInfo.FullName);
        Assert.Equal(userInfo.BirthDate, accountInfo.BirthDate);
    }

    [Fact]
    public void ShouldCorrect_Map_UpdateAccountRequest_to_CreateUserInfoRequest()
    {
        var account = Fixture
            .Build<UpdateAccountRequest>()
            .With(x => x.BirthDate, DateOnly.FromDateTime(DateTime.Parse("1973-03-19")))
            .Create();

        var userInfo = _mapper.Map<CreateUserInfoRequest>(account);

        Assert.NotNull(userInfo);
        Assert.Equal(account.FirstName, userInfo.FirstName);
        Assert.Equal(account.MiddleName, userInfo.MiddleName);
        Assert.Equal(account.LastName, userInfo.LastName);
        Assert.Equal(account.BirthDate, userInfo.BirthDate);
    }
}
