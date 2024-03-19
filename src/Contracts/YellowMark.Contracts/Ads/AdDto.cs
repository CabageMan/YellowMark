using YellowMark.Contracts.Categories;
using YellowMark.Contracts.Currnecies;
using YellowMark.Contracts.Subcategories;
using YellowMark.Contracts.Users;

namespace YellowMark.Contracts.Ads;

/// <summary>
/// Ad data transfer object. 
/// </summary>
public class AdDto
{
    /// <summary>
    /// Ad record identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Ad creation date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Ad update date.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    // TODO: Check, may be use part of user data, or other DTO.
    /// <summary>
    /// Ad owner <see cref="UserDto"/>
    /// </summary>
    public UserDto Owner { get; set; }

    /// <summary>
    /// Ad title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Ad description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Ad category <see cref="CategoryDto"/>.
    /// </summary>
    public CategoryDto Category { get; set; }

    /// <summary>
    /// Ad subcategory <see cref="SubcategoryDto"/>.
    /// </summary>
    public SubcategoryDto Subcategory { get; set; }

    /// <summary>
    /// Goods currency <see cref="CurrencyDto"/> specified in the ad. 
    /// </summary>
    public CurrencyDto Currency { get; set; }

    /// <summary>
    /// Goods price.
    /// </summary>
    public Double Price { get; set; }
}