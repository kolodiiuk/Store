using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class BasketRepository : GenericRepository<BasketItem>, IBasketRepository
{
    public BasketRepository(LaundryDbContext context) : base(context)
    {
        
    }

    public Result<IQueryable<BasketItem>> GetUserBasket(int userId)
    {
        try
        {
            var userBasket = _context.BasketItems
                .Where(bi => bi.UserId == userId)
                .AsNoTracking();

            return Result.Success<IQueryable<BasketItem>>(userBasket);
        }
        catch (Exception e)
        {
            return Result.Fail<IQueryable<BasketItem>>(e.Message);
        }
    }
}