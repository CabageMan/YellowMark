using FluentValidation;
using YellowMark.Contracts.Account;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="UpdateAccountRequest"/> user model.
/// </summary>
public class UpdateAccountValidator : AbstractValidator<UpdateAccountRequest>
{
    /// <summary>
    /// Update account validator.
    /// </summary>
    public UpdateAccountValidator()
    {
        RuleFor(acc => acc.FirstName).UserNameCorrectFormat();
        RuleFor(acc => acc.MiddleName).UserNameCorrectFormat();
        RuleFor(acc => acc.LastName).UserNameCorrectFormat();
        RuleFor(acc => acc.Email).EmailCorrectFormat();
        RuleFor(acc => acc.Phone).PhoneNumberCorrectFormat();
        RuleFor(acc => acc.BirthDate).UserMustBeOverAge(18);
    }
}
