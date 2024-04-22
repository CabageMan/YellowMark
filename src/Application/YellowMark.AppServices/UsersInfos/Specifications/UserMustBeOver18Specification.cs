using System.Linq.Expressions;
using YellowMark.AppServices.Specifications;
using YellowMark.Domain.UsersInfos.Entity;

namespace YellowMark.AppServices.Users.Specifications;

/// <summary>
/// Get User older 18 years implementation of the Specification.
/// </summary>
public class UserMustBeOver18Specification : Specification<UserInfo>
{
    /// <summary>
    /// Get user older 18 years implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<UserInfo, bool>> ToExpression()
    {
        var dateNow = DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
        return user => user.BirthDate <= dateNow;
    }
}
