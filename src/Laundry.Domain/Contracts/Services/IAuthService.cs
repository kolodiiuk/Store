using Laundry.Domain.Entities;

namespace Laundry.Domain.Contracts.Services;

public interface IAuthService
{
    Task<int> RegisterAsync(User user);
    Task<User> LoginAsync(string email, string password);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserAsync(int userId);
    Task<IEnumerable<Address>> GetAllAddressesAsync();
    Task<IEnumerable<Address>> GetUserAddressesAsync(int userId);
    Task<Address> GetAddressByIdAsync(int addressId);
    Task<int> CreateAddressAsync(Address address);
    Task UpdateAddressAsync(Address address);
    Task DeleteAddressAsync(int addressId);
}
