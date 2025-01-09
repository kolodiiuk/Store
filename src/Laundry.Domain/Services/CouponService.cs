using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Contracts.Services;

namespace Laundry.Domain.Services;

public class CouponService : ICouponService
{
   private readonly ICouponRepository _couponRepository;

   public CouponService(ICouponRepository couponRepository)
   {
      _couponRepository = couponRepository;
   }
}