namespace Laundry.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public int AddressId { get; set; }
    public ICollection<Address> Addresses { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; }
}
