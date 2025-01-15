using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;
using Laundry.Domain.Enums;

namespace Laundry.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    // private readonly OrderPlacedHandler _orderPlacedHandler;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public Task PlaceOrderAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOrdersAsync(int userId)
    {
         throw new NotImplementedException();
    }

    public Task<Order> GetOrderAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePaymentStatusAsync(int orderId, PaymentStatus status)
    {
        throw new NotImplementedException();
    }

    // public async Task<Result> MarkCouponAsUsed(int couponId)
    // {
    //     try
    //     {
    //         var coupon = await _context.Coupons
    //             .FirstOrDefaultAsync(c => c.Id == couponId);
    //     
    //         if (coupon == null)
    //         {
    //             return Result.Fail($"Coupon with id {couponId} not found.");
    //         }
    //         if (coupon.UsedCount > 0)
    //         {
    //             --coupon.UsedCount;
    //             _context.Coupons.Update(coupon);
    //         }
    //         else
    //         {
    //             _context.Coupons.Remove(coupon);
    //         }
    //
    //         await _context.SaveChangesAsync();
    //     
    //         return Result.Success();
    //     }
    //     catch (Exception e)
    //     {
    //         return Result.Fail(
    //             $"Error decrementing/deleting a coupon {couponId}: {e.Message}");
    //     }
    // }
}
