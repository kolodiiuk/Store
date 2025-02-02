using Store.Domain.Entities;

namespace Store.Domain.Contracts.Services;

public interface IAuthService
{
    Task<int> RegisterAsync(User user);
    Task<User> LoginAsync(string email, string password);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserAsync(int userId);
}
