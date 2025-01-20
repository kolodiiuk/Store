using Laundry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laundry.DataAccess.EntityConfigs;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("address");
        
        builder.HasKey(a => a.Id);
        
        builder.HasMany(a => a.Orders)
            .WithOne(o => o.Address)
            .HasForeignKey(o => o.AddressId);
        
        builder.HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(a => a.Id)
            .HasColumnName("address_id");

        builder.Property(a => a.Apartments)
            .HasColumnName("apartments")
            .HasMaxLength(20);
        
        builder.Property(a => a.House)
            .HasColumnName("house")
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(a => a.Street)
            .HasColumnName("street")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(a => a.District)
            .HasColumnName("district")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(a => a.City)
            .HasColumnName("city")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(a => a.UserId)
            .HasColumnName("user_id")
            .IsRequired(false);
    }
}
