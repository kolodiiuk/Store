using Laundry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laundry.DataAccess.EntityConfigs;

public class ServiceCouponConfiguration : IEntityTypeConfiguration<ServiceCoupon>
{
    public void Configure(EntityTypeBuilder<ServiceCoupon> builder)
    {
        builder.ToTable("service_coupon");

        builder.HasKey(sc => sc.Id);

        builder.HasOne(sc => sc.Service)
            .WithMany(s => s.ServiceCoupons)
            .HasForeignKey(sc => sc.ServiceId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(sc => sc.Coupon)
            .WithMany(c => c.ServiceCoupons)
            .HasForeignKey(sc => sc.CouponId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(sc => sc.Id)
            .HasColumnName("service_coupon_id");
        
        builder.Property(sc => sc.ServiceId)
            .HasColumnName("service_id");

        builder.Property(sc => sc.CouponId)
            .HasColumnName("coupon_id");
    }
}