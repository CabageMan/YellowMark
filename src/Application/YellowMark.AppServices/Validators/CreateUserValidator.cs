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
            .MaximumLength(30);

        RuleFor(user => user.MiddleName)
            .MaximumLength(30);

        RuleFor(user => user.LastName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(user => user.Email)
            .EmailAddress();

        // TODO: Create validation for phone number.
        RuleFor(user => user.Phone)
            .NotEmpty()
            .NotNull();

        var eighteenYearsAgo = DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
        RuleFor(user => user.BirthDate)
        .NotEmpty()
        .NotNull()
        .LessThanOrEqualTo(eighteenYearsAgo)
        .WithMessage("You must be at least 18 years old.");
    }
}
