using FluentValidation;
using Laundry.API.Dto;

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid order status.");
        RuleFor(x => x.Subtotal)
            .GreaterThan(0)
            .WithMessage("Subtotal must be greater than zero.");
        RuleFor(x => x.PaymentMethod)
            .IsInEnum()
            .WithMessage("Invalid payment method.");
        RuleFor(x => x.PaymentStatus)
            .IsInEnum()
            .WithMessage("Invalid payment status.");
        RuleFor(x => x.DeliveryFee)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Delivery fee must be greater than or equal to zero.");
        RuleFor(x => x.CollectedDate)
            .LessThanOrEqualTo(x => x.DeliveredDate)
            .WithMessage("Collected date must be before or equal to delivered date.");
        RuleFor(x => x.UserId)
            .GreaterThan(-1)
            .WithMessage("UserId must be greater than -1.");
    }
}