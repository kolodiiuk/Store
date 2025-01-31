using System.Text.Json.Serialization;

namespace Laundry.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public decimal CurrentUnitPrice { get; set; }
    public int? ServiceId { get; set; }
    public int OrderId { get; set; }
    
    [JsonIgnore]
    public Service? Service { get; set; }
    [JsonIgnore]
    public Order Order { get; set; }
}
