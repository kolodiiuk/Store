using Laundry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laundry.DataAccess.EntityConfigs;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable("coupon");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .HasColumnName("coupon_id");
        
        builder.Property(c => c.Code)
            .IsRequired()
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