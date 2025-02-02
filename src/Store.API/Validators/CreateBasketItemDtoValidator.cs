using FluentValidation;
using Store.API.Dto;

namespace Store.API.Validators;

public class CreateBasketItemDtoValidator : AbstractValidator<CreateBasketItemDto>
{
    public CreateBasketItemDtoValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
        RuleFor(x => x.ProductId)
            .GreaterThan(-1)
            .WithMessage("ProductId must be greater than -1.");
        RuleFor(x => x.UserId)
            .GreaterThan(-1)
            .WithMessage("UserId must be greater than -1.");
    }
}