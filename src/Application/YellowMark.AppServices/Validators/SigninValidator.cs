using FluentValidation;
using YellowMark.Contracts.Account;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="SigninValidator"/> user model.
/// </summary>
public class SigninValidator : AbstractValidator<SignInRequest>
{
    /// <summary>
    /// Signin info validator.
    /// </summary>
    public SigninValidator()
    {

        RuleFor(s => s.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .WithMessage("A valid email address is required.");

        RuleFor(s => s.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password must not be empty.")
            .Length(8, 20)
            .WithMessage("Password min length - 8, max - 20.");
    }
}
