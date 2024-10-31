namespace Laundry.Domain.Entities;

public class Coupon : BaseEntity
{
    public int Id { get; set; }
    public string Code { get; set; }
    public float Percentage { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}