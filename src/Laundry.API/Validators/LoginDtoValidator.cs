using FluentValidation;
using Laundry.API.Dto;

namespace Laundry.API.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("A valid email is required.");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}