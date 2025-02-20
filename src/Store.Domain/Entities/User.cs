using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Store.Domain.Enums;

namespace Store.Domain.Entities;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public Role Role { get; set; }
    
    [JsonIgnore]
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    
    [JsonIgnore]
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    
    [JsonIgnore]
    public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
}
