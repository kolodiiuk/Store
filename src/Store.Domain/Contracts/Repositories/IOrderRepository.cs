using Store.Domain.Entities;
using Store.Domain.Utils;

namespace Store.Domain.Contracts.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Result<IEnumerable<Order>>> GetOrdersOfUser(int userId);
    
    Task<Result<Order>> GetOrderAsync(int orderId);
    
    Task<Result<IEnumerable<Order>>> GetAllOrders();
    
    Task<Result<int>> CreateOrderAsync(Order order);
    
    Task<Result> UpdateOrderAsync(Order order);
    
    Task<Result<IEnumerable<OrderItem>>> GetOrderItemsAsync(int orderId);
}
