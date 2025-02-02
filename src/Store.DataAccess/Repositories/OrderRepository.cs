using Store.Domain.Contracts.Repositories;
using Store.Domain.Entities;
using Store.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Store.DataAccess.Repositories;

public class OrderRepository(AppDbContext context) : GenericRepository<Order>(context), IOrderRepository
{
    public async Task<Result<IEnumerable<Order>>> GetOrdersOfUser(int userId)
    {
        try
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .Include(o => o.Feedbacks)
                .Include(o => o.User)
                .Include(o => o.Address)
                .Include(o => o.Coupon)
                .ToListAsync();

            return Result.Success<IEnumerable<Order>>(orders);
        }
        catch (Exception e)
        {
            return Result.Fail<IEnumerable<Order>>($"Error fetching orders: {e.Message}");
        }
    }

    public async Task<Result<Order>> GetOrderAsync(int orderId)
    {
        try
        {
            var order = await _context.Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.Feedbacks)
                .Include(o => o.User)
                .Include(o => o.Address)
                .Include(o => o.Coupon)
                .FirstOrDefaultAsync();
            if (order == null)
            {
                return Result.Fail<Order>($"No order {orderId}");
            }
            
            return Result.Success(order);
        }
        catch (Exception e)
        {
            return Result.Fail<Order>($"Error fetching order {orderId}: {e.Message}");
        }
    }

    public async Task<Result<IEnumerable<Order>>> GetAllOrders()
    {
        try
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Feedbacks)
                .Include(o => o.User)
                .Include(o => o.Address)
                .Include(o => o.Coupon)
                .AsNoTracking()
                .ToListAsync();
            
            return Result.Success((IEnumerable<Order>) orders);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<Order>>($"Error fetching orders: {ex.Message}");
        }
    }

    public async Task<Result<int>> CreateOrderAsync(Order order)
    {
        try
        {
            var entityEntry = await _context.AddAsync(order);
            await _context.SaveChangesAsync();
            
            return Result.Success(entityEntry.Entity.Id);
        }
        catch (Exception e)
        {
            return Result.Fail<int>(e.Message);
        }
    }

    public async Task<Result> UpdateOrderAsync(Order order)
    {
        try
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
    }

    public async Task<Result<IEnumerable<OrderItem>>> GetOrderItemsAsync(int orderId)
    {
        try
        {
            var orderItems = await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();

            return Result.Success<IEnumerable<OrderItem>>(orderItems);
        }
        catch (Exception e)
        {
            return Result.Fail<IEnumerable<OrderItem>>(
                    $"Problem getting order items of order {orderId}: {e.Message}");
        }
    }
}
