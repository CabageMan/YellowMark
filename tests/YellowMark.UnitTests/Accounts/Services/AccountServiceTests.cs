using AutoMapper;
using Moq;
using YellowMark.AppServices.Accounts.Services;

namespace YellowMark.UnitTests.Users.Services;

/// <summary>
/// <see cref="AccountService"/>
/// </summary>
public class AccountServiceTests : BaseUnitTest
{
    private readonly IAccountService _accountService;

    /// <summary>
    /// Init instance of AccountServiceTests with calling constructor of base class.
    /// </summary>
    public AccountServiceTests() : base()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        // TODO: Make mock of UserManager
    }
}
