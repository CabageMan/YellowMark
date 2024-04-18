namespace YellowMark.AppServices.Accounts.Services;

public class AccountService : IAccountService
{
    private readonly IConfiguration _configuration;

    public AccountService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private string GenerateJwtToken()
    {

//  Salt password.
        var token = new JwtSecurityToken(); 
        return "";
    }
}
