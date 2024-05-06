using FluentValidation;
using YellowMark.Contracts.Categories;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="UpdateCategoryRequest"/>
/// </summary>
public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
{
    /// <summary>
    /// Constrauctor for UpdateCategoryValidator. 
    /// </summary>
    public UpdateCategoryValidator()
    {
        RuleFor(s => s.Id).NonEmptyGuid();
        RuleFor(s => s.Name).StringCorrectLength(0, 255);
    }
}