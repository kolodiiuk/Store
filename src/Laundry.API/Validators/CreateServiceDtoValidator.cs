using FluentValidation;
using Laundry.API.Dto;

namespace Laundry.API.Validators;

public class CreateServiceDtoValidator : AbstractValidator<CreateServiceDto>
{
    public CreateServiceDtoValidator()
    {
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
