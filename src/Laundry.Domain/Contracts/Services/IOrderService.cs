using Laundry.Domain.Entities;

namespace Laundry.Domain.Contracts.Services;

public interface IOrderService
{
    void PlaceOrder();
    List<Order> GetOrders(int userId);
    Order GetOrder(int orderId);
}