using Store.Domain.Entities;
using Store.Domain.Utils;

namespace Store.Domain.Contracts.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
        Task<Result<IEnumerable<T>>> GetAllAsync();
        Task<Result<T>> GetByIdAsync(int id);
        Task<Result<int>> CreateAsync(T entity);
        Task<Result> UpdateAsync(T entity);
        Task<Result> DeleteAsync(int id);
        
}