using Laundry.Domain.Entities;
using Laundry.Domain.Enums;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Services;

public interface IOrderService
{
    Task<Order> PlaceOrderAsync(CreateOrderDto order);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<IEnumerable<Order>> GetUserOrdersAsync(int userId);
    Task<Order> GetOrderAsync(int orderId);
    Task UpdateOrderAsync(Order order);
    Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId);
}
