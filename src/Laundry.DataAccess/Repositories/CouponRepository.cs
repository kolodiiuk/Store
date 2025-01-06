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

    public Result<IQueryable<Coupon>> GetCouponsAvailableForService(int serviceId)
    {
        try
        {
            var coupons = _context.Coupons
                .Where(c => c.ServiceCoupons.Any(sc => sc.ServiceId == serviceId) && c.StartDate <= DateOnly.FromDateTime(DateTime.Now) 
                    && c.EndDate >= DateOnly.FromDateTime(DateTime.Now) && c.UsedCount > 0)
                .AsNoTracking();

            return Result.Success(coupons);
        }
        catch (Exception e)
        {
            return Result.Fail<IQueryable<Coupon>>(e.Message);
        }
    }
}