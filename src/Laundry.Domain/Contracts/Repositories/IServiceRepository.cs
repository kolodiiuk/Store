using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface IServiceRepository : IGenericRepository<Service>
{
    public Result<IQueryable<Service>> GetAllAvailableServices();

}