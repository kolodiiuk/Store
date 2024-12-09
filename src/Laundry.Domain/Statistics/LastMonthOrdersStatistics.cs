namespace Laundry.Domain.Statistics;

public class LastMonthOrdersStatistics
{
    public int Day { get; set; }
    public int OrderCountPerDay { get; set; }
    public decimal TotalPerDay { get; set; }
}