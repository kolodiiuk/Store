using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Services;

public class CouponService : ICouponService
{
    private readonly ICouponRepository _couponRepository;

    public CouponService(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<Result<IEnumerable<Coupon>>> GetAllCoupons()
    {
        var result = await _couponRepository.GetAllAsync();
        if (result.Failure)
        {
            return Result<IEnumerable<Coupon>>.Fail<IEnumerable<Coupon>>(result.Error);
        }

        return Result<IEnumerable<Coupon>>.Success(result.Value);
    }

    public async Task<int> CreateCoupon(Coupon coupon)
    {
        var result = await _couponRepository.CreateAsync(coupon);
        if (result.Failure)
        {
            throw new Exception(result.Error);
        }

        return result.Value;
    }

    public async Task UpdateCoupon(Coupon coupon)
    {
        var result = await _couponRepository.UpdateAsync(coupon);
        if (result.Failure)
        {
            throw new Exception(result.Error);
        }
    }

    public async Task DeleteCoupon(int couponId)
    {
        var result = await _couponRepository.DeleteAsync(couponId);
        if (result.Failure)
        {
            throw new Exception(result.Error);
        }
    }

    public async Task<bool> ValidateCoupon(string code)
    {
        var result = await _couponRepository.GetCouponByCodeAsync(code);
        if (result.Failure && result.Error.Contains("Data access"))
        {
            throw new Exception(result.Error);
        }

        return result.IsSuccess;
    }
}