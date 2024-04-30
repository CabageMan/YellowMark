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
        RuleFor(s => s.AlphabeticCode).StringCorrectLength(0, 3);

        RuleFor(s => s.NumericCode)
            .NotNull()
            .NotEmpty()
            .WithMessage("Non empty numeric code required.")
            .Must(BeLess1000)
            .WithMessage("Numeric code should be less than 1000");
    }

    private static bool BeLess1000(int code)
    {
        return code < 1000;
    }
}

