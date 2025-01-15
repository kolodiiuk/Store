using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Statistics;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly LaundryDbContext _context;
    private readonly IDateTimeProvider _dateTime;

    public StatisticsRepository(LaundryDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTime = dateTimeProvider;
    }

    public async Task<Result<IEnumerable<CustomersWhichOrderedTheMostOften>>>
        GetCustomersWhichOrderedTheMostOftenAsync()
    {
        try
        {
            var statistics = await _context.Set<CustomersWhichOrderedTheMostOften>()
                .FromSqlRaw(@"SELECT u.first_name AS FirstName,
                                  u.last_name AS LastName, 
                                  COUNT(o.order_id) AS OrderCount,
                                  AVG(COALESCE(f.rating, 0)) AS AvgRating,
                                  SUM(o.subtotal - COALESCE(o.discount, 0)) AS SumOrders
                              FROM user u
                              INNER JOIN orders o ON u.user_id = o.user_id
                              LEFT JOIN feedback f ON o.order_id = f.order_id
                              WHERE u.role = 0
                              GROUP BY u.user_id, u.first_name, u.last_name;")
                .ToListAsync();

            if (statistics == null)
            {
                return Result<IEnumerable<CustomersWhichOrderedTheMostOften>>
                    .Fail<IEnumerable<CustomersWhichOrderedTheMostOften>>("No statistics available");
            }

            return Result<IEnumerable<CustomersWhichOrderedTheMostOften>>
                .Success<IEnumerable<CustomersWhichOrderedTheMostOften>>(
                    (IEnumerable<CustomersWhichOrderedTheMostOften>)statistics);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<CustomersWhichOrderedTheMostOften>>
                .Fail<IEnumerable<CustomersWhichOrderedTheMostOften>>(
                    $"Error fetching customers which ordered the most statistics: {e.Message}");
        }
    }

    public async Task<Result<IEnumerable<TheMostFrequentlyOrderedServices>>> GetTheMostFrequentlyOrderedServicesAsync()
    {
        try
        {
            var statistics = await _context.Set<TheMostFrequentlyOrderedServices>()
                .FromSqlRaw(@$"SELECT s.service_name ServiceName, 
                                      s.category ServiceCategory, 
                                      s.price_per_unit ServicePrice, 
                                      s.unit_type UnitType, 
                                      COUNT(DISTINCT oi.order_id) OrderCount, 
                                      COUNT(DISTINCT o.user_id) ClientOrderedServiceCount
                               FROM service s
                               LEFT JOIN order_item oi ON s.service_id = oi.service_id
                               LEFT JOIN orders o ON oi.order_id = o.order_id
                               GROUP BY s.service_name, s.category, s.price_per_unit, s.unit_type
                               ORDER BY OrderCount DESC
                               LIMIT 10;")
                .ToListAsync();
            if (statistics == null)
            {
                return Result<IEnumerable<TheMostFrequentlyOrderedServices>>
                    .Fail<IEnumerable<TheMostFrequentlyOrderedServices>>(
                        "No statistics available");
            }

            return Result<IEnumerable<TheMostFrequentlyOrderedServices>>
                .Success<IEnumerable<TheMostFrequentlyOrderedServices>>(
                    (IEnumerable<TheMostFrequentlyOrderedServices>)statistics);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<TheMostFrequentlyOrderedServices>>
                .Fail<IEnumerable<TheMostFrequentlyOrderedServices>>(
                    $"Error fetching the most frequently " +
                    $"ordered services statistics: {e.Message}");
        }
    }

    public async Task<Result<IEnumerable<LastYearOrdersStatistics>>> GetLastYearOrdersStatisticsAsync()
    {
        try
        {
            var lastYear = _dateTime.Now.AddYears(-1);
            var statistics = await _context.Orders
                .Where(o => o.CreatedAt >= lastYear)
                .GroupBy(o => o.CreatedAt.Month)
                .Select(g => new LastYearOrdersStatistics
                {
                    Month = g.Key,
                    OrderCountPerMonth = g.Count(),
                    TotalPerMonth = g.Sum(o => o.Subtotal)
                })
                .ToListAsync();

            if (statistics == null)
            {
                return Result<IEnumerable<LastYearOrdersStatistics>>
                    .Fail<IEnumerable<LastYearOrdersStatistics>>("No statistics available");
            }

            return Result<IEnumerable<LastYearOrdersStatistics>>
                .Success<IEnumerable<LastYearOrdersStatistics>>(
                    (IEnumerable<LastYearOrdersStatistics>)statistics);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<LastYearOrdersStatistics>>
                .Fail<IEnumerable<LastYearOrdersStatistics>>(
                    $"Error fetching last year orders statistics: {e.Message}");
        }
    }

    public async Task<Result<IEnumerable<LastMonthOrdersStatistics>>> GetLastMonthOrdersStatisticsAsync()
    {
        try
        {
            var lastMonth = _dateTime.Now.AddMonths(-1);
            var statistics = await _context.Orders
                .Where(o => o.CreatedAt >= lastMonth)
                .GroupBy(o => o.CreatedAt.Day)
                .Select(g => new LastMonthOrdersStatistics
                {
                    Day = g.Key,
                    OrderCountPerDay = g.Count(),
                    TotalPerDay = g.Sum(o => o.Subtotal)
                })
                .ToListAsync();
            if (statistics == null)
            {
                return Result<IEnumerable<LastMonthOrdersStatistics>>
                    .Fail<IEnumerable<LastMonthOrdersStatistics>>("No statistics available");
            }

            return Result<IEnumerable<LastMonthOrdersStatistics>>
                .Success<IEnumerable<LastMonthOrdersStatistics>>(statistics);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<LastMonthOrdersStatistics>>
                .Fail<IEnumerable<LastMonthOrdersStatistics>>(
                    $"Error fetching last month orders statistics: {e.Message}");
        }
    }
}
