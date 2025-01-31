using Laundry.Domain.Enums;

namespace Laundry.Domain.Statistics;

public class TheMostFrequentlyOrderedServices
{
    public string Name { get; set; }
    public ServiceCategory ServiceCategory { get; set; }
    public decimal ServicePrice { get; set; }
    public UnitType UnitType { get; set; } 
    public int OrderCount { get; set; }
    public int ClientOrderedServiceCount { get; set; }
}