using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Services;

public interface IServiceService
{
    public IQueryable<Service> GetAllAvailableServices();
    public IQueryable<Service> GetAllServices();
    public void AddService(Service service);
    public void UpdateService(Service service);
    public void DeleteService(int id);
}