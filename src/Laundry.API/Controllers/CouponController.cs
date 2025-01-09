using Laundry.API.Dto;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouponController : ControllerBase
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    [HttpGet(Name = "all")]
    public async Task<ActionResult<List<Coupon>>> GetCoupons()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateCoupon([FromForm] CreateCouponDto couponDto)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{code}")]
    public async Task<ActionResult<bool>> ValidateCoupon(string code)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public async Task<ActionResult<Coupon>> UpdateCoupon([FromForm] UpdateCouponDto couponDto)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{couponId}")]
    public async Task<ActionResult> DeleteCoupon(int couponId)
    {
        throw new NotImplementedException();
    }
}
