using Laundry.Domain.Statistics;

namespace Laundry.Domain.Contracts.Services;

public interface IStatisticsService
{
    public CustomersWhichOrderedTheMostOften GetCustomersWhichOrderedTheMostOften();
    public TheMostFrequentlyOrderedServices GetTheMostFrequentlyOrderedServices();
    public LastYearOrdersStatistics GetLastYearOrdersStatistics();
    public LastMonthOrdersStatistics GetLastMonthOrdersStatistics();
}