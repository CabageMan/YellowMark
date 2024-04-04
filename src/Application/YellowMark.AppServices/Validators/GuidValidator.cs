using FluentValidation;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="Guid"/>.
/// </summary>
public class GuidValidator : AbstractValidator<Guid>
{
    
    /// <summary>
    /// Guid validator.
    /// </summary>
    public GuidValidator()
    {
        RuleFor(guid => guid)
            .NotEmpty()
            .WithMessage("Id must not be empty.");
    }
}
