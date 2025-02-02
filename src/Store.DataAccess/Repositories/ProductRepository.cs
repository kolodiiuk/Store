using Store.Domain.Contracts.Repositories;
using Store.Domain.Entities;
using Store.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Store.DataAccess.Repositories;

public class ProductRepository(AppDbContext context) 
    : GenericRepository<Product>(context), IProductRepository
{
    public async Task<Result<IEnumerable<Product>>> GetAllAvailableProductsAsync()
    {
        try
        {
            var services = await _context.Products
                .Where(s => s.IsAvailable == true).ToListAsync();
            
            return Result.Success<IEnumerable<Product>>(services);
        }
        catch (Exception e)
        {
            return Result.Fail<IEnumerable<Product>>(
                $"Error fetching available services: {e.Message}");
        }
    }
}