namespace  Laundry.Domain.Entities;

public class Address : BaseEntity
{
    public string Apartments { get; set; }
    public string House { get; set; }
    public string Street { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public int? UserId { get; set; } 
    
    public User? User { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
