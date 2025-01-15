using Laundry.Domain.Entities;

namespace Laundry.Domain.Contracts.Services;

public interface IAuthService
{
    Task RegisterAsync(User user);
    Task LoginAsync(string email, string password);
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserAsync(int userId);
}
