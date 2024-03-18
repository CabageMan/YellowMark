using System.Data;
using FluentValidation;
using YellowMark.Contracts;

namespace YellowMark.AppServices.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(user => user.FirstName).NotEmpty();
    }
}
