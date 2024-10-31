using Laundry.Domain.Enums;

namespace Laundry.Domain.Entities;

public class Order : BaseEntity
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
    public decimal SubTotal { get; set; }
    public string Description { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public bool HasCoupon { get; set; }
    public decimal Discount { get; set; }
    public decimal DeliveryFee { get; set; }
    public string PaymentIntentId { get; set; }
    public string ClientSecret { get; set; }
    public int CouponId { get; set; }
    public DateTime CollectedDate { get; set; }
    public DateTime DeliveredDate { get; set; }
    public Guid UserId { get; set; }
    public List<OrderItem> Items { get; set; }
    public Coupon Coupon { get; set; }
    public User User { get; set; }
}