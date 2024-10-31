using Laundry.Domain.Entities;

namespace Laundry.Domain.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
        Task<IReadOnlyList<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
}