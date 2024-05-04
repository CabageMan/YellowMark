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
            .ForMember(dest => dest.AccountId, opt => opt.Ignore())
            .ForMember(dest => dest.ShowPhone, opt => opt.Ignore());

        CreateMap<UserInfoDto, AccountInfoDto>()
            .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
            .ForMember(dest => dest.Phone, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore());

        CreateMap<UpdateAccountRequest, UpdateUserInfoRequest>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.AccountId, opt => opt.Ignore());
    }
}
