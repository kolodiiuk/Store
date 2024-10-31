namespace Laundry.Domain.Entities;

public class Basket : BaseEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Total { get; set; }
    public List<BasketItem> Items { get; set; }
    public User User { get; set; }
}
