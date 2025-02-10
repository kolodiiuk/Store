using Store.Domain.Contracts.Repositories;
using Store.Domain.Entities;
using Store.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Store.DataAccess.Repositories;

public class BasketRepository : GenericRepository<BasketItem>, IBasketRepository
{
    public BasketRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Result<IEnumerable<BasketItem>>> GetUserBasketAsync(int userId)
    {
        try
        {
            var basket = await _context.BasketItems
                .Where(bi => bi.UserId == userId).ToListAsync();
            
            return Result.Success<IEnumerable<BasketItem>>(basket);
        }
        catch (Exception e)
        {
            return Result.Fail<IEnumerable<BasketItem>>(
                $"Error fetching basket of user {userId}: {e.Message}");
        }
    }
}
