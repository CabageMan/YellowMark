﻿using System.Linq.Expressions;
using YellowMark.AppServices.Specifications;
using YellowMark.Domain.UsersInfos.Entity;

namespace YellowMark.AppServices.UsersInfos.Specifications;

/// <summary>
/// Get User by name implementation of the Specification.
/// </summary>
public class UserInfoByNameSpecification : Specification<UserInfo>
{
    private readonly string _nameLowerCase;

    /// <summary>
    /// Constructor of specification.
    /// </summary>
    /// <param name="name">Target user param.</param>
    public UserInfoByNameSpecification(string name)
    {
        _nameLowerCase = name.ToLower();
    }

    /// <summary>
    /// Get user by name implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<UserInfo, bool>> ToExpression()
    {
        return user => user.FirstName.ToLower().Contains(_nameLowerCase) || 
            user.MiddleName.ToLower().Contains(_nameLowerCase) ||
            user.LastName.ToLower().Contains(_nameLowerCase);
    }
}
