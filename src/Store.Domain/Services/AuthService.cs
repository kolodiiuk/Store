using Store.Domain.Utils;
using Store.Domain.Contracts.Repositories;
using Store.Domain.Contracts.Services;
using Store.Domain.Entities;

namespace Store.Domain.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    
    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<int> RegisterAsync(User user)
    {
        var result = await _userRepository.CreateAsync(user);
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }

    public async Task<User> LoginAsync(string email, string password)
    {
        var result = await _userRepository.GetUserByEmailPassword(email, password);
        result.OnFailure(() => throw new ArgumentException(result.Error));
        
        return result.Value;
    }
    
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var result = await _userRepository.GetAllAsync();
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }

    public async Task<User> GetUserAsync(int userId)
    {
        var result = await _userRepository.GetByIdAsync(userId);
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }
}
