using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(LaundryDbContext context) : base(context)
    {
    }

    public async Task<Result<IEnumerable<Address>>> GetUserAddresses(int userId)
    {
        try
        {
            var addresses = await _context.Addresses
                .Where(a => a.UserId == userId).ToListAsync();
            if (addresses == null)
            {
                return Result<IEnumerable<Address>>.Fail<IEnumerable<Address>>($"No addresses of user {userId}");
            }

            return Result<IEnumerable<Address>>.Success((IEnumerable<Address>)addresses);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Address>>.Fail<IEnumerable<Address>>(
                $"Error fetching addresses of user {userId}: {e.Message}");
        }
    }
}