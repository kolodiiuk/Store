using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;
using Laundry.Domain.Utils;

namespace Laundry.Api.DataAccess.Repositories;

public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
{
    public CouponRepository(LaundryDbContext context) : base(context)
    {
        
    }
    //getByCond, add
    public Task<Result<IQueryable<Coupon>>> GetCouponsAvailableForService(int serviceId)
    {
        throw new NotImplementedException();
    }
}