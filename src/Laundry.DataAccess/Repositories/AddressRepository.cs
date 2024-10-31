using Laundry.DataAccess.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;

namespace Laundry.DataAccess.Repository;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    public AddressRepository(LaundryDbContext context) : base(context)
    {
        
    }
}