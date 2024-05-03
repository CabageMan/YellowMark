using FluentValidation;
using YellowMark.Contracts.Currnecies;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="CreateCurrencyRequest"/>
/// </summary>
public class CreateCurrencyValidator : AbstractValidator<CreateCurrencyRequest>
{
    /// <summary>
    /// Constrauctor for CreateCurrecyValidator. 
    /// </summary>
    public CreateCurrencyValidator()
    {
        RuleFor(s => s.AlphabeticCode)
            .StringCorrectLength(3, 3)
            .Matches(@"^[\p{L}]+$");

        RuleFor(s => s.NumericCode)
            .NotNull()
            .NotEmpty()
            .WithMessage("Non empty numeric code required.")
            .Must(BeGreaterZero)
            .WithMessage("Numeric code should be greater than 0")
            .Must(BeLess1000)
            .WithMessage("Numeric code should be less than 1000");
    }

    private static bool BeGreaterZero(int code)
    {
        return code > 0;
    }

    private static bool BeLess1000(int code)
    {
        return code < 1000;
    }
}

