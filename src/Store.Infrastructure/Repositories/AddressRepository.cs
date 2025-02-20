using Store.Domain.Contracts.Repositories;
using Store.Domain.Entities;
using Store.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Store.Infrastructure.Repositories;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    public AddressRepository(AppDbContext context) : base(context)
    {
    }

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
