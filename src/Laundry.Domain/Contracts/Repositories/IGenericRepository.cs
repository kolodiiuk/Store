using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
        Task<Result<IQueryable<T>>> GetAllAsync();
        Task<Result<T>> GetById(int id);
        
        Task<Result> CreateAsync(T entity);
        Task<Result> UpdateAsync(T entity);
        Task<Result> DeleteAsync(T entity);
}