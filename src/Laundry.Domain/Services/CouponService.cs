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

    public async Task<Result<IEnumerable<Coupon>>> GetAllCouponsAsync()
    {
        var result = await _couponRepository.GetAllCouponsAsync();
        result.OnFailure(() => Result<IEnumerable<Coupon>>.Fail<IEnumerable<Coupon>>(result.Error));

        return Result<IEnumerable<Coupon>>.Success(result.Value);
    }

    public async Task<int> CreateCouponAsync(Coupon coupon, IEnumerable<int> serviceIds)
    {
        var result = await _couponRepository.CreateCouponAsync(coupon, serviceIds);
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }

    public async Task UpdateCouponAsync(Coupon coupon, IEnumerable<int> serviceIds)
    {
        var result = await _couponRepository.UpdateCouponAsync(coupon, serviceIds);
        result.OnFailure(() => throw new Exception(result.Error));
    }

    public async Task DeleteCouponAsync(int couponId)
    {
        var result = await _couponRepository.DeleteCouponAsync(couponId);
        result.OnFailure(() => throw new Exception(result.Error));
    }

    public async Task<bool> ValidateCouponAsync(string code)
    {
        var result = await _couponRepository.GetCouponByCodeAsync(code);
        if (result.Failure && result.Error.Contains("Data access"))
        {
            throw new Exception(result.Error);
        }

        return result.IsSuccess;
    }
}
