using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
{
    public CouponRepository(LaundryDbContext context) : base(context)
    {
    }

    public async Task<Result<Coupon>> GetCouponByCodeAsync(string couponCode)
    {
        try
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == couponCode);
            if (coupon == null)
            {
                return Result<Coupon>.Fail<Coupon>($"No such coupon {couponCode}");
            }

            return Result<Coupon>.Success<Coupon>(coupon);
        }
        catch (Exception e)
        {
            return Result<Coupon>.Fail<Coupon>(
                $"Data access error: {e.Message}");
        }
    }
}
