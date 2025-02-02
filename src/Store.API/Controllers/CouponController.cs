using AutoMapper;
using Store.Domain.Contracts.Services;
using Store.Domain.Entities;
using Store.Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;

namespace Store.API.Controllers;

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
    public async Task<ActionResult<List<UpdateCouponDto>>> GetCouponsAsync()
    {
        var result = await _couponService.GetAllCouponsAsync();
        result.OnFailure(() => BadRequest(result.Error));

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
                ProductIds = c.ProductCoupons
                    .Select(coupon => coupon.ProductId)
                    .ToList()
            };
            couponDtos.Add(dto);
        }


        return Ok(couponDtos);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateCouponAsync([FromBody] CreateCouponDto couponDto)
    {
        if (couponDto == null)
        {
            return BadRequest("Invalid coupon data");
        }

        var coupon = _mapper.Map<Coupon>(couponDto);

        try
        {
            var couponId = await _couponService.CreateCouponAsync(coupon, couponDto.ProductIds);

            return Ok(couponId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("code")]
    public async Task<ActionResult> ValidateCouponAsync([FromBody] string code)
    {
        try
        {
            var isValid = await _couponService.ValidateCouponAsync(code);
            if (!isValid)
            {
                return BadRequest($"No such coupon {code}");
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Coupon>> UpdateCouponAsync([FromBody] UpdateCouponDto couponDto)
    {
        if (couponDto == null)
        {
            return BadRequest("Invalid coupon data");
        }

        var coupon = _mapper.Map<Coupon>(couponDto);

        try
        {
            await _couponService.UpdateCouponAsync(coupon, couponDto.ProductIds ?? new List<int>());

            return Ok(coupon);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{couponId:int}")]
    public async Task<ActionResult> DeleteCouponAsync(int couponId)
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