namespace Laundry.Domain.Entities;

public class User : BaseEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public Guid AddressId { get; set; }
    public Address Address { get; set; }
}
