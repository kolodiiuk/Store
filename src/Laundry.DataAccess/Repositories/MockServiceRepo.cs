using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Enums;
using Laundry.Domain.Utils;

namespace Laundry.DataAccess.Repositories
{
    public class MockServiceRepo : IServiceRepository
    {
        private readonly List<Service> _services = new List<Service>
        {
            new Service { Id = 1, Name = "Dry Cleaning", PricePerUnit = 10.0m, UnitType = UnitType.Pcs, IsAvailable = true },
            new Service { Id = 2, Name = "Laundry", PricePerUnit = 5.0m, UnitType = UnitType.Kg, IsAvailable = false },
            new Service { Id = 3, Name = "Ironing", PricePerUnit = 2.0m, UnitType = UnitType.Pcs, IsAvailable = true },
            new Service { Id = 4, Name = "Washing", PricePerUnit = 3.0m, UnitType = UnitType.Kg, IsAvailable = true },
            new Service { Id = 5, Name = "Drying", PricePerUnit = 4.0m, UnitType = UnitType.Kg, IsAvailable = true },
            new Service { Id = 6, Name = "Folding", PricePerUnit = 1.0m, UnitType = UnitType.Pcs, IsAvailable = true  },
        };

        public Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return Task.FromResult((IEnumerable<Service>) _services);
        }

        public Task<Result<IEnumerable<Service>>> GetAllAsync()
        {
            var queryableServices = _services;
            return Task.FromResult(Result<IEnumerable<Service>>.Success((IEnumerable<Service>) queryableServices));
        }

        public Task<Result<Service>> GetByIdAsync(int id)
        {
            var service = _services.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(Result<Service>.Success(service));
        }

        public Task<Result<int>> CreateAsync(Service entity)
        {
            entity.Id = _services.Count + 1;
            _services.Add(entity);
            return Task.FromResult(Result.Success(entity.Id));
        }

        public Task<Result> UpdateAsync(Service entity)
        {
            var existingService = _services.FirstOrDefault(s => s.Id == entity.Id);
            if (existingService != null)
            {
                _services.Remove(existingService);
                _services.Add(entity);
            }
            return Task.FromResult(Result.Success());
        }

        public Task<Result> DeleteAsync(int id)
        {
            var service = _services.FirstOrDefault(s => s.Id == id);
            if (service != null)
            {
                _services.Remove(service);
            }
            return Task.FromResult(Result.Success());
        }

        public Task<Result<IEnumerable<Service>>> GetAllAvailableServicesAsync()
        {
            var availableServices = _services.Where(s => s.IsAvailable);
            return Task.FromResult(Result<IEnumerable<Service>>.Success(availableServices));
        }
    }
}