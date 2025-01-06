using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(LaundryDbContext context) : base(context)
    {
        
    }

    public async Task<Result<IQueryable<Order>>> GetOrdersForUser(int userId)
    {
        try
        {
            var userAddresses = _context.Addresses
                .Where(a => a.UserId == userId)
                .AsNoTracking();

            return Result.Success(userAddresses);
        }
        catch (Exception e)
        {
            return Result.Fail<IQueryable<Address>>(e.Message);
        }

    }
}
// if count isn't 0, if service is correct, if isn't expired