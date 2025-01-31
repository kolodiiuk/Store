using Laundry.Domain.Statistics;

namespace Laundry.Domain.Contracts.Services;

public interface IStatisticsService
{
     Task<IEnumerable<CustomersWhichOrderedTheMostOften>> GetCustomersWhichOrderedTheMostOftenAsync();
     Task<IEnumerable<TheMostFrequentlyOrderedServices>> GetTheMostFrequentlyOrderedServicesAsync();
     Task<IEnumerable<LastYearOrdersStatistics>> GetLastYearOrdersStatisticsAsync();
     Task<IEnumerable<LastMonthOrdersStatistics>> GetLastMonthOrdersStatisticsAsync();
}
