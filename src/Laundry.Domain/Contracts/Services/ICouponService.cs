using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Services;

public interface ICouponService
{
    Task<Result<IEnumerable<Coupon>>> GetAllCouponsAsync(); 
    Task<int> CreateCouponAsync(Coupon coupon, IEnumerable<int> serviceIds);
    Task UpdateCouponAsync(Coupon coupon, IEnumerable<int> serviceIds);
    Task DeleteCouponAsync(int couponId);
    Task<bool> ValidateCouponAsync(string code);
}
