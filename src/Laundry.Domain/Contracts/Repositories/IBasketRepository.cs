using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface IBasketRepository : IGenericRepository<BasketItem>
{
    Task<Result<IEnumerable<BasketItem>>> GetUserBasketAsync(int userId);
}
