using FluentValidation;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="Guid"/>.
/// </summary>
public class GuidValidator : AbstractValidator<Guid>
{
    /// <summary>
    /// Constructor fot guid validator.
    /// </summary>
    public GuidValidator()
    {
        RuleFor(guid => guid)
            .NotNull()
            .NotEmpty()
            .WithMessage("Id must not be empty.");
    }
}
