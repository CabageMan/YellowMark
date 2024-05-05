using System.Linq.Expressions;
using Microsoft.IdentityModel.Tokens;
using YellowMark.AppServices.Specifications;
using YellowMark.Domain.Ads.Entity;

namespace YellowMark.AppServices.Ads.Specifications;

/// <summary>
/// Get Ad by category implementation of the Specification.
/// </summary>
public class AdsByCategorySpecification : Specification<Ad>
{
    private readonly Guid _categoryId;

    /// <summary>
    /// Constructor of specification.
    /// </summary>
    /// <param name="title">Target ad param.</param>
    public AdsByCategorySpecification(Guid categoryId)
    {
        _categoryId = categoryId;
    }

    /// <summary>
    /// Get Ad by category implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<Ad, bool>> ToExpression()
    {
        return ad => (ad.Category.Subcategories == null || ad.Category.Subcategories.Count == 0) && ad.CategoryId == _categoryId;
    }
}
