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
        
        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);
        
        builder.HasMany(o => o.Feedbacks)
            .WithOne(f => f.Order)
            .HasForeignKey(f => f.OrderId);

        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(o => o.Address)
            .WithMany(a => a.Orders)
            .HasForeignKey(o => o.AddressId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(o => o.Coupon)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CouponId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Property(o => o.Id)
            .HasColumnName("order_id");
        
        builder.Property(o => o.Status)
            .HasColumnName("status")
            .IsRequired();
        
        builder.Property(o => o.Subtotal)
            .HasColumnName("subtotal")
            .IsRequired();

        builder.Property(o => o.Description)
            .HasColumnName("description");
        
        builder.Property(o => o.PaymentMethod)
            .HasColumnName("payment_method")
            .IsRequired();
        
        builder.Property(o => o.PaymentStatus)
            .HasColumnName("payment_status")
            .IsRequired();
        
        builder.Property(o => o.HasCoupon)
            .HasColumnName("has_coupon")
            .IsRequired();
        
        builder.Property(o => o.Discount)
            .HasColumnName("discount")
            .IsRequired();
        
        builder.Property(o => o.DeliveryFee)
            .HasColumnName("delivery_fee")
            .IsRequired();
        
        builder.Property(o => o.PaymentIntentId)
            .HasColumnName("payment_intent_id")
            .IsRequired()
            .HasColumnType("CHAR(27)");

        builder.Property(o => o.CouponId)
            .HasColumnName("coupon_id");
        
        builder.Property(o => o.CollectedDate)
            .HasColumnName("collected_date")
            .IsRequired()
            .HasColumnType("DATE");
        
        builder.Property(o => o.DeliveredDate)
            .HasColumnName("delievered_date")
            .IsRequired()
            .HasColumnType("DATE");

        builder.Property(o => o.UserId)
            .HasColumnName("user_id");

        builder.Property(o => o.AddressId)
            .HasColumnName("address_id");
        
        builder.Property(o => o.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
