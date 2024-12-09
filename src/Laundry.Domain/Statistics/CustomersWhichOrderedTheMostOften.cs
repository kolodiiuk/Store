namespace Laundry.Domain.Statistics;

public class CustomersWhichOrderedTheMostOften
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int OrderCount { get; set; }
    public double AvgRating { get; set; }
    public decimal SumOrders { get; set; }
}