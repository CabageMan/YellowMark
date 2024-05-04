using FluentValidation;
using YellowMark.Contracts.Comments;

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
        RuleFor(c => c.Text).StringCorrectLength(0, 1024);
        RuleFor(c => c.AdId).NonEmptyGuid();
    }
}
