using Laundry.Domain.Enums;

namespace Laundry.API.Dto;

public class CreateServiceDto
{
    public string Name { get; set; }
    public ServiceCategory Category { get; set; }
    public string Description { get; set; }
    public decimal PricePerUnit { get; set; }
    public UnitType UnitType { get; set; }
    public bool IsAvailable { get; set; }
}

public class UpdateServiceDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ServiceCategory Category { get; set; }
    public string Description { get; set; }
    public decimal PricePerUnit { get; set; }
    public UnitType UnitType { get; set; }
    public bool IsAvailable { get; set; }
}
