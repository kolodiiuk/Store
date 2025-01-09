using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;

namespace Laundry.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    // private readonly OrderPlacedHandler _orderPlacedHandler;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public void PlaceOrder()
    {
        throw new NotImplementedException();
    }

    public List<Order> GetOrders(int userId)
    {
        throw new NotImplementedException();
    }

    public Order GetOrder(int orderId)
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