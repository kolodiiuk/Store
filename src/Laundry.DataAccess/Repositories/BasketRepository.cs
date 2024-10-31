using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;

namespace Laundry.Api.DataAccess.Repositories;

public class BasketRepository : GenericRepository<Basket>, IBasketRepository
{
    public BasketRepository(LaundryDbContext context) : base(context)
    {
        
    }
    // add, getByCond, delete
}