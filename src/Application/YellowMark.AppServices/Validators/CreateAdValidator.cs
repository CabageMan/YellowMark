using FluentValidation;
using YellowMark.Contracts.Ads;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="CreateAdRequest"/>
/// </summary>
public class CreateAdValidator : AbstractValidator<CreateAdRequest>
{
    /// <summary>
    /// Constrauctor for CreateAdValidator. 
    /// </summary>
    public CreateAdValidator()
    {
        RuleFor(ad => ad.Title)
            .NotNull()
            .NotEmpty()
            .Length(0, 255);

        RuleFor(ad => ad.Description)
            .NotNull()
            .NotEmpty()
            .MaximumLength(2500);

        RuleFor(ad => ad.Price)
            .Must(NotNegative)
            .WithMessage("Price should be positive");

        // TODO: Check in case not null.
        // RuleFor(ad => ad.CurrencyId)
        //     .SetValidator(new GuidValidator());

        RuleFor(ad => ad.OwnerId)
            .SetValidator(new GuidValidator());

        RuleFor(ad => ad.SubcategoryId)
            .SetValidator(new GuidValidator());
    }

    private static bool NotNegative(decimal? value)
    {
        return value == null || value >= 0;
    }
}
