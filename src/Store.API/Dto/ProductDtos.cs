using Store.Domain.Enums;

namespace Store.API.Dto;

public class CreateProductDto
{
    public string Name { get; set; }
    public ProductCategory? Category { get; set; }
    public string Description { get; set; }
    public decimal? PricePerUnit { get; set; }
    public UnitType? UnitType { get; set; }
    public bool? IsAvailable { get; set; }
}

public class UpdateProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ProductCategory Category { get; set; }
    public string Description { get; set; }
    public decimal PricePerUnit { get; set; }
    public UnitType UnitType { get; set; }
    public bool IsAvailable { get; set; }
}
