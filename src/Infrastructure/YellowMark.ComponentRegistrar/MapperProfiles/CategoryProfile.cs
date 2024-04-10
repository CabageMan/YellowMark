using AutoMapper;
using YellowMark.Contracts.Categories;
using YellowMark.Domain.Categories.Entity;

namespace YellowMark.ComponentRegistrar;

/// <summary>
/// Category mapping profile.
/// </summary>
public class CategoryProfile : Profile
{
    /// <summary>
    /// Constructor for category mapping profile.
    /// </summary>
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();

        CreateMap<CreateCategoryRequest, Category>()
            .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
            .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow))
            .ForMember(s => s.UpdatedAt, map => map.MapFrom(s => DateTime.UtcNow))
            .ForMember(s => s.Subcategories, map => map.Ignore());
    }
}
