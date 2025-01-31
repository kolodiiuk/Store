using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<Result<IEnumerable<Address>>> GetUserAddresses(int userId);
    // Task<Result> AddAddress(int userId);
    Task<Result<User>> GetUserByEmailPassword(string email, string password);
}
