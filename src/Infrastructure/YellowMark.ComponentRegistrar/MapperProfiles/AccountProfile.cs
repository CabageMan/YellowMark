using AutoMapper;
using YellowMark.Contracts.Account;
using YellowMark.Contracts.UsersInfos;

namespace YellowMark.ComponentRegistrar.MapperProfiles;

/// <summary>
/// Account mapping profile.
/// </summary>
public class AccountProfile : Profile
{
    /// <summary>
    /// Constructor for account mapping profile.
    /// </summary>
    public AccountProfile()
    {
        CreateMap<CreateAccountRequest, CreateUserInfoRequest>()
            .ForMember(dest => dest.AccountId, opt => opt.Ignore());
    }
}
