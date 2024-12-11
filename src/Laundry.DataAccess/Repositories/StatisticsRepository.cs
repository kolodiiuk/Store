using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Statistics;
using Laundry.Domain.Utils;

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

    public Task<Result<TheMostFrequentlyOrderedServices>> GetTheMostFrequentlyOrderedServices()
    {
        throw new NotImplementedException();
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