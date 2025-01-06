using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Statistics;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly LaundryDbContext _context;

    public StatisticsRepository(LaundryDbContext context)
    {
        _context = context;
    }

    public Task<Result<CustomersWhichOrderedTheMostOften>> GetCustomersWhichOrderedTheMostOften()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<TheMostFrequentlyOrderedServices>> GetTheMostFrequentlyOrderedServices()
    {
        try
        {
            var orders = await _context.OrderItems
                .Include(oi => oi.Service)
                .GroupBy(oi => new { oi.ServiceId, oi.Service.Name, oi.Service.Category, 
                    oi.Service.PricePerUnit, oi.Service.UnitType })
                .Select(g => new TheMostFrequentlyOrderedServices
                {
                    ServiceName = g.Key.Name,
                    ServiceCategory = g.Key.Category,
                    ServicePrice = g.Key.PricePerUnit,
                    UnitType = g.Key.UnitType,
                    OrderCount = g.Count(),
                    ClientOrderedServiceCount = g.Sum(oi => oi.Quantity)
                })
                .OrderByDescending(s => s.OrderCount)
                .FirstOrDefaultAsync();

            if (orders == null)
            {
                return Result<TheMostFrequentlyOrderedServices>.Fail("No services found.");
            }

            return Result<TheMostFrequentlyOrderedServices>.Success(orders);
        }
        catch (Exception e)
        {
            return Result<TheMostFrequentlyOrderedServices>.Fail(e.Message);
        }
    }

    public Task<Result<LastYearOrdersStatistics>> GetLastYearOrdersStatistics()
    {
        throw new NotImplementedException();
    }

    public Task<Result<LastMonthOrdersStatistics>> GetLastMonthOrdersStatistics()
    {
        throw new NotImplementedException();
    }
}