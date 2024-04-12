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
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Subcategory.Category));

        CreateMap<CreateAdRequest, Ad>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.OwnerId))
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Subcategory, opt => opt.Ignore())
            .ForMember(dest => dest.Currency, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore());
    }
}
