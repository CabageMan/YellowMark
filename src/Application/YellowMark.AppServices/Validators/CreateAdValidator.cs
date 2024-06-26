﻿using FluentValidation;
using YellowMark.Contracts.Ads;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="CreateAdRequest"/>
/// </summary>
public class CreateAdValidator : AbstractValidator<CreateAdRequest>
{
    /// <summary>
    /// Constrauctor for CreateAdValidator. 
    /// </summary>
    public CreateAdValidator()
    {
        RuleFor(ad => ad.Title).StringCorrectLength(0, 255);
        RuleFor(ad => ad.Description).StringCorrectLength(0, 2500);
        RuleFor(ad => ad.Price).NotNegative();
        RuleFor(ad => ad.CategoryId).NonEmptyGuid();
    }
}
