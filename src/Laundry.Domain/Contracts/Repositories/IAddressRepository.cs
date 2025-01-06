using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface IAddressRepository : IGenericRepository<Address>
{
    Result<IQueryable<Address>> GetUserAddresses(int userId);
}