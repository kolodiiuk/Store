using Laundry.Domain.Entities;
using Laundry.Domain.Enums;

namespace Laundry.Domain.Contracts.Services;

public interface IOrderService
{
    Task PlaceOrderAsync();
    Task<IEnumerable<Order>> GetOrdersAsync(int userId);
    Task<Order> GetOrderAsync(int orderId);
    Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
    Task UpdatePaymentStatusAsync(int orderId, PaymentStatus status);
}
