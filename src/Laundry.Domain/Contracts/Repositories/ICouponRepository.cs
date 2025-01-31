using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface ICouponRepository : IGenericRepository<Coupon>
{
    Task<Result<Coupon>> GetCouponByCodeAsync(string couponCode);
    Task<Result<Coupon>> GetCouponByIdAsync(int id);
    Task<Result<IEnumerable<Coupon>>> GetAllCouponsAsync();
    Task<Result<int>> CreateCouponAsync(Coupon coupon, IEnumerable<int> serviceIds);
    Task<Result> UpdateCouponAsync(Coupon coupon, IEnumerable<int> serviceIds);
    Task<Result> DeleteCouponAsync(int id);
}
