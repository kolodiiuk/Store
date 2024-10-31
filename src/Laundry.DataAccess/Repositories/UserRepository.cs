using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;

namespace Laundry.Api.DataAccess.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(LaundryDbContext context) : base(context)
    {
        
    }
    // getby, delete, add, update
}
