using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Store.Infrastructure;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Address> Addresses { get; set; }
    
    public DbSet<BasketItem> BasketItems { get; set; }
    
    public DbSet<Coupon> Coupons { get; set; }
    
    public DbSet<Feedback> Feedbacks { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderItem> OrderItems { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public DbSet<ProductCoupon> ProductCoupons { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext)));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=laundry;User=myuser;Password=mypassword;");
    }
}
