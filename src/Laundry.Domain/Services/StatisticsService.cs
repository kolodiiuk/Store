using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Statistics;

namespace Laundry.Domain.Services;

public class StatisticsService : IStatisticsService
{
    public CustomersWhichOrderedTheMostOften GetCustomersWhichOrderedTheMostOften()
    {
        throw new NotImplementedException();
    }

    public TheMostFrequentlyOrderedServices GetTheMostFrequentlyOrderedServices()
    {
        throw new NotImplementedException();
    }

    public LastYearOrdersStatistics GetLastYearOrdersStatistics()
    {
        throw new NotImplementedException();
    }

    public LastMonthOrdersStatistics GetLastMonthOrdersStatistics()
    {
        throw new NotImplementedException();
    }
}