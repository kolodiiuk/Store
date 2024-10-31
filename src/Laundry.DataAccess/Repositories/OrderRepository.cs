using Laundry.Api.Interfaces;
using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Entities;

namespace Laundry.Api.DataAccess.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(LaundryDbContext context) : base(context)
    {
        
    }
    // add, getByCond, 
}