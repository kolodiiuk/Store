using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;

namespace Laundry.Domain.Services;

public class ServiceService : IServiceService
{
    public IQueryable<Service> GetAllAvailableServices()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Service> GetAllServices()
    {
        throw new NotImplementedException();
    }

    public void AddService(Service service)
    {
        throw new NotImplementedException();
    }

    public void UpdateService(Service service)
    {
        throw new NotImplementedException();
    }

    public void DeleteService(int id)
    {
        throw new NotImplementedException();
    }
}