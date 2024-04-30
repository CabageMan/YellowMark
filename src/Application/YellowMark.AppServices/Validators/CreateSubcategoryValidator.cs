using FluentValidation;
using YellowMark.Contracts.Subcategories;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="CreateSubcategoryRequest"/>
/// </summary>
public class CreateSubcategoryValidator : AbstractValidator<CreateSubcategoryRequest>
{
    /// <summary>
    /// Constrauctor for CreateSubcategoryValidator. 
    /// </summary>
    public CreateSubcategoryValidator()
    {
        RuleFor(s => s.Name).StringCorrectLength(0, 255);
        RuleFor(s => s.CategoryId).NonEmptyGuid();
    }
}
