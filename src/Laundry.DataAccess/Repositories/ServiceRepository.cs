using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;

namespace Laundry.Api.DataAccess.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(LaundryDbContext context) : base(context)
    {
        
    }
    // getAll, getByName
}