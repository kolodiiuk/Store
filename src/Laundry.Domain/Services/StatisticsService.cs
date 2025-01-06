using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Statistics;

namespace Laundry.Domain.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IStatisticsRepository _repository;

    public StatisticsService(IStatisticsRepository repository)
    {
        _repository = repository;
    }

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