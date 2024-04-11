using System.Text.RegularExpressions;
using FluentValidation;
using YellowMark.Contracts.Users;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="CreateUserRequest"/> user model.
/// </summary>
public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    /// <summary>
    /// CreateUser validator.
    /// </summary>
    public CreateUserValidator()
    {
        RuleFor(user => user.FirstName)
            .NotNull()
            .NotEmpty()
            .Length(0, 255);

        RuleFor(user => user.MiddleName)
            .Length(0, 255);

        RuleFor(user => user.LastName)
            .NotNull()
            .NotEmpty()
            .Length(0, 255);

        RuleFor(user => user.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .WithMessage("A valid email address is required.");

        int minPhoneLength = 11;
        int maxPhoneLength = 15;
        // TODO: Update phone regex if it needed. Check in w.
        // Refactor on tests creating.
        // Phone format: 1(234)567-8901
        RuleFor(user => user.Phone)
            .NotEmpty()
            .NotNull()
            .MinimumLength(minPhoneLength)
            .WithMessage($"The phone number must be at least {minPhoneLength} characters.")
            .MaximumLength(maxPhoneLength)
            .WithMessage($"The phonae number must not exceed {maxPhoneLength} characters.")
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
            .WithMessage("PhoneNumber not valid");

        RuleFor(user => user.BirthDate)
            .NotEmpty()
            .NotNull()
            .Must(BeOver18)
            .WithMessage("You must be at least 18 years old.");
    }

    private static bool BeOver18(DateOnly date)
    {
        return date <= DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
    }
}
