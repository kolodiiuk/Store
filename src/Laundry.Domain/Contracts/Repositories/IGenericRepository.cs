using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
        Result<IQueryable<T>> GetAll();
        Task<Result<T>> GetByIdAsync(int id);
        Task<Result<int>> CreateAsync(T entity);
        Task<Result> UpdateAsync(T entity);
        Task<Result> DeleteAsync(int id);
}