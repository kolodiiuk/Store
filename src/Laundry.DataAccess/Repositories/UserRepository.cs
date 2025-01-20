using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Result = MySqlX.XDevAPI.Common.Result;

namespace Laundry.DataAccess.Repositories;

public class UserRepository(LaundryDbContext context) : GenericRepository<User>(context), IUserRepository
{
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

    public async Task<Result<User>> GetUserByEmailPassword(string email, string password)
    {
        try
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                return Result<User>.Fail<User>("No such user");
            }

            return Result<User>.Success(user);
        }
        catch (Exception e)
        {
            return Result<User>.Fail<User>(
                $"Error getting user {email}: {e.Message}");
        }
    }
}