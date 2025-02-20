using Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Infrastructure.EntityConfigs;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        // Primary Key
        builder.HasKey(o => o.Id);

        // Relationships
        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);

        builder.HasMany(o => o.Feedbacks)
            .WithOne(f => f.Order)
            .HasForeignKey(f => f.OrderId);

        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .IsRequired(false) 
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(o => o.Address)
            .WithMany(a => a.Orders)
            .HasForeignKey(o => o.AddressId)
            .IsRequired(false) 
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(o => o.Coupon)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CouponId)
            .OnDelete(DeleteBehavior.SetNull);

        // Props
        builder.Property(o => o.Id)
            .HasColumnName("order_id");

        builder.Property(o => o.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.Property(o => o.Subtotal)
            .HasColumnName("subtotal")
            .IsRequired();

        builder.Property(o => o.Description)
            .HasColumnName("description")
            .HasMaxLength(1000) 
            .IsRequired(false);

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
            .HasColumnType("CHAR(27)")
            .IsRequired(false); 

        builder.Property(o => o.CouponId)
            .HasColumnName("coupon_id");

        builder.Property(o => o.CollectedDate)
            .HasColumnName("collected_date")
            .HasColumnType("DATE");

        builder.Property(o => o.DeliveredDate)
            .HasColumnName("delivered_date") 
            .HasColumnType("DATE");

        builder.Property(o => o.UserId)
            .HasColumnName("user_id")
            .IsRequired(false);

        builder.Property(o => o.AddressId)
            .HasColumnName("address_id")
            .IsRequired(false);

        builder.Property(o => o.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("TIMESTAMP");
    }
}
