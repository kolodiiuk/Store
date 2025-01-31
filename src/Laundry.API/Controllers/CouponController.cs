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
    public async Task<ActionResult<List<UpdateCouponDto>>> GetCoupons()
    {
        var result = await _couponService.GetAllCouponsAsync();
        if (result.Failure)
        {
            return BadRequest(result.Error);
        }

        List<UpdateCouponDto> couponDtos = new List<UpdateCouponDto>();
        foreach (var c in result.Value)
        {
            UpdateCouponDto dto = new UpdateCouponDto()
            {
                Code = c.Code,
                EndDate = c.EndDate,
                StartDate = c.StartDate,
                Id = c.Id,
                Percentage = c.Percentage,
                UsedCount = c.UsedCount,
                ServiceIds = c.ServiceCoupons
                    .Select(coupon => coupon.ServiceId)
                    .ToList()
            };
            couponDtos.Add(dto);
        }


        return Ok(couponDtos);
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
            var couponId = await _couponService.CreateCouponAsync(coupon, couponDto.ServiceIds);

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
            var isValid = await _couponService.ValidateCouponAsync(code);
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
            await _couponService.UpdateCouponAsync(coupon, couponDto.ServiceIds ?? new List<int>());

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
            await _couponService.DeleteCouponAsync(couponId);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}