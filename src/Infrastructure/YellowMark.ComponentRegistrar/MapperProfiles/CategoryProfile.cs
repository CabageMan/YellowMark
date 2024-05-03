﻿using AutoMapper;
using YellowMark.Contracts.Categories;
using YellowMark.Domain.Categories.Entity;

namespace YellowMark.ComponentRegistrar.MapperProfiles;

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
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.ParentCategory, opt => opt.Ignore())
            .ForMember(dest => dest.Subcategories, opt => opt.Ignore())
            .ForMember(dest => dest.Ads, opt => opt.Ignore());
    }
}
