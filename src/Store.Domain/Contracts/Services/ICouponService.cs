using Store.Domain.Entities;
using Store.Domain.Utils;

namespace Store.Domain.Contracts.Services;

public interface ICouponService
{
    Task<Result<IEnumerable<Coupon>>> GetAllCouponsAsync(); 
    Task<int> CreateCouponAsync(Coupon coupon, IEnumerable<int> productIds);
    Task UpdateCouponAsync(Coupon coupon, IEnumerable<int> productIds);
    Task DeleteCouponAsync(int couponId);
    Task<bool> ValidateCouponAsync(string code);
}
