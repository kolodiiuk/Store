namespace Laundry.Domain.Entities;

public class ServiceCoupon
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public int CouponId { get; set; }

    public Coupon Coupon { get; set; }
    public Service Service { get; set; }
}

