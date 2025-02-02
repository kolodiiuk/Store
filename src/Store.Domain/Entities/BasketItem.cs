using System.Text.Json.Serialization;

namespace Store.Domain.Entities;

public class BasketItem : BaseEntity
{
    public decimal Total { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    [JsonIgnore]
    public Product Product { get; set; }
    [JsonIgnore]
    public User User { get; set; }
}
