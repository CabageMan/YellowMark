using FluentValidation;
using YellowMark.Contracts.Comments;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="UpdateCommentRequest"/>
/// </summary>
public class UpdateCommentValidator : AbstractValidator<UpdateCommentRequest>
{
    /// <summary>
    /// Constrauctor for UpdateCommentValidator. 
    /// </summary>
    public UpdateCommentValidator()
    {
        RuleFor(c => c.Text).StringCorrectLength(0, 1024);
    }
}