using System.Text.RegularExpressions;
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
        RuleFor(acc => acc.FirstName)
            .NotNull()
            .NotEmpty()
            .Length(1, 50)
            .Matches(@"^[\p{L}]+$");

        RuleFor(acc => acc.MiddleName)
            .Length(1, 50)
            .Matches(@"^[\p{L}.-]+$");

        RuleFor(acc => acc.LastName)
            .NotNull()
            .NotEmpty()
            .Length(1, 50)
            .Matches(@"^[\p{L}-]+$");

        RuleFor(acc => acc.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .WithMessage("A valid email address is required.");

        int minPhoneLength = 11;
        int maxPhoneLength = 15;
        // TODO: Update phone regex if it needed. Check in w.
        // Phone format: 1(234)567-8901
        RuleFor(acc => acc.Phone)
            .NotEmpty()
            .NotNull()
            .MinimumLength(minPhoneLength)
            .WithMessage($"The phone number must be at least {minPhoneLength} characters.")
            .MaximumLength(maxPhoneLength)
            .WithMessage($"The phonae number must not exceed {maxPhoneLength} characters.")
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
            .WithMessage("PhoneNumber not valid");

        RuleFor(acc => acc.BirthDate)
            .NotEmpty()
            .NotNull()
            .Must(BeOver18)
            .WithMessage("You must be at least 18 years old.");
    }

    // TODO: Create separate validator BeOver18 and user it.
    private static bool BeOver18(DateOnly date)
    {
        return date <= DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
    }
}
