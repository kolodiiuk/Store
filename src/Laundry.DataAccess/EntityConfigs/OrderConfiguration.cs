using Laundry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laundry.DataAccess.EntityConfigs;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        
        builder.HasKey(o => o.Id);

        builder.HasOne(o => o.Feedback)
            .WithOne(f => f.Order);
        
        builder.Property(o => o.Id)
            .HasColumnName("order_id");
        
        builder.Property(o => o.Status)
            .HasColumnName("status");
        
        builder.Property(o => o.Subtotal)
            .HasColumnName("subtotal")
            .IsRequired();
        
        builder.Property(o => o.Description)
            .HasColumnName("description")
            .IsRequired();
        
        builder.Property(o => o.PaymentMethod)
            .HasColumnName("payment_method")
            .IsRequired();
        
        builder.Property(o => o.PaymentStatus)
            .HasColumnName("payment_status")
            .IsRequired();
        
        builder.Property(o => o.HasCoupon)
            .HasColumnName("has_coupon");
        
        builder.Property(o => o.Discount)
            .HasColumnName("discount")
            .IsRequired();
        
        builder.Property(o => o.DeliveryFee)
            .HasColumnName("delivery_fee")
            .IsRequired();
        
        builder.Property(o => o.PaymentIntentId)
            .HasColumnName("payment_intent_id")
            .IsRequired();

        builder.Property(o => o.CouponId)
            .HasColumnName("coupon_id")
            .IsRequired();
        
        builder.Property(o => o.CollectedDate)
            .HasColumnName("collected_date")
            .IsRequired();
        
        builder.Property(o => o.DeliveredDate)
            .HasColumnName("delievered_date")
            .IsRequired();
        
        builder.Property(o => o.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        
        builder.Property(o => o.AddressId)
            .HasColumnName("address_id")
            .IsRequired();
        
        builder.Property(o => o.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}