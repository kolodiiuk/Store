using Laundry.Domain.Statistics;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface IStatisticsRepository
{
    public Task<Result<IEnumerable<CustomersWhichOrderedTheMostOften>>> GetCustomersWhichOrderedTheMostOftenAsync();
    public Task<Result<IEnumerable<TheMostFrequentlyOrderedServices>>> GetTheMostFrequentlyOrderedServicesAsync();
    public Task<Result<IEnumerable<LastYearOrdersStatistics>>> GetLastYearOrdersStatisticsAsync();
    public Task<Result<IEnumerable<LastMonthOrdersStatistics>>> GetLastMonthOrdersStatisticsAsync();
}