using AutoMapper;
using YellowMark.Contracts.Subcategories;
using YellowMark.Domain.Subcategories.Entity;

namespace YellowMark.ComponentRegistrar.MapperProfiles;

/// <summary>
/// Subcategory mapping profile.
/// </summary>
public class SubcategoryProfile : Profile
{
    /// <summary>
    /// Constructor for subcategory mapping profile.
    /// </summary>
    public SubcategoryProfile()
    {
        CreateMap<Subcategory, SubcategoryDto>();

        CreateMap<CreateSubcategoryRequest, Subcategory>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Ads, opt => opt.Ignore());
    }
}
