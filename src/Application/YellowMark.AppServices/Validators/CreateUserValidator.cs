using System.Text.RegularExpressions;
using FluentValidation;
using YellowMark.Contracts;

namespace YellowMark.AppServices.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(user => user.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(user => user.MiddleName)
            .MaximumLength(100);

        RuleFor(user => user.LastName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);

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




        var eighteenYearsAgo = DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
        RuleFor(user => user.BirthDate)
            .NotEmpty()
            .NotNull()
            .LessThanOrEqualTo(eighteenYearsAgo)
            .WithMessage("You must be at least 18 years old.");
    }
}
