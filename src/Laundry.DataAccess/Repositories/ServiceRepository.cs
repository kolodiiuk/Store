using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(LaundryDbContext context) : base(context)
    {
    }

    public async Task<Result<IEnumerable<Service>>> GetAllAvailableServicesAsync()
    {
        try
        {
            var services = await _context.Services
                .Where(s => s.IsAvailable == true).ToListAsync();
            if (services == null)
            {
                return Result<IEnumerable<Service>>.Fail<IEnumerable<Service>>($"No services");
            }

            return Result<IEnumerable<Service>>.Success<IEnumerable<Service>>((IEnumerable<Service>)services);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Service>>.Fail<IEnumerable<Service>>(
                $"Error fetching available services: {e.Message}");
        }
    }
}