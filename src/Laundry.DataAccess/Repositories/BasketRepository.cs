using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;
using Laundry.Domain.Utils;

namespace Laundry.DataAccess.Repositories;

public class BasketRepository : GenericRepository<BasketItem>, IBasketRepository
{
    public BasketRepository(LaundryDbContext context) : base(context)
    {
        
    }

    public async Task<Result<IQueryable<BasketItem>>> GetUserBasket(int userId)
    {
        throw new NotImplementedException();
    }
}