using FluentValidation;
using YellowMark.AppServices.Validators;
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
        RuleFor(s => s.PageNumber).NotNegative();
        RuleFor(s => s.BatchSize).NotNegative();
    }
}