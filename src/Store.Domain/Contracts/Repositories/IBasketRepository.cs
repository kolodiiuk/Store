using Store.Domain.Entities;
using Store.Domain.Utils;

namespace Store.Domain.Contracts.Repositories;

public interface IBasketRepository : IGenericRepository<BasketItem>
{
    Task<Result<IEnumerable<BasketItem>>> GetUserBasketAsync(int userId);
}
