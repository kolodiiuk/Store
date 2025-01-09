namespace Laundry.Domain.Statistics;

public class LastYearOrdersStatistics
{
    public int Month { get; set; }
    public int OrderCountPerMonth { get; set; }
    public decimal TotalPerMonth { get; set; }
}