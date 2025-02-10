using System.Text.Json.Serialization;
using Store.Domain.Enums;

namespace Store.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    
    public ProductCategory Category { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public UnitType UnitType { get; set; }
    
    public bool IsAvailable { get; set; }
    
    [JsonIgnore]
    public ICollection<ProductCoupon> ProductCoupons { get; set; } = new List<ProductCoupon>();
    
    [JsonIgnore]
    public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    
    [JsonIgnore]
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
