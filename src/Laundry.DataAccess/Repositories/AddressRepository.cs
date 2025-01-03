using Laundry.DataAccess.Repositories;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;
using Laundry.Domain.Utils;

namespace Laundry.DataAccess.Repository;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    public AddressRepository(LaundryDbContext context) : base(context)
    {
        
    }

    public async Task<Result<IQueryable<Address>>> GetUserAddress(int userId)
    {
        throw new NotImplementedException();
    }
}