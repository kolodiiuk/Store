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
            var coupon = await _context.Coupons
                .Include(c => c.ServiceCoupons)
                .FirstOrDefaultAsync(c => c.Code == couponCode);

            if (coupon == null)
            {
                return Result<Coupon>.Fail<Coupon>($"No such coupon {couponCode}");
            }

            return Result<Coupon>.Success<Coupon>(coupon);
        }
        catch (Exception e)
        {
            return Result<Coupon>.Fail<Coupon>($"Data access error: {e.Message}");
        }
    }

    public async Task<Result<Coupon>> GetCouponByIdAsync(int id)
    {
        try
        {
            var coupon = await _context.Coupons
                .Include(c => c.ServiceCoupons)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (coupon == null)
            {
                return Result<Coupon>.Fail<Coupon>($"Coupon with id {id} not found");
            }

            return Result<Coupon>.Success<Coupon>(coupon);
        }
        catch (Exception e)
        {
            return Result<Coupon>.Fail<Coupon>($"Failure getting coupon {id}: {e.Message}");
        }
    }

    public async Task<Result<IEnumerable<Coupon>>> GetAllCouponsAsync()
    {
        try
        {
            var coupons = await _context.Coupons
                .Include(c => c.ServiceCoupons)
                .ToListAsync();

            return Result<IEnumerable<Coupon>>.Success<IEnumerable<Coupon>>(coupons);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Coupon>>
                .Fail<IEnumerable<Coupon>>($"Failure getting coupons: {e.Message}");
        }
    }

    public async Task<Result<int>> CreateCouponAsync(Coupon coupon, IEnumerable<int> serviceIds)
    {
        try
        {
            coupon.ServiceCoupons = new List<ServiceCoupon>();

            if (serviceIds != null)
            {
                foreach (var serviceId in serviceIds)
                {
                    coupon.ServiceCoupons.Add(new ServiceCoupon
                    {
                        ServiceId = serviceId,
                        CouponId = coupon.Id
                    });
                }
            }

            await _context.Coupons.AddAsync(coupon);
            await _context.SaveChangesAsync();

            return Result<int>.Success<int>(coupon.Id);
        }
        catch (Exception e)
        {
            return Result<int>.Fail<int>($"Failure creating coupon: {e.Message}");
        }
    }

    public async Task<Result> UpdateCouponAsync(Coupon coupon, IEnumerable<int> serviceIds)
    {
        // if (coupon == null || serviceIds == null)
        // {
        //     return Result.Fail("Invalid input data");
        // }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var existingCoupon = await _context.Coupons
                    .Include(c => c.ServiceCoupons)
                    .FirstOrDefaultAsync(c => c.Id == coupon.Id);

                if (existingCoupon == null)
                {
                    return Result.Fail($"Coupon with id {coupon.Id} not found");
                }

                _context.Entry(existingCoupon).CurrentValues.SetValues(coupon);

                // Remove existing ServiceCoupons
                _context.ServiceCoupons.RemoveRange(existingCoupon.ServiceCoupons);

                // Add new ServiceCoupons
                foreach (var serviceId in serviceIds)
                {
                    var serviceCoupon = new ServiceCoupon
                    {
                        ServiceId = serviceId,
                        CouponId = coupon.Id
                    };
                    _context.ServiceCoupons.Add(serviceCoupon);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Result.Success();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                return Result.Fail($"Failure updating coupon {coupon.Id}: {e.Message}");
            }
        }
    }

    public async Task<Result> DeleteCouponAsync(int id)
    {
        try
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return Result.Fail($"Coupon with id {id} not found");
            }

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Fail($"Failure deleting coupon {id}: {e.Message}");
        }
    }
}