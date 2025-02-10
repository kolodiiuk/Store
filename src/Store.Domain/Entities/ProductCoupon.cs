using System.Text.Json.Serialization;

namespace Store.Domain.Entities;

public class ProductCoupon : BaseEntity
{
    public int ProductId { get; set; }
    
    public int CouponId { get; set; }

    [JsonIgnore]
    public Coupon Coupon { get; set; }
    
    [JsonIgnore]
    public Product Product { get; set; }
}

