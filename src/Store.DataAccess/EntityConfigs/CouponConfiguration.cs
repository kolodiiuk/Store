using Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.DataAccess.EntityConfigs;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable("coupon");
        
        builder.HasKey(c => c.Id);
        
        builder.HasMany(c => c.ProductCoupons)
            .WithOne(sc => sc.Coupon)
            .HasForeignKey(sc => sc.ProductId);
        
        builder.HasMany(c => c.Orders)
            .WithOne(o => o.Coupon)
            .HasForeignKey(o => o.CouponId);
        
        builder.Property(c => c.Id)
            .HasColumnName("coupon_id");
        
        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnName("code");
        
        builder.Property(c => c.Percentage)
            .HasColumnName("percentage")
            .IsRequired();

        builder.Property(c => c.StartDate)
            .HasColumnName("start_date")
            .IsRequired()
            .HasColumnType("DATE");
        
        builder.Property(c => c.EndDate)
            .HasColumnName("end_date")
            .IsRequired()
            .HasColumnType("DATE");

        builder.Property(c => c.UsedCount)
            .HasColumnName("used_count")
            .IsRequired();
    }
}