using Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.DataAccess.EntityConfigs;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");

        builder.HasKey(s => s.Id);
        
        builder.HasMany(s => s.ProductCoupons)
            .WithOne(sc => sc.Product)
            .HasForeignKey(sc => sc.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(s => s.BasketItems)
            .WithOne(bi => bi.Product)
            .HasForeignKey(bi => bi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(s => s.OrderItems)
            .WithOne(oi => oi.Product)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Property(s => s.Id)
            .HasColumnName("product_id");
        
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

        builder.Property(s => s.Price)
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
