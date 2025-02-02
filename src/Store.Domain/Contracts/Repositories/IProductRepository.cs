using Store.Domain.Entities;
using Store.Domain.Utils;

namespace Store.Domain.Contracts.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    public Task<Result<IEnumerable<Product>>> GetAllAvailableProductsAsync();
}