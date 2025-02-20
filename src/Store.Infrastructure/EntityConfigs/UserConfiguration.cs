using Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Infrastructure.EntityConfigs;

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

        builder.HasMany(u => u.Addresses)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(u => u.BasketItems)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId)
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

        builder.Property(u => u.Role)
            .HasColumnName("role")
            .IsRequired();
        
        // Identity-specific properties
        builder.Property(u => u.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.PhoneNumber)
            .HasColumnName("phone_number")
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.UserName)
            .HasColumnName("username")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.NormalizedUserName)
            .HasColumnName("normalized_username")
            .HasMaxLength(150);

        builder.Property(u => u.NormalizedEmail)
            .HasColumnName("normalized_email")
            .HasMaxLength(150);

        builder.Property(u => u.EmailConfirmed)
            .HasColumnName("email_confirmed")
            .IsRequired();

        builder.Property(u => u.SecurityStamp)
            .HasColumnName("security_stamp")
            .HasMaxLength(36);

        builder.Property(u => u.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp")
            .HasMaxLength(36);

        builder.Property(u => u.PhoneNumberConfirmed)
            .HasColumnName("phone_number_confirmed")
            .IsRequired();

        builder.Property(u => u.TwoFactorEnabled)
            .HasColumnName("two_factor_enabled")
            .IsRequired();

        builder.Property(u => u.LockoutEnd)
            .HasColumnName("lockout_end");

        builder.Property(u => u.LockoutEnabled)
            .HasColumnName("lockout_enabled")
            .IsRequired();

        builder.Property(u => u.AccessFailedCount)
            .HasColumnName("access_failed_count")
            .IsRequired();
    }
}
