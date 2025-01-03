using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface IAddressRepository : IGenericRepository<Address>
{
    Task<Result<IQueryable<Address>>> GetUserAddress(int userId);
}