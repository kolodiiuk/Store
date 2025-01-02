using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;

namespace Laundry.Domain.Services;

public class BasketService : IBasketService
{
    public void AddItemToBasket(BasketItem basketItem)
    {
        throw new NotImplementedException();
    }

    public void DeleteItemFromBasket(int basketItemId)
    {
        throw new NotImplementedException();
    }

    public List<BasketItem> GetBasket(int userId)
    {
        throw new NotImplementedException();
    }

    public decimal CalculateTotal(int userId)
    {
        throw new NotImplementedException();
    }
}