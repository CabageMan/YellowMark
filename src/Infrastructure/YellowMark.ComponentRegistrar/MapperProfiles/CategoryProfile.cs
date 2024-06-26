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
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Subcategories, opt => opt.MapFrom(src => src.Subcategories));

        var creationDate = DateTime.UtcNow;
        CreateMap<CreateCategoryRequest, Category>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => creationDate))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => creationDate))
            .ForMember(dest => dest.ParentCategory, opt => opt.Ignore())
            .ForMember(dest => dest.Subcategories, opt => opt.Ignore())
            .ForMember(dest => dest.Ads, opt => opt.Ignore());

        CreateMap<UpdateCategoryRequest, Category>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.ParentCategoryId, opt => opt.Ignore())
            .ForMember(dest => dest.ParentCategory, opt => opt.Ignore())
            .ForMember(dest => dest.Subcategories, opt => opt.Ignore())
            .ForMember(dest => dest.Ads, opt => opt.Ignore());
    }
}
