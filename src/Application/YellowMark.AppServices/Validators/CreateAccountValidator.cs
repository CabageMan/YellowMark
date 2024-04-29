﻿using System.Text.RegularExpressions;
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

        RuleFor(acc => acc.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(8)
            .Matches(@"[A-Z]")
            .WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]")
            .WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]")
            .WithMessage("Your password must contain at least one number.")
            .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]")
            .WithMessage("Your password must contain one or more special characters.")
            .Matches("^[^£# “”]*$")
            .WithMessage("Your password must not contain the following characters £ # “” or spaces.");
    }

    // TODO: Create separate validator BeOver18 and user it.
    private static bool BeOver18(DateOnly date)
    {
        return date <= DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
    }
}
