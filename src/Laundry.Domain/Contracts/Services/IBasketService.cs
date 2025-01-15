using Laundry.Domain.Entities;

namespace Laundry.Domain.Contracts.Services;

public interface IBasketService
{
    Task<int> AddItemToBasketAsync(BasketItem basketItem);
    Task UpdateQuantity(int basketItemId, int newValue);
    Task DeleteItemFromBasket(int basketItemId);
    Task<IEnumerable<BasketItem>> GetBasket(int userId);
    Task<decimal> CalculateTotal(int userId);
}
