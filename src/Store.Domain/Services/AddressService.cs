using Store.Domain.Utils;
using Store.Domain.Contracts.Repositories;
using Store.Domain.Contracts.Services;
using Store.Domain.Entities;

namespace Store.Domain.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<IEnumerable<Address>> GetAllAddressesAsync()
    {
        var result = await _addressRepository.GetAllAsync();
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }

    public async Task<IEnumerable<Address>> GetUserAddressesAsync(int userId)
    {
        var result = await _addressRepository.GetUserAddresses(userId);
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }

    public async Task<Address> GetAddressByIdAsync(int addressId)
    {
        var result = await _addressRepository.GetByIdAsync(addressId);
        result.OnFailure(() => throw new InvalidOperationException(result.Error));

        return result.Value;
    }

    public async Task<int> CreateAddressAsync(Address address)
    {
        var result = await _addressRepository.CreateAsync(address);
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }

    public async Task UpdateAddressAsync(Address address)
    {
        var result = await _addressRepository.UpdateAsync(address);
        result.OnFailure(() => throw new InvalidOperationException(result.Error));
    }

    public async Task DeleteAddressAsync(int addressId)
    {
        var result = await _addressRepository.DeleteAsync(addressId);
        result.OnFailure(() => throw new InvalidOperationException(result.Error));
    }
}