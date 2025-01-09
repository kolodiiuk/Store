using System.ComponentModel.DataAnnotations;
using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
{
    public CouponRepository(LaundryDbContext context) : base(context)
    {
    }

    public async Task<Result<Coupon>> GetCouponByCode(string couponCode)
    {
        try
        {
            var coupon = _context.Coupons.FirstOrDefault(c => c.Code == couponCode);
            if (coupon == null)
            {
                return Result<Coupon>.Fail<Coupon>($"No such coupon {couponCode}");
            }

            return Result<Coupon>.Success<Coupon>(coupon);
        }
        catch (Exception e)
        {
            return Result<Coupon>.Fail<Coupon>(
                $"Error decrementing used count/deleting a coupon {couponCode}: {e.Message}");
        }
    }
}


