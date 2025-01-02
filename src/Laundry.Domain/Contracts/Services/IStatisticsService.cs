using Laundry.Domain.Statistics;

namespace Laundry.Domain.Contracts.Services;

public interface IStatisticsService
{
     CustomersWhichOrderedTheMostOften GetCustomersWhichOrderedTheMostOften();
     TheMostFrequentlyOrderedServices GetTheMostFrequentlyOrderedServices();
     LastYearOrdersStatistics GetLastYearOrdersStatistics();
     LastMonthOrdersStatistics GetLastMonthOrdersStatistics();
}
