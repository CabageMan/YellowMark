using FluentValidation;
using YellowMark.Contracts.Categories;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="CreateCategoryRequest"/>
/// </summary>
public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    /// <summary>
    /// Constrauctor for CreateCategoryValidator. 
    /// </summary>
    public CreateCategoryValidator()
    {
        RuleFor(s => s.Name).StringCorrectLength(0, 255);
    }
}
