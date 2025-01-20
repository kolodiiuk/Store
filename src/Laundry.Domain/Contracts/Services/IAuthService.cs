using Laundry.Domain.Entities;

namespace Laundry.Domain.Contracts.Services;

public interface IAuthService
{
    Task<int> RegisterAsync(User user);
    Task<bool> LoginAsync(string email, string password);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserAsync(int userId);
    Task<IEnumerable<Address>> GetUserAddresses(int userId);
    Task<int> CreateAddressAsync(Address address);
}
