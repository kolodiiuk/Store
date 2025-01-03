using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;
using Laundry.Domain.Utils;

namespace Laundry.DataAccess.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(LaundryDbContext context) : base(context)
    {
        
    }
    
    public Task<Result<IQueryable<Service>>> GetAllAvailableServicesAsync()
    {
        throw new NotImplementedException();
    }
}
