using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<Result<IEnumerable<Address>>> GetUserAddresses(int userId);
}
