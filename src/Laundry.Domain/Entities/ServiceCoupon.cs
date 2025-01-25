using System.Text.Json.Serialization;

namespace Laundry.Domain.Entities;

public class ServiceCoupon : BaseEntity
{
    public int ServiceId { get; set; }
    public int CouponId { get; set; }

    [JsonIgnore]
    public Coupon Coupon { get; set; }
    [JsonIgnore]
    public Service Service { get; set; }
}

