using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface IBasketRepository : IGenericRepository<BasketItem>
{
    Result<IQueryable<BasketItem>> GetUserBasket(int userId);
}
