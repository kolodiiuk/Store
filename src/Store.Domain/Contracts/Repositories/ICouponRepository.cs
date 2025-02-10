using Store.Domain.Entities;
using Store.Domain.Utils;

namespace Store.Domain.Contracts.Repositories;

public interface ICouponRepository : IGenericRepository<Coupon>
{
    Task<Result<Coupon>> GetCouponByCodeAsync(string couponCode);
    
    Task<Result<Coupon>> GetCouponByIdAsync(int id);
    
    Task<Result<IEnumerable<Coupon>>> GetAllCouponsAsync();
    
    Task<Result<int>> CreateCouponAsync(Coupon coupon, IEnumerable<int> productIds);
    
    Task<Result> UpdateCouponAsync(Coupon coupon, IEnumerable<int> productIds);
    
    Task<Result> DeleteCouponAsync(int id);
}
