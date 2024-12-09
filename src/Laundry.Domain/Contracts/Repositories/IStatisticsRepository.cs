using Laundry.Domain.Statistics;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface IStatisticsRepository
{
    public Task<Result<CustomersWhichOrderedTheMostOften>> GetCustomersWhichOrderedTheMostOften();
    public Task<Result<TheMostFrequentlyOrderedServices>> GetTheMostFrequentlyOrderedServices();
    public Task<Result<LastYearOrdersStatistics>> GetLastYearOrdersStatistics();
    public Task<Result<LastMonthOrdersStatistics>> GetLastMonthOrdersStatistics();
}