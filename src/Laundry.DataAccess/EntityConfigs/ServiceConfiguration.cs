using Laundry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laundry.DataAccess.EntityConfigs;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("service");

        builder.HasKey(s => s.Id);
        
        builder.Property(p => p.RowVersion)
            .IsRowVersion();
        
        builder.HasMany(s => s.ServiceCoupons)
            .WithOne(sc => sc.Service)
            .HasForeignKey(sc => sc.ServiceId);
        
        builder.HasMany(s => s.BasketItems)
            .WithOne(bi => bi.Service)
            .HasForeignKey(bi => bi.ServiceId);
        
        builder.HasMany(s => s.OrderItems)
            .WithOne(oi => oi.Service)
            .HasForeignKey(oi => oi.ServiceId);
        
        builder.Property(s => s.Id)
            .HasColumnName("service_id");
        
        builder.Property(s => s.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Category)
            .HasColumnName("category")
            .IsRequired();

        builder.Property(s => s.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(s => s.PricePerUnit)
            .HasColumnName("price_per_unit")
            .IsRequired();

        builder.Property(s => s.UnitType)
            .HasColumnName("unit_type")
            .IsRequired();

        builder.Property(s => s.IsAvailable)
            .HasColumnName("is_available")
            .IsRequired();
    }
}