using AutoMapper;
using YellowMark.Contracts.Ads;
using YellowMark.Domain.Ads.Entity;

namespace YellowMark.ComponentRegistrar.MapperProfiles;

/// <summary>
/// Ad mapping profile.
/// </summary>
public class AdProfile : Profile
{
    /// <summary>
    /// Constructor for ad mapping profile.
    /// </summary>
    public AdProfile()
    {
        CreateMap<Ad, AdDto>()
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.UserInfo))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

        CreateMap<CreateAdRequest, Ad>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UserInfoId, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.UserInfo, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Currency, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ForMember(dest => dest.Files, opt => opt.Ignore());
    }
}
