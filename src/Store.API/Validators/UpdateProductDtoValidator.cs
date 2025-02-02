using FluentValidation;
using Store.API.Dto;

namespace Store.API.Validators;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
        RuleFor(x => x.Category)
            .IsInEnum()
            .WithMessage("Invalid category.");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.");
        RuleFor(x => x.PricePerUnit)
            .GreaterThan(0)
            .WithMessage("Price per unit must be greater than zero.");
        RuleFor(x => x.UnitType)
            .IsInEnum()
            .WithMessage("Invalid unit type.");
        RuleFor(x => x.IsAvailable)
            .NotNull()
            .WithMessage("Availability status is required.");
    }
}
