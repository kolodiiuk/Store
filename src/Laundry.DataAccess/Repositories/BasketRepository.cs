using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class BasketRepository : GenericRepository<BasketItem>, IBasketRepository
{
    public BasketRepository(LaundryDbContext context) : base(context)
    {
    }

    public async Task<Result<IEnumerable<BasketItem>>> GetUserBasket(int userId)
    {
        try
        {
            var basket = await _context.BasketItems
                .Where(bi => bi.UserId == userId).ToListAsync();
            if (basket == null)
            {
                return Result<IEnumerable<BasketItem>>.Fail<IEnumerable<BasketItem>>($"No basketitems");
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