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
        
        builder.HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId);

        builder.Property(a => a.Id)
            .HasColumnName("address_id");

        builder.Property(a => a.Apartments)
            .HasColumnName("apartments");
        
        builder.Property(a => a.House)
            .HasColumnName("house")
            .IsRequired();
        
        builder.Property(a => a.Street)
            .HasColumnName("street")
            .IsRequired();
        
        builder.Property(a => a.District)
            .HasColumnName("district")
            .IsRequired();
        
        builder.Property(a => a.City)
            .HasColumnName("city")
            .IsRequired();
        
        builder.Property(a => a.UserId)
            .HasColumnName("user_id")
            .IsRequired();
    }
}
