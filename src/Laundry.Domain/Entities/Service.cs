using Laundry.Domain.Enums;

namespace Laundry.Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; set; }
    public ServiceCategory Category { get; set; }
    public string Description { get; set; }
    public decimal PricePerUnit { get; set; }
    public UnitType UnitType { get; set; }
    public bool IsAvailable { get; set; }
    public ICollection<ServiceCoupon> ServiceCoupons { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
