using AutoMapper;
using YellowMark.Contracts.UsersInfos;
using YellowMark.Domain.UsersInfos.Entity;

namespace YellowMark.ComponentRegistrar.MapperProfiles;

/// <summary>
/// User info mapping profile.
/// </summary>
public class UserProfile : Profile
{
    /// <summary>
    /// Constructor for user mapping profile.
    /// </summary>
    public UserProfile()
    {
        CreateMap<UserInfo, UserInfoDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => 
                $"{src.LastName} {src.MiddleName} {src.FirstName}"
            ));

        CreateMap<CreateUserInfoRequest, UserInfo>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId)) 
            .ForMember(dest => dest.Ads, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore());
    }
}
