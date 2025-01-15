using Laundry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laundry.DataAccess.EntityConfigs;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_item");
        
        builder.HasKey(oi => oi.Id);
        
        builder.HasOne(oi => oi.Service)
            .WithMany(s => s.OrderItems)
            .HasForeignKey(oi => oi.ServiceId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(o => o.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(oi => oi.Id)
            .HasColumnName("order_item_id");
        
        builder.Property(oi => oi.Quantity)
            .HasColumnName("quantity")
            .IsRequired();
        
        builder.Property(oi => oi.Total)
            .HasColumnName("total")
            .IsRequired();
        
        builder.Property(oi => oi.CurrentUnitPrice)
            .HasColumnName("current_price_per_unit")
            .IsRequired();

        builder.Property(oi => oi.ServiceId)
            .HasColumnName("service_id");

        builder.Property(oi => oi.OrderId)
            .HasColumnName("order_id");
    }
}
