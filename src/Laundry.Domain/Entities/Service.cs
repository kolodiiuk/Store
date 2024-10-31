using Laundry.Domain.Enums;

namespace Laundry.Domain.Entities;

public class Service : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ServiceType Type { get; set; }
    public string Description { get; set; }
    public decimal PricePerUnit { get; set; }
    public UnitType UnitType { get; set; }
    public bool IsAvailable { get; set; }
}
