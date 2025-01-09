using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface IServiceRepository : IGenericRepository<Service>
{
    public Task<Result<IEnumerable<Service>>> GetAllAvailableServicesAsync();
}