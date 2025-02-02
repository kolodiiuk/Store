using Store.Domain.Enums;

namespace Store.Domain.Utils;

public class CreateOrderDto
{
    public int UserId { get; set; }
    public int AddressId { get; set; }
    public string Description { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal DeliveryFee { get; set; }
    public bool HasCoupon { get; set; }
    public string Code { get; set; }
    public ICollection<CreateOrderItemDto> OrderItems { get; set; }
}

public class CreateOrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}