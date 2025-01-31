using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IAddressRepository _addressRepository;
    
    public AuthService(IUserRepository userRepository, 
        IAddressRepository addressRepository)
    {
        _userRepository = userRepository;
        _addressRepository = addressRepository;
    }
    
    public async Task<int> RegisterAsync(User user)
    {
        var result = await _userRepository.CreateAsync(user);
        if (result.Failure)
        {
            throw new Exception(result.Error);
        }

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
        if (result.Failure)
        {
            throw new Exception(result.Error);
        }

        return result.Value;
    }

    public async Task<User> GetUserAsync(int userId)
    {
        var result = await _userRepository.GetByIdAsync(userId);
        if (result.Failure)
        {
            throw new Exception(result.Error);
        }

        return result.Value;
    }

    public async Task<IEnumerable<Address>> GetAllAddressesAsync()
    {
        var result = await _addressRepository.GetAllAsync();
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }

    public async Task<IEnumerable<Address>> GetUserAddressesAsync(int userId)
    {
        var result = _addressRepository.GetUserAddresses(userId);
        if (result.Failure)
        {
            throw new Exception(result.Error);
        }

        return result.Value;
    }

    public async Task<Address> GetAddressByIdAsync(int addressId)
    {
        var result = await _addressRepository.GetByIdAsync(addressId);
        result.OnFailure(
            () => throw new InvalidOperationException(result.Error));
        
        return result.Value;
    }

    public async Task<int> CreateAddressAsync(Address address)
    {
        var result = await _addressRepository.CreateAsync(address);
        if (result.Failure)
        {
            throw new Exception(result.Error);
        }

        return result.Value;
    }

    public async Task UpdateAddressAsync(Address address)
    {
        var result = await _addressRepository.UpdateAsync(address);
        result.OnFailure(
            () => throw new InvalidOperationException(result.Error));
    }

    public async Task DeleteAddressAsync(int addressId)
    {
        var result = await _addressRepository.DeleteAsync(addressId);
        result.OnFailure(
            () => throw new InvalidOperationException(result.Error));
    }
}
