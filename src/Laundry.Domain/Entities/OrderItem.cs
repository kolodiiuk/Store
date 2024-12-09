namespace Laundry.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public decimal CurrentUnitPrice { get; set; }
    public int ServiceId { get; set; }
    public int OrderId { get; set; }
    
    public Service Service { get; set; }
    public Order Order { get; set; }
}
