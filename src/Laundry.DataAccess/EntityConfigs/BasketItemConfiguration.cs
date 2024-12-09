using Laundry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laundry.DataAccess.EntityConfigs;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.ToTable("basket_item");
        
        builder.HasKey(bi => bi.Id);
        
        builder.Property(bi => bi.Id)
            .HasColumnName("basket_item_id");
        
        builder.Property(bi => bi.Total)
            .HasColumnName("total");
        
        builder.Property(bi => bi.Quantity)
            .HasColumnName("quantity")
            .IsRequired();
        
        builder.Property(bi => bi.ServiceId)
            .HasColumnName("service_id")
            .IsRequired();
        
        builder.Property(bi => bi.UserId)
            .HasColumnName("user_id")
            .IsRequired();
    }
}
