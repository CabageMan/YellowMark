using System.Text.RegularExpressions;
using FluentValidation;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Set of custom reusable validations rules.
/// </summary>
public static class ValidatiorExtensions
{
    /// <summary>
    /// Check if user is over certain age.
    /// </summary>
    /// <typeparam name="T">Type of the root object.</typeparam>
    /// <param name="ruleBuilder">Rule builder <see cref="IRuleBuilder"/> for <see cref="DateOnly"/> property </param>
    /// <param name="minAge">Minimum Age for the user.</param>
    /// <returns><see cref="IRuleBuilderOptions"/></returns>
    public static IRuleBuilderOptions<T, DateOnly> UserMustBeOverAge<T>(
        this IRuleBuilder<T, DateOnly> ruleBuilder,
        int minAge)
    {
        return ruleBuilder
            .NotEmpty()
            .NotNull()
            .WithMessage("{PropertyName} should not be empty.")
            .Must((rootObject, date, context) =>
            {
                context.MessageFormatter.AppendArgument("MinAge", minAge);
                return date <= DateOnly.FromDateTime(DateTime.Now.AddYears(-minAge));
            })
            .WithMessage("You must be at least {MinAge} years old.");
    }

    /// <summary>
    /// Check if string property is not empty and has certain length.
    /// </summary>
    /// <typeparam name="T">Type of the root object.</typeparam>
    /// <param name="ruleBuilder">Rule builder <see cref="IRuleBuilder"/> for <see cref="string"/> property. </param>
    /// <param name="minLength">Minimal property value length.</param>
    /// <param name="maxLength">Maximal property value length.</param>
    /// <returns><see cref="IRuleBuilderOptions"/></returns>
    public static IRuleBuilderOptions<T, string> StringCorrectLength<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int minLength,
        int maxLength)
    {
        return ruleBuilder
            .NotNull()
            .NotEmpty()
            .WithMessage("{PropertyName} should not be empty.")
            .Must((rootObject, property, context) =>
            {
                context.MessageFormatter.AppendArgument("MinLength", minLength);
                return property == null || property.Length >= minLength;
            })
            .WithMessage("{PropertyName} must be at least {MinLength} characters.")
            .Must((rootObject, property, context) =>
            {
                context.MessageFormatter.AppendArgument("MaxLength", maxLength);
                return property == null || property.Length <= maxLength;
            })
            .WithMessage("{PropertyName} must not exceed {MaxLength} characters.");
    }

    /// <summary>
    /// Check if Username format is correct.
    /// </summary>
    /// <typeparam name="T">Type of the root object.</typeparam>
    /// <param name="ruleBuilder">Rule builder <see cref="IRuleBuilder"/> for <see cref="string"/> property. </param>
    /// <param name="minLength">Minimal username length. Default is 1 character.</param>
    /// <param name="maxLength">Maximal username length. Default is 50 characters.</param>
    /// <returns><see cref="IRuleBuilderOptions"/></returns>
    public static IRuleBuilderOptions<T, string> UserNameCorrectFormat<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int minLength = 1,
        int maxLength = 50)
    {
        return ruleBuilder
            .StringCorrectLength(minLength, maxLength)
            .Matches(@"^[\p{L}.-]+$")
            .WithMessage("{PropertyName} should be in valid format.");
    }

    /// <summary>
    /// Check if phone number is in correct format: 1(234)567-8901.
    /// </summary>
    /// <typeparam name="T">Type of the root object.</typeparam>
    /// <param name="ruleBuilder">Rule builder <see cref="IRuleBuilder"/> for <see cref="string"/> property </param>
    /// <param name="minLength">Minimal phone number length. Default is 11 characters.</param>
    /// <param name="maxLength">Maximal phone number length. Default is 15 characters.</param>
    /// <returns><see cref="IRuleBuilderOptions"/></returns>
    public static IRuleBuilderOptions<T, string> PhoneNumberCorrectFormat<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int minLength = 11,
        int maxLength = 15)
    {
        return ruleBuilder
            .StringCorrectLength(minLength, maxLength)
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
            .WithMessage("PhoneNumber not valid");
    }

    /// <summary>
    /// Check if password format is correct.
    /// </summary>
    /// <typeparam name="T">Type of the root object.</typeparam>
    /// <param name="ruleBuilder">Rule builder <see cref="IRuleBuilder"/> for <see cref="string"/> property. </param>
    /// <param name="minLength">Minimal password length. Default is 8 characters.</param>
    /// <returns><see cref="IRuleBuilderOptions"/></returns>
    public static IRuleBuilderOptions<T, string> PasswordCorrectFormat<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int minLength = 8)
    {
        return ruleBuilder
            .NotNull()
            .NotEmpty()
            .WithMessage("{PropertyName} should not be empty.")
            .Must((rootObject, password, context) =>
            {
                context.MessageFormatter.AppendArgument("MinLength", minLength);
                return password == null || password.Length >= minLength;
            })
            .WithMessage("{PropertyName} must be at least {MinLength} characters.")
            .Matches(@"[A-Z]")
            .WithMessage("{PropertyName} must contain at least one uppercase letter.")
            .Matches(@"[a-z]")
            .WithMessage("{PropertyName} must contain at least one lowercase letter.")
            .Matches(@"[0-9]")
            .WithMessage("{PropertyName} must contain at least one number.")
            .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]")
            .WithMessage("{PropertyName} must contain one or more special characters.")
            .Matches("^[^£# “”]*$")
            .WithMessage("{PropertyName} must not contain the following characters £ # “” or spaces.");
    }

    /// <summary>
    /// Check if Email format is correct.
    /// </summary>
    /// <typeparam name="T">Type of the root object.</typeparam>
    /// <param name="ruleBuilder">Rule builder <see cref="IRuleBuilder"/> for <see cref="string"/> property. </param>
    /// <returns><see cref="IRuleBuilderOptions"/></returns>
    public static IRuleBuilderOptions<T, string> EmailCorrectFormat<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        // TODO: Check if '.EmailAddress' contains null and empty validations.
        return ruleBuilder
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .WithMessage("A valid email address is required.");
    }

    /// <summary>
    /// Check if guid is not empty.
    /// </summary>
    /// <typeparam name="T">Type of the root object.</typeparam>
    /// <param name="ruleBuilder">Rule builder <see cref="IRuleBuilder"/> for <see cref="Guid"/> property. </param>
    /// <returns><see cref="IRuleBuilderOptions"/></returns>
    public static IRuleBuilderOptions<T, Guid> NonEmptyGuid<T>(
        this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder
            .NotNull()
            .NotEmpty()
            .WithMessage("Id must not be empty.");
    }
}