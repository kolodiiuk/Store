using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;

namespace Laundry.Domain.Services;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepository;

    public BasketService(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<IEnumerable<BasketItem>> GetBasket(int userId)
    {
        var result = await _basketRepository.GetUserBasketAsync(userId);
        if (result.Failure)
        {
            throw new Exception(result.Error);
        }

        return result.Value ?? Enumerable.Empty<BasketItem>();
    }
    
    public async Task<int> AddItemToBasketAsync(BasketItem basketItem)
    {
        if (basketItem == null)
        {
            throw new ArgumentNullException(nameof(basketItem));
        }
        var result = await _basketRepository.CreateAsync(basketItem);
        if (result.Failure)
        {
            throw new Exception(result.Error);
        }

        return result.Value;
    }

    public async Task UpdateQuantity(int basketItemId, int newValue)
    {
        if (newValue < 0)
        {
            throw new ArgumentException("Quantity can't be negative", nameof(newValue));
        }
        var result = await _basketRepository.GetByIdAsync(basketItemId);
        if (result.Failure)
        {
            throw new Exception(result.Error);
        }
        if (result.Value == null)
        {
            throw new Exception($"Basket item with ID {basketItemId} not found.");
        }
        
        result.Value.Quantity = newValue;
        await _basketRepository.UpdateAsync(result.Value);
    }

    public async Task DeleteItemFromBasket(int basketItemId)
    {
        var result = await _basketRepository.DeleteAsync(basketItemId);
        if (result.Failure)
        {
            throw new Exception(result.Error);
        }
    }

    public async Task<decimal> CalculateTotal(int userId)
    {
        var result = await _basketRepository.GetUserBasketAsync(userId);
        if (result.Failure)
        {
            throw new InvalidOperationException(result.Error);
        }

        return result.Value?.Sum(bi => bi.Total) ?? 0;
    }
}
