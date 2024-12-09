namespace Laundry.Domain.Entities;

public class Coupon : BaseEntity
{
    public string Code { get; set; }
    public float Percentage { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int UsedCount { get; set; }
}