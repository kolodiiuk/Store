namespace  Laundry.Domain.Entities;

public class Address : BaseEntity
{
    public Guid Id { get; set; }
    public string Apartments { get; set; }
    public string House { get; set; }
    public string Street { get; set; }
    public string District { get; set; }
    public string City { get; set; }
}
