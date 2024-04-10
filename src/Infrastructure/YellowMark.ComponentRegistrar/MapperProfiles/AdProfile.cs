using AutoMapper;
using YellowMark.Contracts.Ads;
using YellowMark.Domain.Ads.Entity;

namespace YellowMark.ComponentRegistrar;

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
        // CreateMap<Ad, AdDto>()
        //     .ForMember(s => s.Owner, map => map.MapFrom(s =>
        //         s.User
        //     ));
    }
}
