﻿using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using YellowMark.AppServices.Specifications;
using YellowMark.Domain.Ads.Entity;

namespace YellowMark.AppServices.Ads.Specifications;

/// <summary>
/// Get Ad by title implementation of the Specification.
/// </summary>
public class AdByTitleSpecification : Specification<Ad>
{
    private readonly string _title;

    /// <summary>
    /// Constructor of specification.
    /// </summary>
    /// <param name="title">Target ad param.</param>
    public AdByTitleSpecification(string title)
    {
        _title = title;
    }

    /// <summary>
    /// Get Ad by title implementation of ToExpression method.
    /// </summary>
    /// <returns>Expression</returns>
    public override Expression<Func<Ad, bool>> ToExpression()
    {
        return ad => ad.Title.ToLower().Contains(_title.ToLower());
    }
}