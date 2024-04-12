using System.Linq.Expressions;
using YellowMark.AppServices.Specifications;
using YellowMark.Domain.Users.Entity;

namespace YellowMark.AppServices.Users.Specifications;

/// <summary>
/// Get User older 18 years implementation of the Specification.
/// </summary>
public class UserMustBeOver18Specification : Specification<User>
{
    /// <summary>
    /// Get user older 18 years implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<User, bool>> ToExpression()
    {
        var dateNow = DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
        return user => user.BirthDate <= dateNow;
    }
}
