using System.Text.Json.Serialization;

namespace Store.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public decimal CurrentUnitPrice { get; set; }
    public int? ProductId { get; set; }
    public int OrderId { get; set; }
    
    [JsonIgnore]
    public Product Product { get; set; }
    [JsonIgnore]
    public Order Order { get; set; }
}
