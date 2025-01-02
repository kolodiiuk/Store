using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;

namespace Laundry.DataAccess.Repositories;

public class BasketRepository : GenericRepository<BasketItem>, IBasketRepository
{
    public BasketRepository(LaundryDbContext context) : base(context)
    {
        
    }
}