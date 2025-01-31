using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Statistics;

namespace Laundry.Domain.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IStatisticsRepository _statisticsRepository;

    public StatisticsService(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }
    
    public async Task<IEnumerable<CustomersWhichOrderedTheMostOften>> GetCustomersWhichOrderedTheMostOftenAsync()
    {
        var stats = await _statisticsRepository.GetCustomersWhichOrderedTheMostOftenAsync();
        if (stats.Failure)
        {
            throw new Exception("Failure getting stats");
        }

        return stats.Value;
    }

    public async Task<IEnumerable<TheMostFrequentlyOrderedServices>> GetTheMostFrequentlyOrderedServicesAsync()
    {
        var stats = await _statisticsRepository.GetTheMostFrequentlyOrderedServicesAsync();
        if (stats.Failure)
        {
            throw new Exception("Failure getting stats");
        }

        return stats.Value;
    }

    public async Task<IEnumerable<LastYearOrdersStatistics>> GetLastYearOrdersStatisticsAsync()
    {
        var stats = await _statisticsRepository.GetLastYearOrdersStatisticsAsync();
        if (stats.Failure)
        {
            throw new Exception("Failure getting stats");
        }

        return stats.Value;
    }

    public async Task<IEnumerable<LastMonthOrdersStatistics>> GetLastMonthOrdersStatisticsAsync()
    {
        var stats = await _statisticsRepository.GetLastMonthOrdersStatisticsAsync();
        if (stats.Failure)
        {
            throw new Exception("Failure getting stats");
        }

        return stats.Value;
    }
}
