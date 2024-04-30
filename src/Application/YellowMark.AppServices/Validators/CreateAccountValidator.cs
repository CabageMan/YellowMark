using FluentValidation;
using YellowMark.Contracts.Account;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="CreateAccountRequest"/> user model.
/// </summary>
public class CreateAccountValidator : AbstractValidator<CreateAccountRequest>
{
    /// <summary>
    /// Create account validator.
    /// </summary>
    public CreateAccountValidator()
    {
        RuleFor(acc => acc.FirstName).UserNameCorrectFormat();
        RuleFor(acc => acc.MiddleName).UserNameCorrectFormat();
        RuleFor(acc => acc.LastName).UserNameCorrectFormat();
        RuleFor(acc => acc.Email).EmailCorrectFormat();
        RuleFor(acc => acc.Phone).PhoneNumberCorrectFormat();
        RuleFor(acc => acc.BirthDate).UserMustBeOverAge(18);
        RuleFor(acc => acc.Password).PasswordCorrectFormat();
    }
}
