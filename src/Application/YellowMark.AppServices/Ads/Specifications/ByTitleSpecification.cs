using System.Linq.Expressions;
using YellowMark.AppServices.Specifications;
using YellowMark.Domain.Ads.Entity;

namespace YellowMark.AppServices.Ads.Specifications;

/// <summary>
/// Get Ad by title implementation of the Specification.
/// </summary>
public class ByTitleSpecification : Specification<Ad>
{
    private readonly string _title;

    /// <summary>
    /// Constructor of specification.
    /// </summary>
    /// <param name="title">Target ad param.</param>
    public ByTitleSpecification(string title)
    {
        _title = title;
    }

    /// <summary>
    /// Get user by name implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<Ad, bool>> ToExpression()
    {
        return ad => ad.Title == _title;
    }
}
