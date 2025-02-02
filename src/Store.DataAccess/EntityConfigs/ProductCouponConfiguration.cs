using Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.DataAccess.EntityConfigs;

public class ProductCouponConfiguration : IEntityTypeConfiguration<ProductCoupon>
{
    public void Configure(EntityTypeBuilder<ProductCoupon> builder)
    {
        builder.ToTable("service_coupon");

        builder.HasKey(sc => sc.Id);
        
        builder.HasOne(sc => sc.Product)
            .WithMany(s => s.ProductCoupons)
            .HasForeignKey(sc => sc.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(sc => sc.Coupon)
            .WithMany(c => c.ProductCoupons)
            .HasForeignKey(sc => sc.CouponId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(sc => sc.Id)
            .HasColumnName("service_coupon_id");
        
        builder.Property(sc => sc.ProductId)
            .HasColumnName("service_id");

        builder.Property(sc => sc.CouponId)
            .HasColumnName("coupon_id");
    }
}