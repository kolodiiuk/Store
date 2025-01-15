using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;

namespace Laundry.Domain.Services;

public class AuthService : IAuthService
{
    private readonly IBasketService _basketService;
    private readonly IUserRepository _userRepository;

    public AuthService(IBasketService basketService, 
        IUserRepository userRepository)
    {
        _basketService = basketService;
        _userRepository = userRepository;
    }
    
    public Task RegisterAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserAsync(int userId)
    {
        throw new NotImplementedException();
    }
}
