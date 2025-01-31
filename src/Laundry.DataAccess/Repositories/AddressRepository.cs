using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class AddressRepository(LaundryDbContext context) : GenericRepository<Address>(context), IAddressRepository
{
    public Result<IEnumerable<Address>> GetUserAddresses(int userId)
    {
        try
        {
            var userAddresses = _context.Addresses
                .Where(a => a.UserId == userId)
                .AsNoTracking();

            return Result.Success<IEnumerable<Address>>((IEnumerable<Address>) userAddresses);
        }
        catch (Exception e)
        {
            return Result.Fail<IEnumerable<Address>>(e.Message);
        }
    }
}