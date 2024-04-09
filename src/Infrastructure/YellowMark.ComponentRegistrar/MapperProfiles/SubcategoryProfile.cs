using AutoMapper;
using YellowMark.Contracts.Subcategories;
using YellowMark.Domain.Subcategories.Entity;

namespace YellowMark.ComponentRegistrar;

/// <summary>
/// User mapping profile.
/// </summary>
public class SubcategoryProfile : Profile
{
    /// <summary>
    /// Constructor for user mapping profile.
    /// </summary>
    public SubcategoryProfile()
    {
        CreateMap<Subcategory, SubcategoryDto>();

        CreateMap<CreateSubcategoryRequest, Subcategory>()
            .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
            .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
            .ForMember(s => s.UpdatedAt, map => map.MapFrom(s => DateTime.UtcNow))
            .ForMember(s => s.Category, map => map.Ignore())
            .ForMember(s => s.Ads, map => map.Ignore());
    }
}
