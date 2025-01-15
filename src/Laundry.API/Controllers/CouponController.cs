using AutoMapper;
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
    private readonly IMapper _mapper;

    public CouponController(ICouponService couponService, IMapper mapper)
    {
        _couponService = couponService;
        _mapper = mapper;
    }

    [HttpGet(Name = "all")]
    public async Task<ActionResult<List<Coupon>>> GetCoupons()
    {
        var result = await _couponService.GetAllCoupons();
        if (result.Failure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value.ToList());
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateCoupon([FromForm] CreateCouponDto couponDto)
    {
        if (couponDto == null)
        {
            return BadRequest("Invalid coupon data");
        }

        var coupon = _mapper.Map<Coupon>(couponDto);

        try
        {
            var couponId = await _couponService.CreateCoupon(coupon);
            
            return Ok(couponId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{code}")]
    public async Task<ActionResult> ValidateCoupon(string code)
    {
        try
        {
            var isValid = await _couponService.ValidateCoupon(code);
            if (!isValid)
            {
                return NotFound($"No such coupon {code}");
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Coupon>> UpdateCoupon([FromForm] UpdateCouponDto couponDto)
    {
        if (couponDto == null)
        {
            return BadRequest("Invalid coupon data");
        }

        var coupon = _mapper.Map<Coupon>(couponDto);

        try
        {
            await _couponService.UpdateCoupon(coupon);
            
            return Ok(coupon);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{couponId}")]
    public async Task<ActionResult> DeleteCoupon(int couponId)
    {
        try
        {
            await _couponService.DeleteCoupon(couponId);
            
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}