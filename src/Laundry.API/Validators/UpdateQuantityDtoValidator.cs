using FluentValidation;
using Laundry.API.Dto;

namespace Laundry.API.Validators;

public class UpdateQuantityDtoValidator : AbstractValidator<UpdateQuantityDto>
{
    public UpdateQuantityDtoValidator()
    {
        RuleFor(x => x.BasketItemId)
            .GreaterThan(-1)
            .WithMessage("BasketItemId must be greater than -1.");
        RuleFor(x => x.NewValue)
            .GreaterThan(0)
            .WithMessage("NewValue must be greater than zero.");
    }
}