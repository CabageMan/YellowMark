using AutoMapper;
using YellowMark.Contracts.Currnecies;
using YellowMark.Domain.Currencies.Entity;

namespace YellowMark.ComponentRegistrar.MapperProfiles;

/// <summary>
/// Currency mapping profile.
/// </summary>
public class CurrencyProfile : Profile
{
    /// <summary>
    /// Constructor for currency mapping profile.
    /// </summary>
    public CurrencyProfile()
    {
        CreateMap<Currency, CurrencyDto>();

        CreateMap<CreateCurrencyRequest, Currency>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Ads, opt => opt.Ignore());
    }
}
