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

    public async Task<Result<User>> GetIfExistsUserAsync(string email, string password)
    {
        try
        {
            var user = await _context.Users
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                return Result<User>.Fail("User not found.");
            }
            return Result<User>.Success(user);
        }
        catch (Exception e)
        {
            return Result<User>.Fail(e.Message);
        }
    }
}
