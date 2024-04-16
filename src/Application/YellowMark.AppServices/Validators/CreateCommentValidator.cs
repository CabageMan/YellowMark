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
        RuleFor(ad => ad.Text)
            .NotNull()
            .NotEmpty()
            .Length(0, 1024);

        RuleFor(ad => ad.UserId)
            .SetValidator(new GuidValidator());

        RuleFor(ad => ad.AdId)
            .SetValidator(new GuidValidator());
    }
}
