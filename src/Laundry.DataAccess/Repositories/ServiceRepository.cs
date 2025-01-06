using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(LaundryDbContext context) : base(context)
    {
        
    }
    
    public Result<IQueryable<Service>> GetAllAvailableServicesAsync()
    {
        try
        {
            var availableServices = _context.Services
                .Where(s => s.IsAvailable == true)
                .AsNoTracking();

            return Result.Success<IQueryable<Service>>(availableServices);
        }
        catch (Exception e)
        {
            return Result.Fail<IQueryable<Service>>(e.Message);
        }

    }
}
