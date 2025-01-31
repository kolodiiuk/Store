using System.Reflection;
using Laundry.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess;

public class LaundryDbContext : DbContext
{
    public LaundryDbContext(DbContextOptions<LaundryDbContext> options) : base(options)
    {
    }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceCoupon> ServiceCoupons { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(LaundryDbContext)));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=laundry;User=myuser;Password=mypassword;");
    }
}