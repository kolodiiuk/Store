namespace Laundry.API.Dto;

public class CreateAddressDto
{
    public string Apartments { get; set; }
    public string House { get; set; }
    public string Street { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public int UserId { get; set; }
}

public class UpdateAddressDto
{
    public int Id { get; set; }
    public string Apartments { get; set; }
    public string House { get; set; }
    public string Street { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public int UserId { get; set; }
}
