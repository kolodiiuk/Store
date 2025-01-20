using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Result<IEnumerable<Order>>> GetOrdersOfUser(int userId);
    Task<Result<Order>> GetOrderAsync(int orderId);
    Task<Result<IEnumerable<Order>>> GetAllOrders();
    Task<Result<int>> CreateOrderAsync(Order order);
    Task<Result> UpdateOrderAsync(Order order);
}
