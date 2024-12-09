using System.Reflection;
using Laundry.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess;

public class LaundryDbContext : DbContext
{
    public LaundryDbContext(DbContextOptions options) : base(options)
    {
    } 
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(LaundryDbContext)));
    }
}