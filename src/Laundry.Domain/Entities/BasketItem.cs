using System.Text.Json.Serialization;

namespace Laundry.Domain.Entities;

public class BasketItem : BaseEntity
{
    public decimal Total { get; set; }
    public int Quantity { get; set; }
    public int ServiceId { get; set; }
    public int UserId { get; set; }
    [JsonIgnore]
    public Service Service { get; set; }
    [JsonIgnore]
    public User User { get; set; }
}
