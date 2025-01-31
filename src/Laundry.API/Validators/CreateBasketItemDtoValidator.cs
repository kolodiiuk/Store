using FluentValidation;
using Laundry.API.Dto;

namespace Laundry.API.Validators;

public class CreateBasketItemDtoValidator : AbstractValidator<CreateBasketItemDto>
{
    public CreateBasketItemDtoValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
        RuleFor(x => x.ServiceId)
            .GreaterThan(-1)
            .WithMessage("ServiceId must be greater than -1.");
        RuleFor(x => x.UserId)
            .GreaterThan(-1)
            .WithMessage("UserId must be greater than -1.");
    }
}