using Laundry.Domain.Entities;

namespace Laundry.Domain.Contracts.Services;

public interface IBasketService
{
    void AddItemToBasket(BasketItem basketItem);
    void DeleteItemFromBasket(int basketItemId);
    List<BasketItem> GetBasket(int userId);
    decimal CalculateTotal(int userId);
}