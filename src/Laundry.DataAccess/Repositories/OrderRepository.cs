using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class OrderRepository(LaundryDbContext context) : GenericRepository<Order>(context), IOrderRepository
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

            return Result<IEnumerable<Order>>.Success<IEnumerable<Order>>(orders);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Order>>
                .Fail<IEnumerable<Order>>($"Error fetching orders: {e.Message}");
        }
    }

    public async Task<Result<Order>> GetOrderAsync(int orderId)
    {
        try
        {
            var order = await _context.Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Service)
                .Include(o => o.Feedbacks)
                .Include(o => o.User)
                .Include(o => o.Address)
                .Include(o => o.Coupon)
                .FirstOrDefaultAsync();
            if (order == null)
            {
                return Result<Order>.Fail<Order>($"No order {orderId}");
            }
            
            return Result<Order>.Success<Order>(order);
        }
        catch (Exception e)
        {
            return Result<Order>.Fail<Order>($"Error fetching order {orderId}");
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
            
            return Result<IEnumerable<Order>>.Success((IEnumerable<Order>) orders);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Order>>
                .Fail<IEnumerable<Order>>($"Error fetching orders: {ex.Message}");
        }
    }

    public async Task<Result<int>> CreateOrderAsync(Order order)
    {
        try
        {
            var entityEntry = await _context.AddAsync(order);
            await _context.SaveChangesAsync();
            
            return Result<int>.Success(entityEntry.Entity.Id);
        }
        catch (Exception e)
        {
            return Result<int>.Fail<int>(e.Message);
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
            var orderItems = _context.OrderItems.Where(oi => oi.OrderId == orderId);

            return Result<IEnumerable<OrderItem>>
                .Success<IEnumerable<OrderItem>>(orderItems);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<OrderItem>>
                .Fail<IEnumerable<OrderItem>>(
                    $"Problem getting order items of order {orderId}: {e.Message}");
        }
    }
}
