using Store.Domain.Entities;
using Store.Domain.Utils;

namespace Store.Domain.Contracts.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<Result<IEnumerable<Address>>> GetUserAddresses(int userId);
    
    Task<Result<User>> GetUserByEmailPassword(string email, string password);
}
