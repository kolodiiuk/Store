namespace Laundry.Domain.Entities;

public class BasketItem : BaseEntity
{
    public decimal Total { get; set; }
    public int Quantity { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    public int UserId { get; set; }
    
    public User User { get; set; }
}
