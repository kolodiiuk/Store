using Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.DataAccess.EntityConfigs;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.ToTable("basket_item");
        
        builder.HasKey(bi => bi.Id);
        
        builder.HasOne(bi => bi.User)
            .WithMany(u => u.BasketItems)
            .HasForeignKey(bi => bi.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(bi => bi.Product)
            .WithMany(s => s.BasketItems)
            .HasForeignKey(bi => bi.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(bi => bi.Id)
            .HasColumnName("basket_item_id");

        builder.Ignore(bi => bi.Total);
        
        builder.Property(bi => bi.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        builder.Property(bi => bi.ProductId)
            .HasColumnName("service_id");

        builder.Property(bi => bi.UserId)
            .HasColumnName("user_id");
    }
}
