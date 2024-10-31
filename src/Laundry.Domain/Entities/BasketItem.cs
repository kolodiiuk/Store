namespace Laundry.Domain.Entities;

public class BasketItem : BaseEntity
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    public int BasketId { get; set; }
    public Basket Basket { get; set; }
}
