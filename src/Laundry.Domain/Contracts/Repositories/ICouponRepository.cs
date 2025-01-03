using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface ICouponRepository : IGenericRepository<Coupon>
{
    Task<Result<IQueryable<Coupon>>> GetCouponsAvailableForService(int serviceId);
}