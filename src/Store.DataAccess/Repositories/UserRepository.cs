using Store.Domain.Contracts.Repositories;
using Store.Domain.Entities;
using Store.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Store.DataAccess.Repositories;

public class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository
{
    public async Task<Result<IEnumerable<Address>>> GetUserAddresses(int userId)
    {
        try
        {
            var addresses = await _context.Addresses
                .Where(a => a.UserId == userId).ToListAsync();

            return Result.Success((IEnumerable<Address>)addresses);
        }
        catch (Exception e)
        {
            return Result.Fail<IEnumerable<Address>>(
                $"Error fetching addresses of user {userId}: {e.Message}");
        }
    }

    public async Task<Result<User>> GetUserByEmailPassword(string email, string password)
    {
        try
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);

            if (user == null)
            {
                return Result.Fail<User>("No such user");
            }

            return Result.Success(user);
        }
        catch (Exception e)
        {
            return Result.Fail<User>(
                $"Error getting user {email}: {e.Message}");
        }
    }
}