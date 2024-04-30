using FluentValidation;
using YellowMark.Contracts;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="CreateCommentRequest"/>
/// </summary>
public class CreateCommentValidator : AbstractValidator<CreateCommentRequest>
{
    /// <summary>
    /// Constrauctor for CreateCommentValidator. 
    /// </summary>
    public CreateCommentValidator()
    {
        RuleFor(ad => ad.Text).StringCorrectLength(0, 1024);
        RuleFor(ad => ad.UserId).NonEmptyGuid();
        RuleFor(ad => ad.AdId).NonEmptyGuid();
    }
}
