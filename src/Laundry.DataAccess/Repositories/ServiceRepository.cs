using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;

namespace Laundry.DataAccess.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(LaundryDbContext context) : base(context)
    {
        
    }
    // getAll, getByName
}