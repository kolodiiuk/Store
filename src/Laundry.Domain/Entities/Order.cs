using Laundry.Domain.Enums;

namespace Laundry.Domain.Entities;

public class Order : BaseEntity
{
    public OrderStatus Status { get; set; }
    public decimal Subtotal { get; set; }
    public string Description { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public bool HasCoupon { get; set; }
    public decimal? Discount { get; set; }
    public decimal DeliveryFee { get; set; }
    public string? PaymentIntentId { get; set; }
    public int? CouponId { get; set; }
    public DateTime CollectedDate { get; set; }
    public DateTime DeliveredDate { get; set; }
    public int UserId { get; set; }
    public int AddressId { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public Coupon? Coupon { get; set; }
    public User User { get; set; }
    public Address Address { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}