using Store.Domain.Entities;

namespace Store.Domain.Contracts.Services;

public interface IAddressService
{
    Task<IEnumerable<Address>> GetAllAddressesAsync();
    Task<IEnumerable<Address>> GetUserAddressesAsync(int userId);
    Task<Address> GetAddressByIdAsync(int addressId);
    Task<int> CreateAddressAsync(Address address);
    Task UpdateAddressAsync(Address address);
    Task DeleteAddressAsync(int addressId);
}