namespace Laundry.Domain.Entities;

public class Coupon : BaseEntity
{
    public string Code { get; set; }
    public double Percentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int UsedCount { get; set; }

    public ICollection<ServiceCoupon>? ServiceCoupons { get; set; }
    public ICollection<Order>? Orders { get; set; } 
}