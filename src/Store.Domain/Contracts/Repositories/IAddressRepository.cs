using Store.Domain.Entities;
using Store.Domain.Utils;

namespace Store.Domain.Contracts.Repositories;

public interface IAddressRepository : IGenericRepository<Address>
{
    Task<Result<IEnumerable<Address>>> GetUserAddresses(int userId);
}