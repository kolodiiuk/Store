using Store.Domain.Contracts.Repositories;
using Store.Domain.Entities;
using Store.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Store.DataAccess.Repositories;

public class AddressRepository(AppDbContext context) : GenericRepository<Address>(context), IAddressRepository
{
    public async Task<Result<IEnumerable<Address>>> GetUserAddresses(int userId)
    {
        try
        {
            var userAddresses = await _context.Addresses
                .Where(a => a.UserId == userId)
                .AsNoTracking()
                .ToListAsync();

            return Result.Success<IEnumerable<Address>>((userAddresses));
        }
        catch (Exception e)
        {
            return Result.Fail<IEnumerable<Address>>(e.Message);
        }
    }
}