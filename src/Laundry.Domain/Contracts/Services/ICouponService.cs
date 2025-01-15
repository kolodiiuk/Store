using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Services;

public interface ICouponService
{
    Task<Result<IEnumerable<Coupon>>> GetAllCoupons(); 
    Task<int> CreateCoupon(Coupon coupon);
    Task UpdateCoupon(Coupon coupon);
    Task DeleteCoupon(int couponId);
    Task<bool> ValidateCoupon(string code);
}
