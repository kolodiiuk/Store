using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;

namespace Laundry.Domain.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _repository;
    
    public ServiceService(IServiceRepository repository)
    {
        _repository = repository;
    } 
    
    public async Task<IEnumerable<Service>> GetAllAvailableServicesAsync()
    {
        var availableServices = await _repository.GetAllAvailableServicesAsync();
        if (availableServices.Failure)
        {
            throw new Exception("Failure getting available services");
        }

        return availableServices.Value;
    }

    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        var services = await _repository.GetAllAsync();
        if (services.Failure)
        {
            throw new Exception("Failure getting services");
        }

        return services.Value;
    }

    public async Task<Service> GetServiceByIdAsync(int id)
    {
        var services = await _repository.GetByIdAsync(id);
        if (services.Failure)
        {
            throw new Exception("Failure getting services");
        }

        return services.Value;
    }

    public async Task<int> AddServiceAsync(Service service)
    {
        var result = await _repository.CreateAsync(service);
        if (result.Failure)
        {
            throw new Exception("Failure adding a new service");
        }

        return result.Value;
    }

    public async Task UpdateServiceAsync(Service service)
    {
        var result = await _repository.UpdateAsync(service);
        if (result.Failure)
        {
            throw new Exception("Failure updating a service");
        }
    }

    public async Task DeleteServiceAsync(int id)
    {
        var result = await _repository.DeleteAsync(id);
        if (result.Failure)
        {
            throw new Exception("Failure deleting a service");
        }
    }
}