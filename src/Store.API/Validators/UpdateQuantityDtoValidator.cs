using FluentValidation;
using Store.API.Dto;

namespace Store.API.Validators;

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