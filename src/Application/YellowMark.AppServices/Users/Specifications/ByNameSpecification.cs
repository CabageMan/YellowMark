using System.Linq.Expressions;
using YellowMark.AppServices.Specifications;
using YellowMark.Domain.Users.Entity;

namespace YellowMark.AppServices.Users.Specifications;

/// <summary>
/// Get User by name implementation of the Specification.
/// </summary>
public class ByNameSpecification : Specification<Domain.Users.Entity.User>
{
    private readonly string _name;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">Target user param.</param>
    public ByNameSpecification(string name)
    {
        _name = name;
    }

    /// <summary>
    /// Get user by name implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.FirstName == _name;
    }
}
