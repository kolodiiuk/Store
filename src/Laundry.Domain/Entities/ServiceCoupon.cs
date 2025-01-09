namespace Laundry.Domain.Entities;

public class ServiceCoupon : BaseEntity
{
    public int ServiceId { get; set; }
    public int CouponId { get; set; }

    public Coupon Coupon { get; set; }
    public Service Service { get; set; }
}

