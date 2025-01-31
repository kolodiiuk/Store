using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class BasketRepository(LaundryDbContext context) : GenericRepository<BasketItem>(context), IBasketRepository
{
    public async Task<Result<IEnumerable<BasketItem>>> GetUserBasketAsync(int userId)
    {
        try
        {
            var basket = await _context.BasketItems
                .Where(bi => bi.UserId == userId).ToListAsync();
            if (basket == null)
            {
                return Result<IEnumerable<BasketItem>>.Fail<IEnumerable<BasketItem>>($"No basket items");
            }

            return Result<IEnumerable<BasketItem>>.Success((IEnumerable<BasketItem>)basket);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<BasketItem>>.Fail<IEnumerable<BasketItem>>(
                $"Error fetching basket of user {userId}: {e.Message}");
        }
    }
}