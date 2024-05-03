using System.Linq.Expressions;
using YellowMark.AppServices.Specifications;
using YellowMark.Domain.Categories.Entity;

namespace YellowMark.AppServices.Categories.Specifications;

/// <summary>
/// Get Category by Name implementation of the Specification.
/// </summary>
public class CategoryByNameSpecification : Specification<Category>
{
    private readonly string _name;

    /// <summary>
    /// Constructor of specification.
    /// </summary>
    /// <param name="name">Target category param.</param>
    public CategoryByNameSpecification(string name)
    {
        _name = name;
    }

    /// <summary>
    /// Get Category by name implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<Category, bool>> ToExpression()
    {
        return category => category.Name == _name;
    }
}