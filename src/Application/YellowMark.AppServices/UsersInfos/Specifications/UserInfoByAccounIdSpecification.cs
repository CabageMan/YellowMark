using System.Linq.Expressions;
using YellowMark.AppServices.Specifications;
using YellowMark.Domain.UsersInfos.Entity;

namespace YellowMark.AppServices.UsersInfos.Specifications;

/// <summary>
/// Get User by account id implementation of the Specification.
/// </summary>
public class UserInfoByAccounIdSpecification : Specification<UserInfo>
{
    private readonly Guid _accountId;

    /// <summary>
    /// Constructor of specification.
    /// </summary>
    /// <param name="accountId">Target userinfo param.</param>
    public UserInfoByAccounIdSpecification(Guid accountId)
    {
        _accountId = accountId;
    }

    /// <summary>
    /// Get user by account id implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<UserInfo, bool>> ToExpression()
    {
        return user => user.AccountId == _accountId;
    }
}
