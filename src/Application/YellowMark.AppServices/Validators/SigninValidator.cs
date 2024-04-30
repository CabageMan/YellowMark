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
        RuleFor(s => s.Email).EmailCorrectFormat();
        RuleFor(s => s.Password).PasswordCorrectFormat();
    }
}
