using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Laundry.DataAccess;

public class LaundryDbContextFactory : IDesignTimeDbContextFactory<LaundryDbContext>
{
    public LaundryDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LaundryDbContext>();
        optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=laundry;User=myuser;Password=mypassword;");

        return new LaundryDbContext(optionsBuilder.Options);
    }
}