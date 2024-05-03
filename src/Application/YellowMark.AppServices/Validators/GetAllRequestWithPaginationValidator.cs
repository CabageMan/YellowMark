using FluentValidation;
using YellowMark.Contracts.Pagination;

namespace YellowMark.AppServices.Validators;

/// <summary>
/// Validator for <see cref="GetAllRequestWithPaginationValidator"/>
/// </summary>
public class GetAllRequestWithPaginationValidator : AbstractValidator<GetAllRequestWithPagination>
{
    /// <summary>
    /// Constrauctor for GetAllRequestWithPaginationValidator. 
    /// </summary>
    public GetAllRequestWithPaginationValidator()
    {
        RuleFor(s => s.PageNumber)
            .NotNull()
            .NotEmpty()
            .NotNegative();

        RuleFor(s => s.BatchSize)
            .NotNull()
            .NotEmpty()
            .NotNegative();
    }
}