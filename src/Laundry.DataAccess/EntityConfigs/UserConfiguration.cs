using Laundry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laundry.DataAccess.EntityConfigs;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasKey(u => u.Id);
 
        builder.HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(c => c.Addresses)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(c => c.BasketItems)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(u => u.Id)
            .HasColumnName("user_id");

        builder.Property(u => u.FirstName)
            .HasColumnName("first_name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .HasColumnName("last_name")
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.PhoneNumber)
            .HasColumnName("phone_number")
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(u => u.Password)
            .HasColumnName("password")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Role)
            .HasColumnName("role")
            .IsRequired();
    }
}