using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    public AddressRepository(LaundryDbContext context) : base(context)
    {
    }

    public Result<IQueryable<Address>> GetUserAddresses(int userId)
    {
        try
        {
            var userAddresses = _context.Addresses
                .Where(a => a.UserId == userId)
                .AsNoTracking();

            return Result.Success<IQueryable<Address>>(userAddresses);
        }
        catch (Exception e)
        {
            return Result.Fail<IQueryable<Address>>(e.Message);
        }
    }
}